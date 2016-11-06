﻿using UnityEngine;
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


	public Vector3 GetStartingPosition(){
		Vector2 pt = sourceMap.GetRoundRobinStartingPoint();
		var width = sourceMap.width;
		var height = sourceMap.height;
		Vector3 positionFix = new Vector3(-width/2 + .5f,0,-height/2 + .5f);
		return new Vector3(pt.x, 0, pt.y) + positionFix;
	}

	public Vector3 GetItemPosition(){
		Vector2 pt = sourceMap.GetRandomItemPosition();
		var width = sourceMap.width;
		var height = sourceMap.height;
		Vector3 positionFix = new Vector3(-width/2 + .5f,0,-height/2 + .5f);
		return new Vector3(pt.x, 0, pt.y) + positionFix;
	}

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
		DestroyMap();
		sourceMap = generator.GenerateMap(width, height, seed.GetHashCode());
		map = generator.ConstructMapObjects(out mapTiles, out units);
	}
	public void DestroyMap(){
		generator.Release();
		if(map != null){
			Destroy(map);
		}
		if(units != null)
			foreach(GameObject gameObject in units){
				Destroy(gameObject);
			}
	}
}
