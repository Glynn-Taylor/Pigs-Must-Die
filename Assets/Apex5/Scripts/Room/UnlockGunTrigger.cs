using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Unlocks a new gun for player upon entering collider bounds
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class UnlockGunTrigger : MonoBehaviour {
	//Index of gun to unlock
	public int Slot;
	
	//Called every frame, rotates gameobject to give spinning effect
	void Update(){
		transform.Rotate(Vector3.forward);
	}
	//Upon player entering collider bounds
	void OnTriggerEnter(Collider other){
		if(other.tag=="Player"){
			//Unlock gun of index
			other.GetComponent<Player>().UnlockGun(Slot);
			//Play sound
			SoundManager.Instance.Play ("GunUnlocked");
			//Destroy this gameobject
			Destroy (gameObject);
		}
	}
}
