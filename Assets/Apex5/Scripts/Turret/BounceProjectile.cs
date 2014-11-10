using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Last boss's projectiles, players have to hit them back by colliding a bullet with them and then if they hit the boss the boss loses health
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class BounceProjectile : ProjectileScript
{
	//Has been bounced back by a bullet
	public bool Bounced;

	//Called every frame, handles movement
	void Update ()
	{
		if (MoveVelocity != null) {
			//Move using movement vector
			transform.position += MoveVelocity;
			//If have passed range of projectile, kill self
			if (Vector3.Distance (InitialPosition, transform.position) > Range) {
				Destroy (gameObject);	
			}
		}
		//If audio finished playing when dead then destroy this
		if (Dead && !audio.isPlaying)
			Destroy (gameObject);
	}
	// On another collider entering bounds
	void  OnTriggerEnter (Collider other)
	{
		//If player then harm player
		if (other.collider.tag == "Player") {
			other.gameObject.GetComponent<Character> ().DamageHealth (1);
			Dead = true;
		//If a bullet then move in direction of bullet and destroy bullet + toggle bounced flag
		} else if (other.collider.tag == "Bullet") {
			MoveVelocity = other.GetComponent<ProjectileScript> ().getVelocity ();
			Destroy (other.gameObject);
			Bounced = true;
		//If hitting the boss then do damage to boss if is bounced
		} else if (other.collider.tag == "King") {
			if (Bounced) {
				other.GetComponent<King> ().DamageHealth ();
				Dead = true;
				audio.Play();
			}
		//Kill self on hitting geometry
		} else if (other.collider.tag == "Obstacle") {
			Dead = true;
		} else {
			Debug.Log ("Collision: " + other.collider.tag);	
		}
	}
}