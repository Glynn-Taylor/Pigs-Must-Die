using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Final boss script
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
//Doesn't extend from Boss as health is based on melon hits
public class King : MonoBehaviour
{
	//King uses seperate health and damage system as is damaged by own projectiles
	private int Health = 26; //(the fight is fairly easy, probably the easiest, but didnt have time to change it)
	public GameObject[] SpareTops; //A few spin tops that will be enabled on low health
	public TurretScript Shoota; // (boss's gun)

	void Start ()
	{
		if(!Game.NormalDifficulty){
			//Make it easy for new players by halving health
			Health=Health/2;
		}
	}


	public void DamageHealth ()
	{
		//Create blood spatter from resources
		GameObject.Instantiate(Resources.Load ("BloodSplatS"),transform.position,transform.rotation);
		//Decrease health
		Health--;
		//Load win level on Death
		if (Health == 0) {
			Application.LoadLevel (6);	
		}
		//Activate spare tops and make boss shoot 1.5x faster
		if (Health == Health/2) {
			Shoota.SpeedMultiplier = Shoota.SpeedMultiplier * 1.5f;
			for (int i=0; i<SpareTops.Length; i++) {
				SpareTops [i].SetActive (true);	
			}
		}
	}
}
