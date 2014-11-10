using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Splits a mesh up into physics enabled faces, skipping a defined amount for performance, and also spawns an effect for a cool death effect
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public class DeathTest : MonoBehaviour
{
	//Prefabs to spawn on death
	private string[] DeathTypes = {
		"Deathball",
		"Dustball"
	};
	//Type of prefab to spawn
	private string DType="Deathball";
	//Position to spawn at
	public Transform DeathPosition;
	//Amount of faces to skip when splitting mesh
	public int SkipEvery;
	//Max amount of time for a fragmented face to exist (MaxTime+1)
	float MaxTime=1;
	
	//Initialisation
	void Start ()
	{
		DeathPosition=transform.parent;
		//Perform mesh split
		SplitMesh();
	}
	//Set some options for splitting
	public void setInitialConditions (int skipFacesEvery, float maxFaceTime, int death)
	{
		SkipEvery=skipFacesEvery;
		MaxTime=maxFaceTime;
		DType=DeathTypes[death];	
	}
	//Perform the mesh split
	void SplitMesh ()
	{
		//Flag for checking if skinned mesh
		bool Skinned=false;
		//References for splitting
		MeshFilter MF=null;
		MeshRenderer MR=null;
		//If is normal mesh renderer get references
		if(GetComponent<MeshRenderer>()){
			MF = GetComponent<MeshFilter> ();
		 	MR = GetComponent<MeshRenderer> ();
		}else if(GetComponent<SkinnedMeshRenderer>()){
			Skinned=true;
		}
		//Get mesh dependent on if skinned or not
		Mesh M = Skinned?GetComponent<SkinnedMeshRenderer>().sharedMesh:MF.mesh;
		//Get mesh data
		Vector3[] verts = M.vertices;
		Vector3[] normals = M.normals;
		Vector2[] uvs = M.uv;
		if(DeathPosition==null)
			DeathPosition=transform;
		//Disable collider
		if(collider)
			collider.enabled=false;
		//Create the death effect prefab
		GameObject db = (GameObject)GameObject.Instantiate (Resources.Load(DType),DeathPosition.position,transform.rotation);
		//Destroy in 3 seconds
		Destroy (db, 3);
		int c = 0;
		Vector3 scale = transform.lossyScale;
		//Perform mesh splitting
		for (int submesh = 0; submesh < M.subMeshCount; submesh++) {
			//Get all indices
			int[] indices = M.GetTriangles (submesh);
			//Loop every 3 indices (triangle)
			for (int i = 0; i < indices.Length; i += 3) {
				c++;
				//Check skipping optimisation condition
				if (c % SkipEvery == 0) {
					//Data for new mesh of one triangle
					Vector3[] newVerts = new Vector3[3];
					Vector3[] newNormals = new Vector3[3];
					Vector2[] newUvs = new Vector2[3];
					//Iterate over indices of triangle and assign data
					for (int n = 0; n < 3; n++) {
						int index = indices [i + n];
						newVerts [n] = verts [index];
						newVerts[n] = new Vector3(newVerts[n].x*scale.x,newVerts[n].y*scale.y,newVerts[n].z*scale.z);
						newUvs [n] = uvs [index];
						newNormals [n] = normals [index];
					}
					//Assign data to new mesh object
					Mesh mesh = new Mesh ();
					mesh.vertices = newVerts;
					mesh.normals = newNormals;
					mesh.uv = newUvs;
 					//Connect points in mesh
					mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };
 					//Create new gameobject for the mesh
					GameObject GO = new GameObject ("Triangle " + (i / 3));
					GO.transform.position = transform.parent.position+new Vector3(0,0,1.72f);
					GO.transform.rotation = transform.parent.rotation;
					Shader shader1 = Shader.Find("Diffuse");
					//Assign shader of this object to new object based on type
					if(Skinned){
						GetComponent<SkinnedMeshRenderer>().materials[submesh].shader=shader1;
					}else{
						MR.materials [submesh].shader=shader1;
					}
					//Add components to split face
					GO.AddComponent<MeshRenderer> ().material = Skinned?GetComponent<SkinnedMeshRenderer>().materials[submesh]:MR.materials [submesh];
					GO.AddComponent<MeshFilter> ().mesh = mesh;
					GO.AddComponent<BoxCollider> ();
					//Destroy fragmented face after a semi random time
					Destroy (GO, 1f + Random.Range (0.0f,MaxTime));
				}
			}
		}
		//Disable this objects mesh renderer as has been replaced by many fragments
		if(!Skinned){
			MR.enabled = false;
		}else{
			GetComponent<SkinnedMeshRenderer>().enabled=false;	
		}
		GameObject go = gameObject;
		//Check if need to destroy higher up
		while(go.tag!="Enemy"&go.transform.parent){
			go=go.transform.parent.gameObject;
		}
		//Destroy the original object
		Destroy (go);
	}
	
}
