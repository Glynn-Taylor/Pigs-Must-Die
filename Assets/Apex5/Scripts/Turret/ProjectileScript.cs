using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Moving projectile that hurts any enemy/player it connects with
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class ProjectileScript : MonoBehaviour
{
	//Movement vector
	protected Vector3 MoveVelocity;
	//Starting position for range calculation
	protected Vector3 InitialPosition;
	//Is projectile dead
	protected bool Dead;
	//Distance projectile can move before self destruction
	protected float Range = 50f;
	//Amount of damage to do
	public float Damage =1;
	
	//Set initial velocity
	public void SetVelocities (float x, float y, float z)
	{
		MoveVelocity = new Vector3 (x, y, z);
		InitialPosition = transform.position;
	}
	//Called every frame, handles movement
	void Update ()
	{
		if (MoveVelocity != null) {
			//Move
			transform.position += MoveVelocity;
			//Check if travelled too far
			if (Vector3.Distance (InitialPosition, transform.position) > Range) {
				Destroy (gameObject);	
			}
		}
		//Destroy if dead and finished playing sound
		if (Dead && !audio.isPlaying)
			Destroy (gameObject);
	}
	//On colliding
	void OnTriggerEnter (Collider other)
	{
		if(!Dead)
		if (other.collider.tag == "Player" | other.collider.tag == "Enemy") {
			//Damage the character
			if(other.gameObject.GetComponent<Character> ())
				other.gameObject.GetComponent<Character> ().DamageHealth (Damage);
			//Play audio if enemy
			if (other.collider.tag == "Enemy")
				audio.Play ();
			//Start death for when audio finishes
			Dead = true;
			Destroy (collider);
		} else if (other.collider.tag == "Obstacle") {
			Dead = true;
			Destroy (collider);
		}
		//other.GetComponent<Gravity>().SetGravity(20);
	}
	//Set function for range
	public void setRange (float r)
	{
		Range = r;	
	}
	//Helper function for ignoring collisions between this and another object
	public void setIgnore (Collider col)
	{
		Physics.IgnoreCollision (collider, col);
	}
	//Get the current movement vector
	public Vector3 getVelocity ()
	{
		return MoveVelocity;	
	}
}
