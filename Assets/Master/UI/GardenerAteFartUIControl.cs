using UnityEngine;
using System.Collections;

public class GardenerAteFartUIControl : MonoBehaviour {

	public Gardener gardener;

	public RectTransform barTransform;

	const float MAX_WIDTH = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		barTransform.sizeDelta = new Vector2(gardener.numOfFartAte/Gardener.FART_STUNNING_THRESHOLD*MAX_WIDTH, barTransform.sizeDelta.y);
	}
}
