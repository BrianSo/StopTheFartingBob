using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SmokeBomb : ThrowingItem {

	public GameObject smokeBombPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnItemArrived(){
		Debug.Log("BOMB ARRIVED");
		StartCoroutine(bomb(new System.Random(Time.time.GetHashCode())));
		Destroy(gameObject, 5f);
	}

	IEnumerator bomb(System.Random rand){
		Debug.Log("bomb gogogo");
		yield return new WaitForEndOfFrame();
		for(float d = 0.1f; d > 0.01f; d-=0.04f){
			var forceVector = new Vector3(d,0,0);
			for(int i = 0; i < 10; i++){
				var dest = Quaternion.AngleAxis(i * 36f, Vector3.up) * forceVector;
				var smoke = Instantiate(smokeBombPrefab, transform.position, Quaternion.identity) as GameObject;
				SmokeParticles sp = smoke.GetComponent<SmokeParticles>();
				sp.Setup(rand, dest, 1);
			}
			yield return new WaitForFixedUpdate();
		}

		while(true){
			var forceVector = new Vector3((float)rand.NextDouble() * 0.03f + 0.04f,0,0);
			float i = (float)rand.NextDouble() * 360f;
			var dest = Quaternion.AngleAxis(i, Vector3.up) * forceVector;
			var smoke = Instantiate(smokeBombPrefab, transform.position, Quaternion.identity) as GameObject;
			SmokeParticles sp = smoke.GetComponent<SmokeParticles>();
			sp.Setup(rand, dest, 0);
			yield return new WaitForSeconds(0.5f);
		}
	}
}
