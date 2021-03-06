﻿using UnityEngine;
using System.Collections.Generic;

public abstract class MapGenerator: MonoBehaviour{

	#region setting
	public Tile[] tiles;

	public const int TILE_NOTHING = 0;
	public const int TILE_GLASS = 1;
	public const int TILE_WALL_TREE = 2;
	public const int TILE_WALL_FENCE = 3;

	public GameObject[] gameObjects;

	public const int OBJ_NOTHING = 0;
	public const int OBJ_BUSHES = 1;
	public const int OBJ_PLANTS = 2;
	#endregion

	protected Map map;
	protected System.Random random;

	public bool drawGizmo = false;
	public bool drawSpaceGizmo = false;
	
	void Start(){
		middlewares = new List<MapGenerationMiddleware>();
	}
	// create and generate a new map in data structure, by the MapGenerationMiddleware used.
	// setup the middleware in Styling();
	public Map GenerateMap(int width, int height, int seed){
		random = new System.Random(seed);
		map = new Map(width,height);
		middlewares.Clear();
		Styling();
		return map;
	}
	public virtual void Release(){
		map = null;
		middlewares.Clear();
	}
	
	// create the gameobjects according to the map generated
	public GameObject ConstructMapObjects(out GameObject[,] mapGameObjects, out List<GameObject> unitGameObjects){
		int width = map.width;
		int height = map.height;

		mapGameObjects = new GameObject[width,height];
		GameObject mapGameObject = new GameObject("Map");
		unitGameObjects = new List<GameObject>();

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if(map.blocks[x,y].tileNumber == TILE_NOTHING){
					continue;
				}
				GameObject generated = tiles[map.blocks[x,y].tileNumber - 1].GetVariation(random);
				generated.transform.position = new Vector3(x, 0, y);
				mapGameObjects[x,y] = generated;
				generated.transform.parent = mapGameObject.transform;
			}
		}
		Vector3 positionFix = new Vector3(-width/2 + .5f,0,-height/2 + .5f);
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if(map.blocks[x,y].objNumber == OBJ_NOTHING){
					continue;
				}
				GameObject generated = Instantiate(gameObjects[map.blocks[x,y].objNumber - 1]);
				generated.transform.position = new Vector3(x, 0, y) + positionFix;
				unitGameObjects.Add(generated);
			}
		}
		ConstructBushesColliders(positionFix, unitGameObjects);

		
		mapGameObject.transform.position = positionFix;
		return mapGameObject;
	}

	void ConstructBushesColliders(Vector3 positionFix, List<GameObject> unitGameObjects){
		int width = map.width;
		int height = map.height;

		bool[,] visited = new bool[width, height];
		for (int x = 0; x < width; x++)
			for(int y = 0; y < height; y++)
				visited[x,y] = (map.blocks[x,y].objNumber != OBJ_BUSHES);
		
		for (int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				if(visited[x,y])
					continue;
				unitGameObjects.Add(ConstructBushesCollider(x, y, visited, positionFix));
			}
		}
	}
	GameObject ConstructBushesCollider(int x, int y, bool[,] visited,Vector3 positionFix){
		int width = visited.GetLength(0);
		int height = visited.GetLength(1);

		int startX, endX;
		int startY, endY;
		startX = endX = x;
		startY = endY = y;
		while(++x < width && !visited[x,startY])
			endX = x;
		while(++y < height && !visited[startX,y])
			endY = y;

		for(x = startX; x <= endX; x++){
			for(y = startY; y <= endY; y++){
				visited[x,y] = true;
			}
		}

		var gameObject = new GameObject();
		gameObject.layer = LayerMask.NameToLayer("Obstacles");
		BoxCollider collider = gameObject.AddComponent<BoxCollider>();
		var size = new Vector3();
		size.x = endX - startX + 1;
		size.y = 1;
		size.z = endY - startY + 1;
		collider.size = size;
		collider.isTrigger = true;
		gameObject.transform.position = new Vector3((endX + startX)/2f, 0, (endY + startY)/2f) + positionFix;
		return gameObject;
	}

	public abstract void Styling();

	private List<MapGenerationMiddleware> middlewares;

	public void Use(MapGenerationMiddleware middleware){
		middlewares.Add(middleware);
		middleware.Process(map, random);
	}

	public abstract class MapGenerationMiddleware{
		protected Map map;
		protected System.Random random;
		public void Process(Map map,System.Random random){
			this.map = map; this.random = random; Process();
		}
		protected abstract void Process();
		public virtual void OnDrawGizmos(){}
	}

	void OnDrawGizmos() {
		if(drawGizmo && map != null){
			int width = map.width;
			int height = map.height;
			for (int x = 0; x < width; x ++) {
                for (int y = 0; y < height; y ++) {
					// draw tiles
					switch(map.blocks[x,y].tileNumber){
						case TILE_GLASS:
							Gizmos.color = Color.gray;
							break;
						case TILE_WALL_TREE:
							Gizmos.color = Color.green;
							break;
						case TILE_WALL_FENCE:
							Gizmos.color = Color.yellow;
							break;
						default:
							Gizmos.color = Color.black;
							break;
					}

                    Vector3 pos = new Vector3(-width/2 + x + .5f, 0,-height/2 + y+.5f);
                    Gizmos.DrawCube(pos,Vector3.one);

					// draw objects
					switch(map.blocks[x,y].objNumber){
						case OBJ_BUSHES:
							Gizmos.color = Color.cyan;
							break;
						case OBJ_PLANTS:
							Gizmos.color = Color.magenta;
							break;
						default:
							Gizmos.color = Color.black;
							break;
					}

					if(map.blocks[x,y].objNumber != OBJ_NOTHING){
						pos.z = -0.5f;
                    	Gizmos.DrawCube(pos,Vector3.one);
					}
                    
                }
            }

			foreach(MapGenerationMiddleware middleware in middlewares){
				middleware.OnDrawGizmos();
			}
		}
		if(drawSpaceGizmo && map != null){
			int width = map.width;
			int height = map.height;
			Gizmos.color = Color.blue;
			for (int x = 0; x < width; x ++) {
                for (int y = 0; y < height; y ++) {
					// draw space
					if(map.blocks[x,y].tilePlaced){
						Vector3 pos = new Vector3(-width/2 + x + .5f, 0,-height/2 + y+.5f);
						Gizmos.DrawCube(pos,Vector3.one);
					}
                }
            }
		}
    }
}
