using UnityEngine;
using System.Collections;

public class ItemUsedHint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.one * 0.6f;
		StartCoroutine(ItemUsedAnimation());
	}


	IEnumerator ItemUsedAnimation(){
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		Color c = new Color(1,1,1,1);
		while(c.a > 0){
			spriteRenderer.color = c;
			transform.localScale = transform.localScale + (Vector3.one * (Time.deltaTime));
			c.a -= Time.deltaTime * 1.5f; // 2/3 second
			yield return null;
		}

		Destroy(gameObject);
		yield return null;
	}
}
