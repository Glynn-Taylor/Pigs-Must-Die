using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Keeps an object a fixed distance away from another object
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class ObservePosition : MonoBehaviour {
	//Object to observe
	public Transform Observing;
	//Initial difference
	private Vector3 ObservationDifference;
	
	//Initialization
	void Start () {
		//Initial set up of difference
		ObservationDifference=transform.position-Observing.position;
		if(transform.position.z<Observing.position.z)
			ObservationDifference.z*=-1;
		if(transform.position.x<Observing.position.x)
			ObservationDifference.x*=-1;
	}
	// Update is called once per frame
	void FixedUpdate () {
		//Set position relative to observed object
		transform.position=Observing.position+ObservationDifference;
	}
}
