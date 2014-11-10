using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Handles player weapon switching and controls as well as death/hurt.
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class Player : Character
{
	//Current gun equipped
	public Gun CurrentGun;
	//Array of guns attached to player
	public GameObject[] Guns;
	//Texture to use for health in GUI
	public Texture2D Heart;
	//Which guns can currently be used
	private bool[] UnlockedGun = {true,false,false,false};
	//GUI reference
	private int MidWidth = Screen.width / 2;
	private int StartX;
	
	//Intialisation
	void Start ()
	{
		//Make player no destroyed on level load
		DontDestroyOnLoad (gameObject);
		//Adjust health if difficulty is easy
		if(!Game.NormalDifficulty){
			MaxHealth*=2;
			Health*=2;
		}
		//Set GUI starting x position
		StartX = (int)((float)MidWidth - (MaxHealth / 2) * 50);
		
	}
	
	// Update is called once per frame, handles mouse and key input
	void Update ()
	{
		//Fire gun and hide mouse cursor to center if LMB down
		if (Input.GetMouseButton (0)) {
			CurrentGun.Fire ();
			Screen.lockCursor = true;
		}
		//1,2,3 handle changing weapon
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SetgunActive (0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SetgunActive (1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SetgunActive (2);
		}
		//Reload on pressing R
		if (Input.GetKeyDown (KeyCode.R)) {
			CurrentGun.Reload ();
		}
		//Let cursor roam free on escape
		if (Input.GetKeyDown (KeyCode.Escape))
			Screen.lockCursor = false;
	}
	//Allow player to use a new gun by setting flag
	public void UnlockGun(int i){
		UnlockedGun [i] = true;
	}
	//Changing weapon
	void SetgunActive (int i)
	{
		//If flag is true then switch current gun gameobject
		if (UnlockedGun [i]) {
			CurrentGun.gameObject.SetActive (false);
			CurrentGun = Guns [i].GetComponent<Gun> ();
			CurrentGun.gameObject.SetActive (true);
		}
	}
	#region implemented abstract members of Character
	//Loads starting level on death
	public override void CharacterDeath ()
	{
		Application.LoadLevel (7);
	}
	//Plays a sound on hurt
	public override void OnHurt ()
	{
		SoundManager.Instance.Play ("PlayerHurt");
	}
	#endregion
	//Draws the player health bar to screen by drawing each heart
	void OnGUI ()
	{
		for (int i=0; i<Health; i++) {
			GUI.Box (new Rect (StartX + 50 * i, 0, 50, 50), Heart);
		}
	}
}
