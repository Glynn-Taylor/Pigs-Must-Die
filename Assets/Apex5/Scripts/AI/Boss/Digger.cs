using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Digger boss script (Teleports and acts as turret)
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class Digger : Boss {
	//Boss's gun
	public TurretScript GunScript;
	//Where to teleport to
	public Transform[] Positions;
	
	//Moving to a new random position
	void Move ()
	{
		//Teleport to random position
		int i = Random.Range(0,Positions.Length);
		transform.position=Positions[i].position;
		transform.rotation=Positions[i].rotation;
	}
	
	//Checks if is time to move yet
	void CheckMoveTime ()
	{
		//Alter moveTime based on Current health
		MoveTimeMultiplier=(Health/MaxHealth)*0.8f+0.2f;
		MoveTime=5*MoveTimeMultiplier;
		MoveTimer+=Time.deltaTime;
		//If it's time, move to new position
		if(MoveTimer>MoveTime){
			Move();
			MoveTimer=0;
		}
		
	}
	//Makes bullets faster dependent on health
	void Fire ()
	{
		//Make bullets faster based on health
		GunScript.SpeedMultiplier = 1+(1-(Health/MaxHealth))*1.5f;
	}
	//Called every frame
	void Update () {
		CheckMoveTime();
		Fire();
	}

	#region implemented abstract members of Character
	public override void CharacterDeath ()
	{
			//Add component to destroy mesh
			DeathMesh.AddComponent<DeathTest>();
			//Only do one face every 15 in order to reduce processor load
			DeathMesh.GetComponent<DeathTest>().SkipEvery=15;
			//Prevent deathmesh colliding with this
			collider.enabled=false;
			//Enable next level teleport
			Teleporter.SetActive(true);
	}

	public override void OnHurt ()
	{
		//Create a blood splatter from Resources
		Instantiate(Resources.Load ("BloodSplatS"),transform.position,transform.rotation);
	}
	#endregion
}
