using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Boss super class
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
//Could be refined further to different arch types of boss (attack type, movement type, etc.)
public abstract class Boss : Character
{
	//Mesh to use as a base for the death splinters
	public GameObject DeathMesh;
	//GameObject to Enable after boss death
	public GameObject Teleporter;
	//Max time to teleporting/moving
	protected float MoveTime = 5f;
	//Current elapsed time since last teleport/move
	protected float MoveTimer=0;
	//How much faster to teleport/move based on Health
	protected float MoveTimeMultiplier=1;
	
}

