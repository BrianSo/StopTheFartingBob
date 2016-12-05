using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour {

	public GameObject baseCharacter;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(
			90,
			-baseCharacter.transform.rotation.eulerAngles.z,
			0);
	}
}
