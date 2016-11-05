using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

	public static MapManager singleton;

	public float tileSize = 32f;

	public int width = 20;
    public int height = 20;

	public MapGenerator generator;

    public string seed;

	public Map sourceMap;

	public GameObject map;
	public List<GameObject> units;
	public GameObject[,] mapTiles;

	void Awake(){
		//singleton pattern
		if (singleton == null)
			singleton = this;    
		else if (singleton != this)
			Destroy(this);
	}
	void Destroy(){
		//singleton pattern
		if(this == singleton)
			singleton = null;
	}

	// Use this for initialization
	void Start () {
		
	}
	public string getRandomSeed(){
		return Time.time.ToString();
	}
	public void GenerateMap(){
		if(map){
			Destroy(map);
		}
		if(units != null)
		foreach(GameObject gameObject in units){
			Destroy(gameObject);
		}
		generator.GenerateMap(width, height, seed.GetHashCode());
		map = generator.ConstructMapObjects(out mapTiles, out units);
	}
	public void DestroyMap(){
		if(map){
			Destroy(map);
		}
	}
}
