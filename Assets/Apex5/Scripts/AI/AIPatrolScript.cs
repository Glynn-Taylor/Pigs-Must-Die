using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Action;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Patrol AI script (never used due to Room system), patrols and chases target when in range
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class AIPatrolScript : AIScript {
	//List of patrol points as gameobjects for easier configuration
	public GameObject[] points;
	//Distance to chase target before giving up
	public float RoamDistance;
	//Need to get a new point to move to
	private bool getNextPoint=true;
	//Flag for chasing target
	private bool Chasing=false;
	//Flag for patrolling between points
	private bool Patrolling=true;
	//Holder for postion to return to when chasing
	private GameObject returnTo;
	//Current point index
	private int CurrentPoint=0;
	private Agent agent;
	
	// Initialization
	void Start () {
		agent = GetComponent<RAINAgent>().Agent;	
	}
	//State for patrolling between points
	void Patrol ()
	{
		//If have reached destination and need to move to the next point
		if(getNextPoint){
			if(points!=null){
				//Set target to next point
				agent.LookTarget.TransformTarget=points[CurrentPoint].transform;
				CurrentPoint++;
				CurrentPoint%=points.Length;
				getNextPoint=false;
			}
		}
		//If have not reached destination
		if(!agent.MoveTo (agent.LookTarget.TransformTarget.position,Time.deltaTime)){
			GameObject go=null;
			//Get aspect to find
			agent.GetObjectWithAspect("TestAspect",out go);
			if(go!=null){
				//Set return point
				returnTo=(GameObject)GameObject.Instantiate (new GameObject("temp"),transform.position,transform.rotation);
				Target=go;
				//Set agent target
				agent.LookTarget.TransformTarget=Target.transform;
				//Set flags
				Patrolling=false;
				Chasing=true;
				//Set roam distance based on spotting distance
				RoamDistance=Vector3.Distance(transform.position,Target.transform.position)+5;
				animation.Play ("run");
				AnimationDefaultTo="run";
				GetComponent<RAINAgent>().maxSpeed=5;
				Debug.Log ("chasing");
				//Reset sensor
				agent.GetSensor("Sensor").Reset();
			}
		}else{
			//If have reached destination then get the next point
			getNextPoint=true;
		}
		
	}
	//State for chasing/attacking target
	void Chase ()
	{
		//Move towards target if chasing
		agent.MoveTo (agent.LookTarget.TransformTarget.position,Time.deltaTime);
		//If melee close to target
		if(Vector3.Distance(transform.position,Target.transform.position)<3.5&!nonDefaultAnim){
			animation["hpunch"].speed=1;
			animation.Play ("hpunch");	
			nonDefaultAnim=true;
		}
		//If have chased outside of roaming distance then return to patrolling
		if(Vector3.Distance(transform.position,points[CurrentPoint].transform.position)>RoamDistance){
				Chasing=false;
				Patrolling=false;
				//Target return point
				agent.LookTarget.TransformTarget=returnTo.transform;
				Debug.Log ("returning");
		}
	}
	//State for returning to patrol state from chasing
	void ReturnToPatrol ()
	{
		//Move and if reached destination
		if(agent.MoveTo (agent.LookTarget.TransformTarget.position,Time.deltaTime)){		
			//Start patrolling again
			Patrolling=true;
			animation.Play ("walk");
			AnimationDefaultTo="walk";
			
			Debug.Log ("patrolling");
			//Slow down
			GetComponent<RAINAgent>().maxSpeed=2;
			//Start moving to a new point next update
			getNextPoint=true;
			agent.GetSensor("Sensor").Sense();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//If attack animation finished
		if(!animation.isPlaying){
			animation.Play(AnimationDefaultTo);
			nonDefaultAnim=false;
		}
		//State handler
		if(Patrolling){
			Patrol();
		}else if(Chasing){
			Chase();
		}else{
			ReturnToPatrol();
		}
		
	}
}
