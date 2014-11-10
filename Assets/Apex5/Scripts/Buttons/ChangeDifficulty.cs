using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Changes the difficulty of the game on mouse down (this collider)
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class ChangeDifficulty : MonoBehaviour {
	//Text renderer
	public TextMesh Text;
	
	//Handles mouse down over this collider	
	void OnMouseDown ()
	{
		//Switch difficulty in Game
		Game.NormalDifficulty=!Game.NormalDifficulty;
		//Update text to reflect change
		Text.text = Game.NormalDifficulty?"Normal":"Easy";
	}
}
