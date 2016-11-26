using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameUIControl : MonoBehaviour {

	public static InGameUIControl singleton;

	public RectTransform indexIndicator;
	public RectTransform indexBar;
	public Text indexPercentage;

	public Image itemSlotDisplay;
	private float indicatorXOffset;
	private float indexBarHeight;


	// Use this for initialization
	void Start () {
	
	}

	public void ShowUI(){
		gameObject.SetActive(true);
	}
	public void HideUI(){
		gameObject.SetActive(false);
	}
	public void PickUpItem(Sprite itemSprite){
		itemSlotDisplay.sprite = itemSprite;
		itemSlotDisplay.enabled = true;
	}

	public void RemoveItem(){
		itemSlotDisplay.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Game.singleton && Game.singleton.isGameStarted){

			//Update index
			indexIndicator.localPosition = new Vector3(indicatorXOffset,Game.singleton.pollutionIndex*0.01f*indexBarHeight - indexBarHeight*0.5f,0);
			indexPercentage.text = Mathf.Round (Game.singleton.pollutionIndex) + "%";
		}
	}

	void Awake(){
		this.Singleton(ref singleton);
		indexBarHeight = indexBar.rect.height;
		indicatorXOffset = indexBar.rect.width / 2;
		HideUI();
	}
	void Destroy(){
		this.RemoveSingleton(ref singleton);
	}
}
