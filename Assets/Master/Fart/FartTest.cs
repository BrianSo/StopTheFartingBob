using UnityEngine;
using System.Collections;

public class FartTest : MonoBehaviour {

	public GameObject fartPrefab;

	// Use this for initialization
	void Start () {
		StartCoroutine(Fart());
	}
	
	IEnumerator Fart(){
		while(true){
			
			var fartObj = Instantiate(fartPrefab, transform.position, Quaternion.identity) as GameObject;









			yield return new WaitForSeconds(1f);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
