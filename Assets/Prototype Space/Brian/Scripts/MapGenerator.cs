using UnityEngine;
using System.Collections.Generic;

public abstract class MapGenerator: MonoBehaviour{

	public const int TILE_NOTHING = 0;
	public const int TILE_GLASS = 1;
	public const int TILE_WALL_TREE = 0x10;
	public const int TILE_WALL_FENCE = 0x20;

	public const int OBJ_NOTHING = 0x0;
	public const int OBJ_BUSHES = 0x1;
	public const int OBJ_PLANTS = 0x2;

	public struct Block{
		public int tileNumber;
		public int objNumber;
	}
	protected Block[,] map;
	protected System.Random random;

	public bool drawGizmo = false;

	public Block[,] GenerateMap(int width, int height, int seed){
		random = new System.Random(seed);
		map = new Block[width,height];
		// for(int x = 0; x < 20; x++){
		// 	for(int y = 0; y <20; y++){
		// 		if(x == 0 || y == 0 || x ==19 || y ==19){
		// 			map[x,y] = new Block();
		// 		}
		// 	}
		// }
		middlewares = new List<MapGenerationMiddleware>();
		Styling();
		return map;
	}

	public abstract void Styling();

	private List<MapGenerationMiddleware> middlewares;

	public void Use(MapGenerationMiddleware middleware){
		middlewares.Add(middleware);
		middleware.Process(map, random);
	}

	public abstract class MapGenerationMiddleware{
		protected Block[,] map;
		protected int width;
		protected int height;
		protected System.Random random;
		public void Process(Block[,] map,System.Random random){
			width = map.GetLength(0);
			height = map.GetLength(1);
			this.map = map; this.random = random; Process();
		}
		protected abstract void Process();
		public virtual void OnDrawGizmos(){}
	}

	void OnDrawGizmos() {
		if(drawGizmo && map != null){
			int width = map.GetLength(0);
			int height = map.GetLength(1);
			for (int x = 0; x < width; x ++) {
                for (int y = 0; y < height; y ++) {
					switch(map[x,y].tileNumber){
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

                    Vector3 pos = new Vector3(-width/2 + x + .5f, -height/2 + y+.5f,0);
                    Gizmos.DrawCube(pos,Vector3.one);

					switch(map[x,y].objNumber){
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

					if(map[x,y].objNumber != OBJ_NOTHING){
						pos.z = -0.5f;
                    	Gizmos.DrawCube(pos,Vector3.one);
					}
                    
                }
            }

			foreach(MapGenerationMiddleware middleware in middlewares){
				middleware.OnDrawGizmos();
			}
		}
    }
}
