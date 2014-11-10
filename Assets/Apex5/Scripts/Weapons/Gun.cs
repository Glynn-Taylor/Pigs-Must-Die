using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Base class for guns, handles firing logic for guns
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public abstract class Gun : MonoBehaviour
{
	//Delay between firing
	public float FireDelay;
	//Time of last fire
	private float LastFireTime;
	//If gun can fire
	private bool CanFire = true;
	//Amount of spare ammo (clips/rounds)
	public float SpareAmmo;
	//Amount of current/useable ammo (rounds)
	public float CurrentAmmo;
	//Maximum ammo in clip
	public float MaxCurrentAmmo;
	//Projectile to fire
	public GameObject Projectile;
	//Location to fire from
	public Transform FireLocation;
	//speed of projectile
	public float SpeedMultiplier;
	//Range of projectile
	public float Range;
	//Reloading sound name
	public string ReloadName;
	protected bool Reloading;
	//Time to reload
	public float ReloadTime;
	
	//Checks if can fire
	public void Fire ()
	{
		//If no ammo attempt to reload
		if (CurrentAmmo == 0) {
			Reload ();
		//Otherwise if not reloading and have the ammo to fire
		} else if(CurrentAmmo>0&!Reloading) {
			//If currently between fire
			if (!CanFire) {
				//Check if delay has elapsed
				if (Time.time - LastFireTime > FireDelay) {
					FireProjectile();
				}
			//Just fire
			} else {
				FireProjectile();
			}	
		}
		
	}
	//Fires a projectile
	protected void FireProjectile(){
		//Reset delay variables
		LastFireTime=Time.time;
		CanFire=false;
		//Instantiate projectile
		GameObject go = (GameObject)Instantiate(Projectile);
		//Set position and movement vectors
		go.transform.position = FireLocation.position;
		Vector3 vp = transform.parent.parent.gameObject.GetComponent<CharacterMotor>().movement.frameVelocity;
		go.GetComponent<ProjectileScript>().SetVelocities(transform.forward.x*SpeedMultiplier+vp.x,transform.forward.y*SpeedMultiplier+vp.y,transform.forward.z*SpeedMultiplier+vp.z);
		//Set range and ignore collisions from object firing
		go.GetComponent<ProjectileScript>().setRange(Range);
		go.GetComponent<ProjectileScript>().setIgnore(transform.parent.parent.collider);
		audio.Play();
		//Use ammo
		CurrentAmmo--;
	}
	//Abstract method for handling reloading
	public abstract void Reload ();
}
