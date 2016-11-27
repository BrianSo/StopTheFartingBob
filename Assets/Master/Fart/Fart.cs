using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Fart : NetworkBehaviour {

	public Sprite[] fartStyles;
	public SpriteRenderer spriteRenderer;

	public Rigidbody rb;

	public Material spriteMaterial;

	bool isAte = false;

	void Awake(){
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start () {

		// transform.localScale = Vector3.zero;
		// rb.AddForce(new Vector3(1f,0,0), ForceMode.Impulse);
		RpcSetup(new System.Random());
		if(BaseCharactor.localCharactor.gameObject == GameManager.singleton.bob){
			spriteRenderer.material = spriteMaterial;
		}
	}
	
	//[ClientRpc]
	public void RpcSetup(System.Random rand){
		spriteRenderer.sprite = fartStyles[rand.Next(0,fartStyles.Length)];
		transform.rotation = Quaternion.AngleAxis((float)rand.NextDouble() * 360f, Vector3.up);
		StartCoroutine(FartAnimation());
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

	void OnTriggerEnter(Collider other){
		if(isAte)
			return;
		Gardener gardener = other.GetComponent<Gardener>();
		if(gardener != null){
			gardener.EatFart();
			isAte = true;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
