using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Controls animation for player.
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class MecanimCharControl : MonoBehaviour {
	//Animation reference
	private Animator anim;
	public CharacterController controller;
	
	// Initialization
	void Start () {
		anim = GetComponent<Animator>();
		//Set jumping layer to have more weight than moving
		anim.SetLayerWeight(1, 1);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//Set animation state to moving on key press
		if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D)){
			anim.SetBool("Moving",true);
		}else{
			anim.SetBool("Moving",false);
		}
		//Set jumping dependent on if on ground or not
		anim.SetBool("Jumping",controller.isGrounded);
	}
}