using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public GameObject[] variation;

	public virtual GameObject GetVariation(System.Random random){
		return variation[random.Next(0,variation.Length-1)];
	}
}
