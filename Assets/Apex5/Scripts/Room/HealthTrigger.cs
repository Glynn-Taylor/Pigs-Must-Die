using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Restores health to the player on entering collider bounds
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class HealthTrigger : MonoBehaviour {
	//Rotates the object every frame to give it a spinning effect, up as difference between unity and blender axes
	void Update(){
		transform.Rotate(Vector3.up);
	}
	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if(other.tag=="Player"){
			other.GetComponent<Player>().Health++;
			//Delete this gameobject
			Destroy (gameObject);
			//Play sound
			SoundManager.Instance.Play ("HealthRestored");
		}
	}
}
