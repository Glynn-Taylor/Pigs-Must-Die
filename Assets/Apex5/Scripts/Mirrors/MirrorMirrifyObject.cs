using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: A prototype mirror script (not yet finished, works under certain situations)
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
[ExecuteInEditMode]
public class MirrorMirrifyObject : MonoBehaviour
{
	public GameObject objectBeforeMirror;
	public GameObject mirrorPlane;
	public bool TrackX;
	public bool TrackZ;
	
	//Called every frame
	void Update ()
	{
		//Check correct objects have been set/not destroyed
		if (null != mirrorPlane) {
			if (null != objectBeforeMirror) {
				//Initially sync transforms
				transform.position = objectBeforeMirror.transform.position;
				transform.rotation = objectBeforeMirror.transform.rotation;
				//Get inverse position across mirrorplane
				Vector3 positionInMirrorSpace = mirrorPlane.transform.InverseTransformPoint( objectBeforeMirror.transform.position );
				positionInMirrorSpace.y = -positionInMirrorSpace.y;
				transform.position = mirrorPlane.transform.TransformPoint( positionInMirrorSpace );
				Vector3 mirrorsNormal = mirrorPlane.transform.localRotation * new Vector3( 0f, 1, 0f ); // Unity planes always start with normal pointing up
				
				Plane planeOfMirror = new Plane(  mirrorsNormal, mirrorPlane.transform.position );
				float intersectionDistance;
				Ray rayToMirror = new Ray( objectBeforeMirror.transform.position, objectBeforeMirror.transform.forward );
				planeOfMirror.Raycast( rayToMirror, out intersectionDistance );
				Vector3 hitPoint = rayToMirror.GetPoint( intersectionDistance );
				transform.LookAt( hitPoint );
				transform.eulerAngles = new Vector3(transform.eulerAngles.x,180+objectBeforeMirror.transform.eulerAngles.y,transform.eulerAngles.z);
				if(TrackZ)
					transform.position = new Vector3(transform.position.x,transform.position.y,mirrorPlane.transform.position.z-(objectBeforeMirror.transform.position.z-mirrorPlane.transform.position.z));
			}
		}
	}
}
