using UnityEngine;
using System.Collections;

public class RadarEffect : MonoBehaviour {

	Mesh viewMesh;

	void Start(){
		//StartCoroutine(Scan(gameObject));
	}

	public void Setup(GameObject owner){
		StartCoroutine(Scan(owner));
	}
	IEnumerator Scan(GameObject owner){
		viewMesh = new Mesh ();
		viewMesh.name = "Radar Mesh";
		MeshFilter filter = GetComponent<MeshFilter>();
		filter.mesh = viewMesh;

		float degree = 0;
		while(degree < 360f){
			degree += Time.deltaTime * 360f;
			DrawMesh(degree, owner.transform.position);
			yield return true;
		}
		yield return new WaitForSeconds(3f);

		float trigger = 2.5f;
		while(trigger > 0){
			filter.mesh = filter.mesh == null ? viewMesh : null;
			yield return new WaitForSeconds(.5f);
			trigger -= .5f;
		}

		Destroy(gameObject);
	}

	void DrawMesh(float degree, Vector3 origin){
		int vertexCount = (int)degree / 10 + 2;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices [0] = origin;
		var up = new Vector3(0,0,10);
		for (int i = 0; i < vertexCount - 2; i++) {
			vertices [i + 1] = origin + Quaternion.AngleAxis(10f * i, Vector3.up) * up; 

			triangles [i * 3] = 0;
			triangles [i * 3 + 1] = i + 1;
			triangles [i * 3 + 2] = i + 2;
		}
		vertices[vertexCount - 1] = origin + Quaternion.AngleAxis(degree, Vector3.up) * up; 
		viewMesh.Clear ();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
	}
}
