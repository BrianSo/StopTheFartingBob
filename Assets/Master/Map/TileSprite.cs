using UnityEngine;

public class TileSprite : Tile{

    public Sprite[] sprites;

    public override GameObject GetVariation(System.Random random){
        GameObject gameObject = Instantiate(variation[random.Next(0,variation.Length-1)]);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[random.Next(0,sprites.Length)];
        return gameObject;
    }
}