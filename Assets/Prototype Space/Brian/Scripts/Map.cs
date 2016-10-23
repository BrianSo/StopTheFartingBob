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

    public Vector2 GetRandomStartingPoint(){
        int index = Random.Range(0,startingPoints.Count - 1);
        Vector2 v = startingPoints[index];
        startingPoints.RemoveAt(index);
        return v;
    }
    public Vector2 GetRandomItemPosition(){
        return itemPositions[Random.Range(0,itemPositions.Count - 1)];
    }
}