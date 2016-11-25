using UnityEngine;
using System.Collections;

public class StartingPointStyler : MapGenerator.MapGenerationMiddleware {
    protected override void Process(){
        
        // This is not necessary true, but just a fast code
        map.blocks[2,2].tileNumber = MapGenerator.TILE_GLASS;
        map.blocks[map.width - 3,2].tileNumber = MapGenerator.TILE_GLASS;
        map.blocks[2,map.height - 3].tileNumber = MapGenerator.TILE_GLASS;
        map.blocks[map.width - 3,map.height - 3].tileNumber = MapGenerator.TILE_GLASS;

        Vector2[] points = new Vector2[]{
            new Vector2(2,2),
            new Vector2(map.width - 3,2),
            new Vector2(2,map.height - 3),
            new Vector2(map.width - 3,map.height - 3)
        };
        Shuffle(random, points);
        foreach(var pt in points){
            map.startingPoints.Add(pt);
        } 
    }

    void Shuffle (System.Random rng, Vector2[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = rng.Next(n--);
            Vector2 temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
