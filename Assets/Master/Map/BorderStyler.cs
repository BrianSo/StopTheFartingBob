using UnityEngine;
using System.Collections;

public class BorderStyler : MapGenerator.MapGenerationMiddleware {
    protected override void Process(){
        for(int x =1; x < map.width-1; x++){
            for(int y = 1; y < map.height-1; y++){
                if(!map.blocks[map.width-1,y].tilePlaced)
                    map.blocks[x,y].tileNumber = MapGenerator.TILE_GLASS;
            }
        }
        for(int x = 0; x < map.width;x++){
            map.blocks[x,0].tileNumber = MapGenerator.TILE_WALL_TREE;
            map.blocks[x,0].tilePlaced = true;
            map.blocks[x,map.height-1].tileNumber = MapGenerator.TILE_WALL_TREE;
            map.blocks[x,map.height-1].tilePlaced = true;
        }
        for(int y = 0; y < map.height;y++){
            map.blocks[0,y].tileNumber = MapGenerator.TILE_WALL_TREE;
            map.blocks[0,y].tilePlaced = true;
            map.blocks[map.width-1,y].tileNumber = MapGenerator.TILE_WALL_TREE;
            map.blocks[map.width-1,y].tilePlaced = true;
        }
    }
}
