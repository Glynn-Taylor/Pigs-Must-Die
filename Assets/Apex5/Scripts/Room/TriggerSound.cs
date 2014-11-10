using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Plays audio when player enters collider bounds (once)
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class TriggerSound : MonoBehaviour {
	//Prevents multiple plays
	private bool HasPlayed;
	
	//Handles player entering collider bounds
	void OnTriggerEnter(Collider other){
		if(other.tag=="Player"&!HasPlayed){
			//Play audio and prevent recurring trigger
			HasPlayed=true;
			audio.Play ();
		}
		
	}
}
