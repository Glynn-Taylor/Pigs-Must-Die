using UnityEngine;
using System.Collections;

/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Melee weapon damage script (Enemy->Player ONLY)
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *		code (when not heavily modified).
 */

public class WeaponContactScript : MonoBehaviour
{
	//Damage
	public float Force;
	//Colliders to ignore if have extra body parts that aren't not colliding due to physics layer setup.
	public Collider[] ignoreColliders;
	//Animation to check if swinging weapon
	public Animator anim;
	
	//Initialisation
	void Start ()
	{
		//Ignore any specified colliders
		foreach (Collider c in ignoreColliders) {
			Physics.IgnoreCollision (c, collider);
		}
		//Make game easier
		if(!Game.NormalDifficulty)
			Force=Force/2;
	}
	//On another collider entering bounds of this one, layermasks are prevent checks on owner
	void OnCollisionEnter (Collision col)
	{
		//If swinging and hitting player then damage him and reverse animation
		if ((col.gameObject.tag == "Player") & anim.GetFloat ("WeaponSpeed") > 0) {
			anim.SetFloat ("WeaponSpeed", -1);	
			col.gameObject.GetComponent<Character>().DamageHealth (Force);
		} 
	}
}
