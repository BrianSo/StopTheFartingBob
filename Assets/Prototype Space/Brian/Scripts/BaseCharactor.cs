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

	void OnPlayerChanged(){
		if(isOwnByLocalPlayer){
			this.Singleton(ref localCharactor);
		}
	}

	void Destroy(){
		EventOnPlayerChanged -= OnPlayerChanged;
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


		// Moving Camera by pixel
		Vector3 newPos = 0.3f*mousePos + 0.7f*transform.position;
		Vector3 roundPos = new Vector3(RoundToNearestPixel(newPos.x, viewCamera), 10, RoundToNearestPixel(newPos.z, viewCamera));
		viewCamera.transform.position = roundPos;

		rb.velocity = new Vector3(x,0,y) * movementSpeed;

		// For Sprite Animator
		bool isWalking = (Mathf.Abs (x) + Mathf.Abs (y)) > 0;
		Vector3 lookDirection = (mousePos - transform.position).normalized;
		anim.SetBool ("isWalking", isWalking);
		anim.SetFloat ("x", lookDirection.x);
		anim.SetFloat ("y", lookDirection.z);
	}

	// For Pixel Perfect Camera Movement
	public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
	{
		float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
		return adjustedUnityUnits;
	}

	void Awake(){
		EventOnPlayerChanged += OnPlayerChanged;
	}
}
