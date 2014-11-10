using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Teleports the player to starting point when the level loads
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class SpawnTeleportScript : MonoBehaviour {
	//Failsafe 1	
	void OnLevelWasLoaded(){
		GameObject.FindGameObjectWithTag ("Player").transform.position=transform.position;
	}
	//Failsafe 2
	void Awake(){
		GameObject.FindGameObjectWithTag ("Player").transform.position=transform.position;
	}
	// Failsafe 3 don't fail harder
	void Start () {
		GameObject.FindGameObjectWithTag ("Player").transform.position=transform.position;
		Destroy (gameObject);
	}
}
