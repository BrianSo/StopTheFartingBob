using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ItemsPool : MonoBehaviour {

	public static ItemsPool singleton;

	public GameObject[] itemPrefabs;
	public GameObject[] rareItemPrefabs;

	public float rareItemProbability = 0.1f;

	public static GameObject GetRandomItemPrefab(){
		return singleton._GetRandomItemPrefab();
	}

	private GameObject _GetRandomItemPrefab(){
		if(rareItemPrefabs.Length > 0 && Random.Range(0,1f) < rareItemProbability)
			return rareItemPrefabs[Random.Range(0,rareItemPrefabs.Length)];
		return itemPrefabs[Random.Range(0,itemPrefabs.Length)];
	}

	// Use this for initialization
	void Start(){
		foreach(var p in itemPrefabs){
			ClientScene.RegisterPrefab(p);
			ThrowableItem item = p.GetComponent<ThrowableItem>();
			if(item){
				ClientScene.RegisterPrefab(item.throwingItemPrefab);
			}
		}
		foreach(var p in rareItemPrefabs){
			ClientScene.RegisterPrefab(p);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Awake () {
		this.Singleton(ref singleton);
	}
	void Destroy(){
		this.RemoveSingleton(ref singleton);
	}
}
