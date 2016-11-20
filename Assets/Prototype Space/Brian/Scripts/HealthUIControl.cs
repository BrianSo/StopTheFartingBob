using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUIControl : MonoBehaviour {

	public Image[] healthImage;

	public void SetHealth(int health){
		Color yes = new Color(1,1,1,1);
		Color no = new Color(0.3f,0.3f,0.3f,1);
		for(int i = 0;i < healthImage.Length; i++){
			healthImage[i].color = i < health ? yes : no; 
		}
	}

	public void ShowHealthImages(){
		Debug.Log("HAHAHHAHAHAHHAHHHA");
		foreach(Image img in healthImage){
			img.material = null;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
