using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Action;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Melee AI script; attacks the target by running directly at them
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class AIAttackScript : AIScript
{
	private const float MIN_SPEED = 11f;
	private const float ATTACK_DISTANCE = 2.2f;
	private Animator anim;
	//Flag for if AI can attack again
	private bool ResetAttackVar=false;
	//Move speed
	private float Speed;
	Agent agent;
	
	// Initialization
	void Start ()
	{
		agent = GetComponent<RAINAgent> ().Agent;
		//Get player target
		Target=GameObject.FindGameObjectWithTag("Player");
		//Set look target to player
		agent.LookTarget.TransformTarget=Target.transform;
		//Create a semi random speed
		Speed=Random.Range(MIN_SPEED,MIN_SPEED+2);
		//Reduce speed if on easy
		if(!Game.NormalDifficulty)
			Speed*=0.8f;
		//Update agent
		GetComponent<RAINAgent>().maxSpeed=Speed;
		anim = GetComponent<Animator>();
		//Setup animation
		anim.SetLayerWeight(1, 1);
		anim.SetBool("Moving",true);
		anim.SetBool("Jumping",false);
		
	}
	//State for pursuing the player
	void Chase ()
	{
		//If within attack distance attack from standing
		if (Vector3.Distance (transform.position, Target.transform.position) < ATTACK_DISTANCE ) {
			SwingSword();
			GetComponent<RAINAgent>().maxSpeed=0;
		//Otherwise reset speed and move in the direction of the target
		}else{
			GetComponent<RAINAgent>().maxSpeed=Speed;
			agent.MoveTo (agent.LookTarget.TransformTarget.position, Time.deltaTime);
		}
	}
	//Animation for sword swing, damage handled by trigger attached to weapon
	public void SwingSword ()
	{
		anim.SetFloat("WeaponSpeed",1);
		anim.SetBool("Swing",true);
		ResetAttackVar=true;
	}
	//Reset attacking variables so that can attack again
	void ResetAttackVariables ()
	{
		anim.SetBool("Swing",false);
		ResetAttackVar=false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(ResetAttackVar){
			ResetAttackVariables();
		}
		Chase ();
		
	}
}
