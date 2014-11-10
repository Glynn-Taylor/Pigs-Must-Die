using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Turret for boss that uses shields, fires outwards from shields every second
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class ShieldTurret : TurretScript {
	//Position to fire from offset from cannon end (TurretScript)
	public Vector3 FireOffset;
	
	//Initialization
	void Start () {
		//Get target (player)
		if(Target==null)
			Target=GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame, handles firing
	void Update () {
		//If player in range
		if(Vector3.Distance(transform.position,Target.position)<Range){
			//If second has elapsed
			if(System.DateTime.Now.Second!=StartTime){
				FireForward();
				StartTime=System.DateTime.Now.Second;
			}
		}
	}
	//Fire projectile
	protected void FireForward(){
		//Instantiate projectile
		GameObject go = (GameObject)Instantiate(ProjectilePrefab);
		go.transform.position = CannonEnd.position+FireOffset;
		//Make velocity and ensure it's normalized and magnitude of desired speed
		Vector3 desiredVelocity = transform.forward;
		desiredVelocity=desiredVelocity.normalized;
		desiredVelocity*=SpeedMultiplier;
		//Set move vector of projectile
		go.GetComponent<ProjectileScript>().SetVelocities(desiredVelocity.x,desiredVelocity.y,desiredVelocity.z);
		go.GetComponent<ProjectileScript>().setRange(Range);
		//Set collision layer
		go.layer=10;
		audio.Play();
	}
}
