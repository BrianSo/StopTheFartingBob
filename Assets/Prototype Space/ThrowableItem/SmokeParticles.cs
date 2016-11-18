using UnityEngine;
using System.Collections;

public class SmokeParticles : MonoBehaviour {

	public Rigidbody rb;
	public SpriteRenderer spriteRenderer;

	public float timeToLeave = 1f;
	public float leaveTime = 1f;

	// Use this for initialization
	void Start () {		
		StartCoroutine(Particle());
	}
	
	public void Setup(System.Random rand, Vector3 force, float initialAlpha){
		transform.rotation = Quaternion.Euler(0,(float)rand.NextDouble() * 360f - 5f,0);
		rb.angularVelocity = new Vector3(0, (float)rand.NextDouble() * 0.1f - 0.05f,0);
		rb.AddForce(force ,ForceMode.Impulse);

		spriteRenderer.color = new Color(1,1,1,initialAlpha);

		timeToLeave = (float)rand.NextDouble() * 2 + 2 - initialAlpha;
		leaveTime = (float)rand.NextDouble() * 2 + 5;
	}

	IEnumerator Particle(){
		yield return new WaitForSeconds(timeToLeave);
		Color c = spriteRenderer.color;

		while(c.a < 0.99f){
			c.a += Time.deltaTime;
			spriteRenderer.color = c;
			yield return null;
		}

		float _leaveTime = leaveTime;
		while(_leaveTime > 0){
			_leaveTime -= Time.deltaTime;
			c.a = _leaveTime / leaveTime;
			spriteRenderer.color = c;
			if(c.a < 0.4f){
				//disable vision blocking
			}
			yield return null;
		}
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {

	}
}
