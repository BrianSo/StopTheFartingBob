using UnityEngine;
using System.Collections;

public class Bushes : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public Sprite normal;
	public Sprite stepped;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject == BaseCharactor.localCharactor.gameObject){
			// change apperance locally
			spriteRenderer.sprite = stepped;
		}
	}
	void OnTriggerExit(Collider other) {
        if(other.gameObject == BaseCharactor.localCharactor.gameObject){
			// change apperance locally
			spriteRenderer.sprite = normal;
		}
    }
}
