using UnityEngine;
using System.Collections;
public class BlockLinearStyler : BlockStyler{

    public BlockLinearStyler(BlockStyle style) : base(style){}

    protected override int[] FindSpace(int dw, int dh, int try_time){
        int[] pos = new int[]{1,1};
        
        bool moved = true;
        while(moved){
            moved = MoveRight(pos, dw, dh);
            if(moved)
                continue;
            moved = MoveDown(pos, dw, dh);
        }

        int x = pos[0];
        int y = pos[1];

        //collision checked
        if(x + dw >= map.width){
            return null;
        }
        if(y + dh >= map.height){
            return null;
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
            return pos;
        }
        return null;
    }

    bool MoveDown(int[] pos, int dw, int dh){
        int x = pos[0];
        int y = pos[1];

        bool ok = true;
        while(ok){
            ok = true; 
            y = y + 1;
            if(y + dh >= map.height){
                break;
            }
            for(int cx = x; ok && cx < x + dw;cx++){
                ok = !map.blocks[cx,y + dh].tilePlaced;
            }
        }
        y = y - 1;
        if(y == pos[1]){
            return false;
        }
        pos[1] = y;
        Debug.Log("Moved y to " + y);
        return true;
    }

    bool MoveRight(int[] pos, int dw, int dh){
        int x = pos[0];
        int y = pos[1];

        bool ok = true;
        while(ok){
            ok = true; 
            x = x + 1;
            if(x + dw >= map.width){
                break;
            }
            for(int cy = y; ok && cy < y + dh;cy++){
                ok = !map.blocks[x + dw,cy].tilePlaced;
            }
        }
        x = x - 1;
        if(x == pos[0]){
            return false;
        }
        pos[0] = x;
        Debug.Log("Moved x to " + x);
        return true;
    }
}