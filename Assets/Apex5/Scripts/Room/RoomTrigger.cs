using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Closes room doors and spawns monsters when player enters, reopens doors when monsters are all dead
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class RoomTrigger : MonoBehaviour
{
	//Animations for closing/opening doors
	public Animation[] DoorAnimations;
	//Array of monsters to spawn/check if dead
	public GameObject[] Monsters;
	//Is room currently locked down
	bool Lockdown;
	//Are currently fading the battle music
	bool FadingAudio;
	
	//Method for fading the battle music after room is cleared
	void FadeAudio ()
	{
		//Decrease volune if still audible
		if (audio.volume > 0.1) {
			audio.volume -= 0.1f * Time.deltaTime;
		//Otherwise stop music and dont fade again
		}else{
			audio.Stop();
			FadingAudio=false;
		}
	}
	//Called every frame
	void Update ()
	{
		if(Lockdown)
		for (int i=0; i<Monsters.Length; i++) {
			//Check if monsters still alive
			if (Monsters [i] != null) {
				break;
			}
			//If not
			if (i == Monsters.Length - 1) {
				//Destroy doors
				foreach (Animation a in DoorAnimations) {
					Destroy (a.gameObject);
				}
				//Destroy this
				Destroy (gameObject);
				//Fade audio
				if (audio) {
					FadingAudio = true;
				}
			}
		}
		//Fade audio
		if (FadingAudio) {
			FadeAudio ();	
		}
	}
	//Handles player entering collider bounds
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
			//If room not active
			if (!Lockdown) {
				//Play battle music
				if (audio) {
					audio.Play ();	
				}
				//Close doors
				foreach (Animation a in DoorAnimations) {
					a.Play ();
				}
				//Enable monsters
				foreach (GameObject m in Monsters) {
					m.SetActive (true);
				}
				//Set room active flag
				Lockdown = true;
			}
	}
	
}
