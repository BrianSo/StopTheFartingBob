using System;

public class MockGenerator : MapGenerator
{

    public bool useRandom = false;

    private class MockStyler : MapGenerationMiddleware
    {
        protected override void Process()
        {
            // asume width = 20, height = 20;

            int _, T, H, M, Q;

            _ = TILE_GLASS;
            T = TILE_WALL_TREE;
            H = TILE_WALL_FENCE;
            M = TILE_GLASS;
            Q = TILE_GLASS;
            int[,] tileMap = new int[,] {
                {T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},
                {T,_,_,_,_,_,M,_,_,_,_,M,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,M,_,_,_,_,_,T,_,Q,Q,Q,Q,_,T},
                {T,_,_,_,_,_,T,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,T,_,_,T,_,_,T,_,Q,Q,Q,Q,_,T},
                {T,M,M,T,T,T,T,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,_,Q,Q,Q,Q,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,M,_,_,M,M,T,T,T,T,_,_,T,H,H,_,_,H,H,T},
                {T,_,_,_,_,_,_,_,_,T,M,M,M,M,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,M,M,M,M,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,T,T,T,T,_,_,T},
                {T,_,T,_,_,_,T,_,_,T,_,_,T,_,_,_,M,_,_,T},
                {T,_,T,_,_,_,T,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,T,_,_,_,T,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,_,_,_,M,M,M,T},
                {T,_,Q,Q,Q,Q,Q,_,_,_,_,_,_,_,_,_,M,_,M,T},
                {T,M,_,_,_,_,_,_,_,_,_,_,_,_,_,_,M,M,M,T},
                {T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T}
            };
            _ = OBJ_NOTHING;
            T = OBJ_NOTHING;
            H = OBJ_NOTHING;
            M = OBJ_BUSHES;
            Q = OBJ_PLANTS;
            int[,] objMap = new int[,] {
                {T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T},
                {T,8,_,_,_,_,M,_,_,_,_,M,T,_,_,_,_,_,_,T},
                {T,8,_,_,_,_,M,_,_,_,_,_,T,_,Q,Q,Q,Q,_,T},
                {T,_,_,_,_,_,T,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,T,_,_,T,_,_,T,_,Q,Q,Q,Q,_,T},
                {T,M,M,T,T,T,T,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,_,Q,Q,Q,Q,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,M,_,_,M,M,T,T,T,T,_,_,T,H,H,_,_,H,H,T},
                {T,_,_,_,_,_,_,_,_,T,M,M,M,M,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,M,M,M,M,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,T,T,T,T,_,_,T},
                {T,_,T,M,M,M,T,_,_,T,_,_,T,8,_,_,M,_,_,T},
                {T,_,T,M,M,M,T,_,_,T,_,_,T,8,_,_,_,_,_,T},
                {T,_,T,M,M,M,T,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,_,_,_,_,_,_,T},
                {T,_,_,_,_,_,_,_,_,T,_,_,T,_,_,_,M,M,M,T},
                {T,_,Q,Q,Q,Q,Q,_,_,_,_,_,_,_,_,_,M,_,M,T},
                {T,M,_,_,_,_,_,_,_,_,_,_,_,_,_,_,M,M,M,T},
                {T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T,T}
            };

            for(int x = 0; x < 20; x++){
                for(int y = 0; y < 20; y++){
                    map[x,y].tileNumber = tileMap[x,y];
                    map[x,y].objNumber = objMap[x,y];
                }
            }
        }

        //public override void OnDrawGizmos(){}
    }

    private class RandomStyler : MapGenerationMiddleware{
        
        protected override void Process()
        {
            for(int x = 0; x < 20; x++){
                for(int y = 0; y < 20; y++){
                    if(x == 0 || x == 19 || y == 0 || y== 19){
                        map[x,y].tileNumber = TILE_WALL_TREE;
                    }else{
                        map[x,y].tileNumber = TILE_GLASS;
                    }
                }
            }
            for(int i = 0;i < 5; i++){
                int x = random.Next(2,17);
                int y = random.Next(2,17);
                map[x,y].tileNumber = TILE_WALL_TREE;
            }
            for(int i = 0;i < 5; i++){
                int x = random.Next(2,17);
                int y = random.Next(2,17);
                map[x,y].tileNumber = TILE_WALL_FENCE;
            }
            for(int i = 0;i < 5; i++){
                int x = random.Next(1,18);
                int y = random.Next(1,18);
                map[x,y].objNumber = OBJ_BUSHES;
            }
            for(int i = 0;i < 5; i++){
                int x = random.Next(1,18);
                int y = random.Next(1,18);
                map[x,y].objNumber = OBJ_PLANTS;
            }
        }
    }
    public override void Styling(){
        if(useRandom){
            Use(new RandomStyler());
        }else{
            Use(new MockStyler());
        }
    }
}