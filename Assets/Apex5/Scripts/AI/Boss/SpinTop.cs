using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Move an object randomly between waypoints
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class SpinTop : MonoBehaviour {
	//Points to spin to
	public Transform[] SpinTo;
	//Current speed
	public float Speed;
	//Current target point
	private Transform Target;
	//When are close enough to a point that need a new point to spin to
	public float CloseEnoughDistance=1;
	
	//Initialisation
	void Start () {
		ChooseAnotherTarget();
	}
	//Get another random position to target
	void ChooseAnotherTarget ()
	{
		Target = SpinTo[Random.Range (0,SpinTo.Length)];
	}
	//Handles moving towards the focused target
	void MoveTowardsTarget ()
	{
			Vector2 MoveVector2D;
			//Get difference
			MoveVector2D.x = Target.position.x - transform.position.x;
			MoveVector2D.y = Target.position.z - transform.position.z;
			//Normalise and multiply speed to get move vector	
			MoveVector2D = MoveVector2D.normalized * Speed;
			Vector3 MoveVector3D = new Vector3 (MoveVector2D.x, 0, MoveVector2D.y);
			//Move using position
			transform.position += MoveVector3D;
			transform.Rotate (new Vector3 (0, 0, 2));
	}
	
	// Update is called once per frame
	void Update () {
		//If close enough to target then find a new target
		if(Vector3.Distance(transform.position,Target.position)<CloseEnoughDistance){
			ChooseAnotherTarget();
		//Otherwise move towards target
		}else{
			MoveTowardsTarget();
		}
	}
}
