public static class BlockStyles{
    public static BlockStyle[] allStyles = new BlockStyle[]{
        new BlockStyle1(),
        new BlockStyle2(),
        new BlockStyle3(),
        new BlockStyle1A(),
        new BlockStyle2A(),
        new BlockStyle3A(),
        new BlockStyle4(),
        new BlockStyle5(),
        new BlockStyle6(),
    };
}

public class BlockStyle1 : BlockStyle{

    public BlockStyle1(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {X,X,X,X,X,X,X,X},
            {X,T,T,T,T,_,M,X},
            {X,_,_,_,_,_,M,X},
            {X,_,_,_,_,_,T,X},
            {X,_,_,_,_,_,T,X},
            {X,M,M,T,T,T,T,X},
            {X,X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {X,X,X,X,X,_,X,X},
            {X,T,T,T,T,K,M,X},
            {X,_,_,_,_,_,M,X},
            {X,K,_,_,_,_,T,X},
            {X,_,_,_,_,K,T,X},
            {X,M,M,T,T,T,T,X},
            {X,X,X,X,X,X,X,K}
        };

        X = 0;
        _ = 0;
        T = 1;
        blockMap = new int[,] {
            {X,X,X,X,X,X,X,X},
            {X,T,T,T,T,T,T,X},
            {X,X,X,T,T,T,T,X},
            {X,X,X,T,T,T,T,X},
            {X,X,X,T,T,T,T,X},
            {X,T,T,T,T,T,T,X},
            {X,X,X,X,X,X,X,X}
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}

public class BlockStyle1A : BlockStyle{

    public BlockStyle1A(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {X,X,X,X,X,X,X,X},
            {X,T,T,T,T,_,M,X},
            {X,_,_,_,_,_,M,X},
            {X,_,_,_,_,_,T,X},
            {X,_,_,_,_,_,T,X},
            {X,M,M,T,T,T,T,X},
            {X,X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {X,X,X,X,X,_,X,X},
            {X,T,T,T,T,K,M,X},
            {X,_,_,_,_,_,M,X},
            {X,_,_,_,_,_,T,X},
            {X,_,_,_,_,K,T,X},
            {X,M,M,T,T,T,T,X},
            {X,X,X,X,X,X,X,K}
        };

        X = 0;
        _ = 0;
        T = 1;
        blockMap = new int[,] {
            {X,X,X,X,X,X,X,X},
            {X,X,X,X,X,X,T,X},
            {X,X,X,X,X,X,T,X},
            {X,X,X,X,X,X,T,X},
            {X,X,X,X,X,X,T,X},
            {X,T,T,T,T,T,T,X},
            {X,X,X,X,X,X,X,X}
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}

public class BlockStyle2 : BlockStyle{

    public BlockStyle2(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {X,X,_,_,_,X,X},
            {M,M,M,T,M,M,M},
            {M,M,M,T,M,M,M},
            {T,T,T,T,T,T,T}
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {X,X,X,T,X,X,X},
            {M,M,M,T,M,M,M},
            {M,M,K,T,K,M,M},
            {T,T,T,T,T,T,T}
        };

        X = 0;
        _ = 0;
        M = 1;
        T = 1;
        blockMap = new int[,] {
            {X,X,X,T,X,X,X},
            {M,M,M,T,M,M,M},
            {M,M,M,T,M,M,M},
            {T,T,T,T,T,T,T}
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}
public class BlockStyle2A : BlockStyle{

    public BlockStyle2A(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {X,X,X,_,X,X,X},
            {X,X,X,T,X,X,X},
            {X,X,X,T,X,X,X},
            {T,T,T,T,T,T,T}
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {X,X,X,_,X,X,X},
            {X,X,X,T,X,X,X},
            {X,X,X,T,X,X,X},
            {T,T,T,T,T,T,T}
        };

        X = 0;
        _ = 0;
        M = 1;
        T = 1;
        blockMap = new int[,] {
            {X,X,X,_,X,X,X},
            {X,X,X,T,X,X,X},
            {X,X,X,T,X,X,X},
            {T,T,T,T,T,T,T}
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}
public class BlockStyle3 : BlockStyle{

    public BlockStyle3(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {X,M,X,X,X,M,X},
            {X,T,_,_,_,T,X},
            {X,T,_,_,_,T,X},
            {X,T,T,T,T,T,X},
            {X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {X,M,X,X,X,M,X},
            {X,T,_,_,_,T,X},
            {X,T,_,K,_,T,X},
            {X,T,T,T,T,T,X},
            {X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        T = 1;
        blockMap = new int[,] {
            {X,X,X,X,X,X,X},
            {X,T,T,T,T,T,X},
            {X,T,T,T,T,T,X},
            {X,T,T,T,T,T,X},
            {X,X,X,X,X,X,X}
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}
public class BlockStyle3A : BlockStyle{

    public BlockStyle3A(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {X,X,X,X,X,X,X},
            {X,T,_,M,_,T,X},
            {X,T,_,M,_,T,X},
            {X,T,T,T,T,T,X},
            {X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {X,X,X,X,X,X,X},
            {X,T,_,_,_,T,X},
            {X,T,_,K,_,T,X},
            {X,T,T,T,T,T,X},
            {X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        T = 1;
        blockMap = new int[,] {
            {X,X,X,T,X,X,X},
            {X,X,X,T,X,X,X},
            {X,X,X,T,X,X,X},
            {X,X,X,T,X,X,X},
            {X,X,X,X,X,X,X}
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}
public class BlockStyle4 : BlockStyle{

    public BlockStyle4(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {X,X,X,X,X,X,X,X,X,X,X},
            {X,T,M,M,M,T,T,T,T,T,X},
            {X,M,_,_,_,T,_,_,_,_,X},
            {X,M,_,_,_,T,_,M,M,M,X},
            {X,M,_,_,_,T,_,M,M,M,X},
            {X,T,T,T,T,T,_,M,M,M,X},
            {X,X,X,X,X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {X,X,X,X,X,X,X,X,X,X,X},
            {X,_,M,M,M,T,T,T,T,T,X},
            {X,M,_,_,_,T,_,_,_,_,X},
            {X,M,_,_,_,T,_,M,M,M,X},
            {X,M,_,_,K,T,_,M,K,M,X},
            {X,T,T,T,T,T,_,M,M,M,X},
            {X,X,X,X,X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        M = 1;
        T = 1;
        blockMap = new int[,] {
            {X,X,X,X,X,X,X,X,X,X,X},
            {X,_,M,M,M,T,T,T,T,T,X},
            {X,M,_,_,_,T,_,_,_,_,X},
            {X,M,_,_,_,T,_,M,M,M,X},
            {X,M,_,_,_,T,_,M,M,M,X},
            {X,T,T,T,T,T,_,M,M,M,X},
            {X,X,X,X,X,X,X,X,X,X,X}
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}

public class BlockStyle5 : BlockStyle{

    public BlockStyle5(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {M,M,M,M,M,M},
            {M,M,M,M,M,M},
            {M,M,M,M,M,M},
            {M,M,M,M,M,M},
            {M,M,M,M,M,M},
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {K,M,M,M,M,M},
            {M,M,M,M,K,M},
            {M,M,K,M,M,M},
            {M,M,M,M,M,M},
            {M,K,M,M,M,M},
        };

        X = 0;
        _ = 0;
        M = 1;
        T = 1;
        blockMap = new int[,] {
            {M,M,M,M,M,M},
            {M,M,M,M,M,M},
            {M,M,M,M,M,M},
            {M,M,M,M,M,M},
            {M,M,M,M,M,M},
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}

public class BlockStyle6 : BlockStyle{

    public BlockStyle6(){
        int _, T, H, M, Q, X;

        X = MapGenerator.TILE_NOTHING;
        _ = MapGenerator.TILE_GLASS;
        T = MapGenerator.TILE_WALL_TREE;
        H = MapGenerator.TILE_WALL_FENCE;
        M = OBJ_THRESHOLD + MapGenerator.OBJ_BUSHES;
        Q = OBJ_THRESHOLD + MapGenerator.OBJ_PLANTS;
        tileObjMap = new int[,] {
            {X,X,X,X,X,X,X},
            {X,T,T,T,T,T,X},
            {X,X,X,X,M,T,X},
            {X,T,T,T,X,T,X},
            {X,M,M,T,X,T,X},
            {X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        T = 0;
        H = 0;
        M = 0;
        Q = 0;
        int K = 1;
        itemMap = new int[,] {
            {X,X,X,X,X,X,X},
            {X,T,T,T,T,T,X},
            {X,X,X,K,M,T,X},
            {X,T,T,T,X,T,X},
            {X,M,M,T,X,T,X},
            {X,X,X,X,X,X,X}
        };

        X = 0;
        _ = 0;
        M = 1;
        T = 1;
        blockMap = new int[,] {
            {X,X,X,X,X,X,X},
            {X,T,T,T,T,T,X},
            {X,M,X,K,M,T,X},
            {X,T,T,T,X,T,X},
            {X,M,M,T,M,T,X},
            {X,X,X,X,X,X,X}
        };

        width = tileObjMap.GetLength(0);
        height = tileObjMap.GetLength(1);
    }
}