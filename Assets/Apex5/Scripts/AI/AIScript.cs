using UnityEngine;
using System.Collections;

public abstract class AIScript : MonoBehaviour {
	//Target of the AI, e.g player
	protected GameObject Target;
	//Animation to default to when not performing an action
	protected string AnimationDefaultTo = "walk";
	//Whether or not are currently animating the default animation
	protected bool nonDefaultAnim=false;

}
