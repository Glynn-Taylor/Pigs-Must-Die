using UnityEngine;
using System.Collections;

public class RotateOnClick : MonoBehaviour {
	public float Rotation= 90;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown ()
	{
		Camera.main.transform.Rotate (new Vector3(0,Rotation,0));
	}
}
