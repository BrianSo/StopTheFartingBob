using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

	public float tileSize = 32f;

	public int width = 20;
    public int height = 20;

	public MapGenerator generator;

	public string seed;
    public bool useRandomSeed;

	// Use this for initialization
	void Start () {
		GenerateMap();
	}
	
	public void GenerateMap(){
		if (useRandomSeed) {
            seed = Time.time.ToString();
        }

		generator.GenerateMap(width, height, seed.GetHashCode());
	}
}
