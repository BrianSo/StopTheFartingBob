using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public abstract class ThrowingItem : NetworkBehaviour {

	Vector3 destination;
	public bool isThrowing;
	public GameObject thrower;

	public bool moveOverObstacle = false;

	private Rigidbody rb; 

	protected virtual void Awake(){
		rb = GetComponent <Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	protected IEnumerator ThrowOver ()
	{
		//Start rotation
		rb.angularVelocity = new Vector3(0,36,0); 

		//Move to destination
		while((transform.position - destination).magnitude > 0.01f && isThrowing)
		{
			var ve = (destination - rb.position);
			rb.velocity = ve + ve.normalized;
			yield return null;
		}

		yield return EndThrow();
	}
	protected IEnumerator ThrowWithForce()
	{
		//Start rotation
		rb.angularVelocity = new Vector3(0,36,0);

		//Move to destination
		var force = (destination - transform.position);
		rb.AddForce(force,ForceMode.Impulse);
		yield return new WaitForFixedUpdate();
		while(rb.velocity.magnitude > 0.4f){
			yield return null;
		}

		yield return EndThrow();
	}
	IEnumerator EndThrow(){
		isThrowing = false;

		OnItemArrived();

		//Stop rotation
		float rotation = rb.rotation.eulerAngles.y;
		if(rb.angularVelocity.y > 0){
			while(rotation < 355){
				rotation = rb.rotation.eulerAngles.y;
				float diff = 360 - rotation;
				rb.angularVelocity = new Vector3(0,1 + diff/20,0);
				yield return null;
			}
		}else{
			while(rotation > 5){
				rotation = rb.rotation.eulerAngles.y;
				float diff = 0 - rotation;
				rb.angularVelocity = new Vector3(0,-1 + diff/20,0);
				yield return null;
			}
		}
		rb.angularVelocity = Vector3.zero;
		rb.rotation = Quaternion.identity;
	}
	

	public abstract void OnItemArrived();

	[ClientRpc]
	public void RpcSetup(Vector3 destination, GameObject thrower){
		this.destination = destination;
		this.thrower = thrower;
		isThrowing = true;
		if(moveOverObstacle){
			StartCoroutine(ThrowOver());
		}else{
			StartCoroutine(ThrowWithForce());
		}
	}
}
