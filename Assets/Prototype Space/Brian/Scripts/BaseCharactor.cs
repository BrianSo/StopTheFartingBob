using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// The Base Class that all charactor will be inherited
public class BaseCharactor : NetworkBehaviour {

	public float movementSpeed = 5f;


	private float raycastLength = 1000f;

	// Use this for initialization
	void Start () {
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if(isLocalPlayer){
			HandleControl();	
		}
	}

	void HandleControl(){
		var x = Input.GetAxis("Horizontal");
		var y = Input.GetAxis("Vertical");

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, raycastLength)){
			Debug.Log(hit.collider.name);
			Debug.Log(hit.point);
			//Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.green);
			Debug.DrawLine(transform.position,hit.point,Color.cyan,0.01f);
		}else{
			//Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.green);
		}
		this.transform.position += new Vector3(x,y,0) * Time.deltaTime * movementSpeed;
	}
}
