using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Loads next level when player enters the collider bounds
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class SwitchLevelsTrigger : MonoBehaviour {
	//When player enters collider bounds
	void OnTriggerEnter(Collider other){
		//Load level
		if(other.tag=="Player")
			Application.LoadLevel(Application.loadedLevel+1);
	}
}
