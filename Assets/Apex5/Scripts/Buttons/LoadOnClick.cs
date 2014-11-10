using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Loads a level on mouse down (this collider)
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class LoadOnClick : MonoBehaviour
{
	//Index of level to change to
	public int Level;
	
	//Initialization
	void Start ()
	{
		//Can only exist in menus where player does not exist, so if returning then destroy player as player not destroyed on load
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			Destroy (GameObject.FindGameObjectWithTag ("Player"));
		}
		//Get cursor back
		Screen.lockCursor = false;
	}
	//Handles mouse down on this collider
	void OnMouseDown ()
	{
		//Load the level
		Application.LoadLevel (Level);
	}
}
