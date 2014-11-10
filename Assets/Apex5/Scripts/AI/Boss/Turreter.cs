using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Turreter boss, has a special ability that fires a stream of bullets, extra turrets are attached to shields.
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */

public class Turreter : Boss
{
	//Weapon to use for special ability
	public TurretScript Turret;
	//Positions to move to
	public Transform[] SpinTo;
	//Pivot point for roatating shields
	public Transform Spinner;
	//Speed of movement
	public float Speed;
	//Current target to move to
	private Transform MoveTarget;
	//Target to shoot at
	private Transform Target;
	//Timer until can use special ability	
	private float SpecialTimer;
	//Time before can use ability again
	private float SpecialCD = 10;
	
	//Initialisation
	void Start ()
	{
		//Get player for target
		if (Target == null)
			Target = GameObject.FindGameObjectWithTag ("Player").transform;
		//Get move target
		ChooseAnotherTarget ();
	}
	//Get a random target to move to
	void ChooseAnotherTarget ()
	{
		MoveTarget = SpinTo [Random.Range (0, SpinTo.Length)];
	}
	//Move towards the movement target
	void MoveTowardsTarget ()
	{
		Vector2 MoveVector2D;
		//Get difference vector
		MoveVector2D.x = MoveTarget.position.x - transform.position.x;
		MoveVector2D.y = MoveTarget.position.z - transform.position.z;
		//Normalize difference and times by speed to get move vector
		MoveVector2D = MoveVector2D.normalized * Speed;
		Vector3 MoveVector3D = new Vector3 (MoveVector2D.x, 0, MoveVector2D.y);
		transform.position += MoveVector3D;
		
	}
	// Update is called once per frame
	void Update ()
	{
		//If close to move target set move target to a new target
		if (Vector3.Distance (transform.position, MoveTarget.position) < 2f) {
			ChooseAnotherTarget ();
		//Otherwise move towards move target
		} else {
			MoveTowardsTarget ();
		}
		//Rotate shield pivot point
		Spinner.Rotate(new Vector3(0,(1.5f-Health/MaxHealth)*2,0));
		//Check if can use special ability yet
		CheckSpecialTime ();
	}
	//Checks to see if is time to use special ability
	void CheckSpecialTime ()
	{
		//Make cooldown shorter when health is low
		SpecialCD = 2 + Health/MaxHealth*8;
		SpecialTimer+=Time.deltaTime;
		//If time to use
		if(SpecialTimer>SpecialCD){
			//Enable firing
			Turret.enabled=true;
			//Disable after 2 seconds
			Invoke("DisableTurret",2);
			SpecialTimer=0;
		}
	}
	//Disables special ability turret
	void DisableTurret(){
		Turret.enabled=false;
	}
	#region implemented abstract members of Character
	public override void CharacterDeath ()
	{
		DeathMesh.AddComponent<DeathTest> ();
		DeathMesh.GetComponent<DeathTest> ().SkipEvery = 15;
		collider.enabled = false;
		Teleporter.SetActive (true);
	}

	public override void OnHurt ()
	{
		Instantiate(Resources.Load ("BloodSplatS"),transform.position,transform.rotation);
	}
	#endregion
	
	private void LookAtTarget ()
	{
		Quaternion newRot = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Target.position - transform.position), 1);
		newRot.eulerAngles = new Vector3 ( newRot.eulerAngles.x , newRot.eulerAngles.y, newRot.eulerAngles.z);
		transform.rotation = newRot;	
	}
}
