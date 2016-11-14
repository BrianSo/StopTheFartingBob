using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// The Base Class that all charactor will be inherited
public class BaseCharactor : NetworkUnit {

	public static BaseCharactor localCharactor;

	public Animator anim;
	public float movementSpeed = 5f;


	private float raycastLength = 1000f;
	private Rigidbody rb;
	Vector3 mousePos;
	Camera viewCamera;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		viewCamera = Camera.main;
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	protected override void OnPlayerChanged(){
		if(isOwnByLocalPlayer){
			this.Singleton(ref localCharactor);
		}
	}

	void Destroy(){
		this.RemoveSingleton(ref localCharactor);
	}
	
	// Update is called once per frame
	void Update () {
		if(isOwnByLocalPlayer){
			mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
			transform.LookAt (mousePos + Vector3.up * transform.position.y);
		}
	}

	void FixedUpdate(){
		if(isOwnByLocalPlayer){
			HandleControl();	
		}
	}

	void HandleControl(){
		var x = Input.GetAxisRaw("Horizontal");
		var y = Input.GetAxisRaw("Vertical");

		viewCamera.transform.position = 0.3f*mousePos + 0.7f*transform.position;
		viewCamera.transform.position = new Vector3(viewCamera.transform.position.x, 10, viewCamera.transform.position.z);

		rb.velocity = new Vector3(x,0,y) * movementSpeed;

		// For Sprite Animator
		bool isWalking = (Mathf.Abs (x) + Mathf.Abs (y)) > 0;
		Vector3 lookDirection = (mousePos - transform.position).normalized;
		anim.SetBool ("isWalking", isWalking);
		anim.SetFloat ("x", lookDirection.x);
		anim.SetFloat ("y", lookDirection.z);
	}
}
