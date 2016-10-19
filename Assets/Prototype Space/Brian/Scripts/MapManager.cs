using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

	public static MapManager singleton;

	public float tileSize = 32f;

	public int width = 20;
    public int height = 20;

	public MapGenerator generator;

	public string seed;
    public bool useRandomSeed;

	public GameObject map;
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
		GenerateMap();
	}
	
	public void GenerateMap(){
		if (useRandomSeed) {
            seed = Time.time.ToString();
        }

		if(map){
			Destroy(map);
		}
		generator.GenerateMap(width, height, seed.GetHashCode());
		map = generator.ConstructMapObjects(out mapTiles);
	}
}
