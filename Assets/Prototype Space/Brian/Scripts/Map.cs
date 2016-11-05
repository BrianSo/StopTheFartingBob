using UnityEngine;
using System.Collections.Generic;

public class Map{

    public struct Block{
		public int tileNumber;
		public int objNumber;
	}

    public Block[,] blocks;
    public List<Vector2> startingPoints;
    public List<Vector2> itemPositions;
    public int width;
    public int height; 
    public Map(int width, int height){
        this.width = width;
        this.height = height;
        blocks = new Block[width,height];
        startingPoints = new List<Vector2>();
        itemPositions = new List<Vector2>();
    }

    private int roundRobinIndex = 0;
    public Vector2 GetRoundRobinStartingPoint(){
        if(roundRobinIndex > startingPoints.Count)
            roundRobinIndex = 0;
        return startingPoints[roundRobinIndex++];
    }
    public Vector2 GetRandomItemPosition(){
        return itemPositions[Random.Range(0,itemPositions.Count - 1)];
    }
}