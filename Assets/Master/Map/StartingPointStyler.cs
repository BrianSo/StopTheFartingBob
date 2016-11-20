using UnityEngine;
using System.Collections;

public class StartingPointStyler : MapGenerator.MapGenerationMiddleware {
    protected override void Process(){
        
        // This is not necessary true, but just a fast code 
        map.startingPoints.Add(new Vector2(2,2));
        map.startingPoints.Add(new Vector2(map.width - 3,2));
        map.startingPoints.Add(new Vector2(2,map.height - 3));
        map.startingPoints.Add(new Vector2(map.width - 3,map.height - 3));
    }
}
