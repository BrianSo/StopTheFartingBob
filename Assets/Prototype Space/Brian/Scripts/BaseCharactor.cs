using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// The Base Class that all charactor will be inherited
public class BaseCharactor : NetworkBehaviour {

	public float movementSpeed = 5f;


	private float raycastLength = 1000f;
	private Rigidbody rb;
	Camera viewCamera;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		viewCamera = Camera.main;
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		transform.LookAt (mousePos + Vector3.up * transform.position.y);
	}

	void FixedUpdate(){
		if(isLocalPlayer){
			HandleControl();	
		}
	}

	void HandleControl(){
		var x = Input.GetAxis("Horizontal");
		var y = Input.GetAxis("Vertical");

		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Camera.main.transform.position.y));
		Debug.DrawLine(transform.position,pos,Color.cyan,0.01f);

		//this.transform.position += new Vector3(x,0,y) * Time.deltaTime * movementSpeed;
		//rb.MovePosition(transform.position + new Vector3(x,0,y) * Time.deltaTime * movementSpeed);
		//rb.AddForce(new Vector3(x,0,y) * Time.deltaTime * movementSpeed);
		rb.velocity = new Vector3(x,0,y) * Time.deltaTime * movementSpeed;
	}
}
