using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Roller boss/mob, spins up and then charges at player, restrained by collisions and distance from an origin point.
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */

public class Roller : Boss
{
	//The object to rotate to show spinning (may as well be a Transform)
	public GameObject SpinnerObject;
	//Current target (normally player)
	public Transform Target;
	//Where to Raycast forward from to check if about to hit a wall.
	public Transform RaycastPos;
	//Origin to stop accidental wall glitching from getting out of hand (prevents movement outwards after a distance)
	public Transform Origin;
	//Time it takes to spin up, decreases as health decreases
	public float SpinUpTime = 5;
	//How fast I move
	public float MoveSpeed;
	//Are we moving
	private bool Charging = false;
	//Did we just hit a wall?
	private bool HitWallLast;
	
	//Initialisation
	void Start ()
	{
		//Aquire target
		if (Target == null) {
			Target = GameObject.FindGameObjectWithTag ("Player").transform;	
		}
	}
	//Charge forward until hitting a wall
	void Charge ()
	{
		//Spin our spinner object to show movement when charging
		SpinnerObject.transform.Rotate (new Vector3 (4, 0, 0));
		//Move self forwards at movespeed
		transform.position += transform.forward * MoveSpeed;
		//Raycasting
		Vector3 fwd = RaycastPos.TransformDirection (Vector3.forward);
		RaycastHit hit;
		//If we hit something (conditions etc logged to hit)
		if (Physics.Raycast (RaycastPos.position, fwd, out hit)) {
			//If distance to hit is directly in front
			if (hit.distance <= 1) {
				if (hit.collider.tag == "Player") {
					//We have successfully charge
					Charging = false;
				} else {
					//We have hit a wall/object
					if (HitWallLast) {
						//Go through the object to prevent roller getting stuck
					} else {
						//Stop
						Charging = false;
					}
					//Toggle whether we hit an object last
					HitWallLast=!HitWallLast;
				}
				
			}
			
			
		}else if(Vector3.Distance(transform.position,Origin.position)>60){
			//If we are too far from our origin (60) (have gone through the wall)
			Charging = false;
			//Go through the wall on next charge
			HitWallLast=true;
		}
			
	}
	//Check if need to charge
	void CheckMoveTime ()
	{
		MoveTimeMultiplier = (Health / MaxHealth) * 0.7f + 0.3f;
		//Spin and rotate based on spinner
		SpinUpTime = 5 * MoveTimeMultiplier;
		MoveTimer += Time.deltaTime;
		SpinnerObject.transform.Rotate (new Vector3 (MoveTimer * 3, 0, 0));
		//Face player
		LookAtTarget ();
		if (MoveTimer > SpinUpTime) {
			//Set charging
			Charging = true;
			MoveTimer = 0;
		}
	}
	//Rotate so that facing player
	private void LookAtTarget ()
	{
		Quaternion newRot = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Target.position - transform.position),3);
		newRot.eulerAngles = new Vector3 (transform.eulerAngles.x,newRot.eulerAngles.y, transform.eulerAngles.z);
		transform.rotation = newRot;
	}
	// Update is called once per frame
	void Update ()
	{
		if (Charging) {
			Charge ();
		} else {
			CheckMoveTime ();
		}
	}

	#region implemented abstract members of Character
	public override void CharacterDeath ()
	{
		DeathMesh.AddComponent<DeathTest> ();
		DeathMesh.GetComponent<DeathTest> ().SkipEvery = 15;
		collider.enabled = false;
		if(Teleporter)
		Teleporter.SetActive (true);
	}

	public override void OnHurt ()
	{
		
		Instantiate(Resources.Load ("BloodSplatS"),transform.position,transform.rotation);
	}
	#endregion
}
