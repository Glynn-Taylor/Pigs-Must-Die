using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Health and death/hurt handler for enemies
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class Enemy : Character {
	//Mesh to use on death for split effect
	public GameObject DeathMesh;
	
	//Initialization
	void Start () {
		//Lower health on easy difficulty
		if(!Game.NormalDifficulty)
			Health*=0.5f;
	}
	#region implemented abstract members of Character
	//Handles what to do on death
	public override void CharacterDeath ()
	{
		//Add effect to mesh
		DeathMesh.AddComponent<DeathTest>();
		//Make effect less performance intense
		DeathMesh.GetComponent<DeathTest>().SkipEvery=15;
		//Disable collisions
		collider.enabled=false;
	}
	//Handles what to do after taking damage
	public override void OnHurt ()
	{
		//Create blood splatter
		Instantiate(Resources.Load ("BloodSplatS"),transform.position,transform.rotation);
	}
	#endregion
}
