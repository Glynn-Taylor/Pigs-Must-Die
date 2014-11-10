using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Handles GUI for shotgun, logic handled in RoundBased
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class Shotgun : RoundBased {
	//Draws GUI
	void OnGUI(){
		//Draw current ammo and spare ammo
		GUI.Box (new Rect(0,0,50,50),CurrentAmmo.ToString());
		GUI.Box (new Rect(50,0,50,50),SpareAmmo.ToString());
	}
}
