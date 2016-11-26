using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFart : MonoBehaviour {

	public Image spriteRenderer;

	// Use this for initialization
	void Start () {
		System.Random rand = new System.Random();
		transform.rotation = Quaternion.Euler(0,0,(float)rand.NextDouble() * 360f);
		StartCoroutine(FartAnimation());
		GetComponent<Rigidbody>().AddForce(new Vector3(Screen.width * 0.2f,0,0), ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator FartAnimation(){
		float scale = 0f;
		Color c = new Vector4(1,1,1,0);
		//grow
		while(scale < 0.99f){
			scale += Time.deltaTime * 2;//0.5s
			c.a = scale;
			transform.localScale = new Vector3(scale, scale, scale);
			spriteRenderer.color = c;
			yield return null;
		}

		//leave
		while(c.a > 0.01f){
			scale -= Time.deltaTime * 0.5f;
			c.a = scale;
			spriteRenderer.color = c;
			yield return null;
		}
		Destroy(gameObject);


		yield return null;
	}
}
