using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Quits the game on mouse down (this collider)
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class QuitOnClick : MonoBehaviour {
	//Handles mouse down on this collider
	void OnMouseDown ()
	{
		//Load the level
		Application.Quit();
	}
}
