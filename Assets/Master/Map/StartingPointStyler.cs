using UnityEngine;
using System.Collections;

public class StartingPointStyler : MapGenerator.MapGenerationMiddleware {
    protected override void Process(){
        
        // This is not necessary true, but just a fast code
        map.blocks[2,2].tileNumber = MapGenerator.TILE_GLASS;
        map.blocks[map.width - 3,2].tileNumber = MapGenerator.TILE_GLASS;
        map.blocks[2,map.height - 3].tileNumber = MapGenerator.TILE_GLASS;
        map.blocks[map.width - 3,map.height - 3].tileNumber = MapGenerator.TILE_GLASS;
        map.startingPoints.Add(new Vector2(2,2));
        map.startingPoints.Add(new Vector2(map.width - 3,2));
        map.startingPoints.Add(new Vector2(2,map.height - 3));
        map.startingPoints.Add(new Vector2(map.width - 3,map.height - 3));
    }
}
