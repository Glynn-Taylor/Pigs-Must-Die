using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Tracks and fires projectiles at a target
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class TurretScript : MonoBehaviour {
	//Target to aim at
	public Transform Target;
	//Projectile to fire
	public GameObject ProjectilePrefab;
	//Time upon firing
	protected int StartTime;
	//Position to fire from
	public Transform CannonEnd;
	//Speed to fire at
	public float SpeedMultiplier;
	//Range for projectile to destroy outside of
	public float Range;
	//Rate of fire (seconds)
	public float FireRate=1;
	//Times since last fire
	private float FireTime=0;
	//Lock rotation of axes
	public bool LockRotX,LockRotY,LockRotZ;

	//Initialization
	void Start () {
		//Get target (player)
		if(Target==null)
			Target=GameObject.FindGameObjectWithTag("Player").transform;
	}
	// Update is called once per frame
	void Update () {
		//If player in range
		if(Vector3.Distance(transform.position,Target.position)<Range){
			//Rotate so that facing target
			LookAtTarget();
			FireTime+=Time.deltaTime;
			//If FireRate has elapsed then fire
			if(FireTime>FireRate){
				Fire ();
				FireTime=0;
			}
		}
	}
	//Fires a projectile
	protected void Fire(){
		//Instantiate projectile
		GameObject go = (GameObject)Instantiate(ProjectilePrefab);
		go.transform.position = CannonEnd.position;
		//Set velocity to normalized vector*speed
		Vector3 desiredVelocity = Target.position-CannonEnd.position;
		desiredVelocity=desiredVelocity.normalized;
		desiredVelocity*=SpeedMultiplier;
		//Set move vector on projectile and range
		go.GetComponent<ProjectileScript>().SetVelocities(desiredVelocity.x,desiredVelocity.y,desiredVelocity.z);
		go.GetComponent<ProjectileScript>().setRange(Range);
		//Set collision layer
		go.layer=10;
		audio.Play();
	}
	//Rotate so that facing target
	protected void LookAtTarget(){
		//Get slerped rotation
		Quaternion newRot = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Target.position - transform.position), 1);
		//Set non locked axes
		newRot.eulerAngles = new Vector3 (LockRotX?0:newRot.eulerAngles.x+2, LockRotY?0:newRot.eulerAngles.y, LockRotZ?0:newRot.eulerAngles.z);
		transform.rotation = newRot;	
	}
}
