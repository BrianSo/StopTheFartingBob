using UnityEngine;
using System.Collections;

public class BlockStyler : MapGenerator.MapGenerationMiddleware {

    BlockStyle style;
    public BlockStyler(BlockStyle style){
        this.style = style;
    }

    protected override void Process(){
        if(random.Next(0,2) == 0){
            this.style = this.style.Flip();
        }
        this.style = this.style.Rotate(random.Next(0,4));

        int[] place = FindSpace(style.width, style.height, 5);
        if(place == null){
            Debug.Log("NO SPACE");
            return;
        }
            
        for(int x = 0; x < style.width; x++ ){
            for(int y = 0; y < style.height; y++){
                var block = map.blocks[place[0] + x,place[1] + y];
                int tileObj = style.tileObjMap[x,y];
                if(tileObj >= BlockStyle.OBJ_THRESHOLD){
                    tileObj -= BlockStyle.OBJ_THRESHOLD;
                    if(tileObj != MapGenerator.OBJ_NOTHING){
                        block.tileNumber = MapGenerator.TILE_GLASS;
                        block.objNumber = tileObj;
                        block.objPlaced = true;
                    }
                }else if(tileObj != MapGenerator.TILE_NOTHING){
                    block.tileNumber = tileObj;
                }
                block.tilePlaced = style.blockMap[x,y] == 1;
                map.blocks[place[0] + x,place[1] + y] = block;
                if(style.itemMap[x,y] != 0){
                    map.itemPositions.Add(new Vector2(place[0] + x,place[1] + y));
                }
            }
        }
    }

    protected virtual int[] FindSpace(int dw, int dh, int try_time){
        int x = 0;
        int y = 0;

        while(try_time > 0)
        {
            x = random.Next(0,map.width);
            y = random.Next(0,map.height);
            if(x + dw >= map.width){
                continue;
            }
            if(y + dh >= map.height){
                continue;
            }
            bool ok = true;
            for(int dx = x; ok && dx < x + dw; dx++){
                if(dx >= map.width){
                    ok = false;
                    break;
                }
                for(int dy = y; ok && dy < y+ dh; dy++){
                    if(dy >= map.height){
                        ok = false;
                        break;
                    }
                    ok = !map.blocks[dx,dy].tilePlaced;
                    
                }
            }
            if(ok){
                return new int[]{x,y};
            }
            try_time--;
        }

        return null;
    }
}
