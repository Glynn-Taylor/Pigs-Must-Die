using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Trigger that specifically hurts the player, could be adapted to hurt dependent on flags
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class HurtPlayerTrigger : MonoBehaviour
{
	//Current elapsed sincelast hurt
	private float HurtTimer;
	//Time between hurts
	public float CD;
	//Can we hurt player? (sadist boolean)
	private bool CanHurt=true;

	//Called every frame
	void Update ()
	{
		//Pretty straight forward, just checks if cooldown time has elapsed
		if (!CanHurt) {
			HurtTimer += Time.deltaTime;
			if (HurtTimer > CD) {
				CanHurt = true;
				HurtTimer = 0;
			}
		}
	}
	//On entering bounds of another non owner collider, set by physics masks
	void OnTriggerEnter (Collider other)
	{
		//If rigidbody with collider has entered trigger
		if(CanHurt)
		if (other.tag == "Player") {
			//Damage player by 1
			other.GetComponent<Character>().DamageHealth(1);
			CanHurt=false;
		}
	}
}
