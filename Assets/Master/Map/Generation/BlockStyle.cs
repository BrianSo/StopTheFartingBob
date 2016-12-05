public class BlockStyle{

    public static int OBJ_THRESHOLD = 0xf00;

    public int width;
    public int height;
    public int[,] tileObjMap;
    public int[,] itemMap;
    public int[,] blockMap;

    public BlockStyle Flip(){
        BlockStyle s = new BlockStyle();
        s.width = width;
        s.height = height;
        s.tileObjMap = new int[width, height];
        s.itemMap = new int[width, height];
        s.blockMap = new int[width, height];
        for(int x = 0;x < width; x++){
            for(int y = 0; y < height; y++){
                s.tileObjMap[x,y] = tileObjMap[width - x - 1,y];
                s.itemMap[x,y] = itemMap[width - x - 1,y];
                s.blockMap[x,y] = blockMap[width - x - 1,y];
            }
        }

        return s;
    }
    public BlockStyle Rotate(int i){
        // i = 1, 2, 3
        switch(i % 4){
            default:
            case 0: return this;
            case 1: return RotateOnce();
            case 2: return Rotate180();  
            case 3: return ReverseRotate();
        }
    }

    //1,2,3
    //4,5,6
    //7,8,9
    //->
    //9,8,7
    //6,5,4
    //3,2,1
    public BlockStyle Rotate180(){
        BlockStyle s = new BlockStyle();
        s.height = height;
        s.width = width;
        s.tileObjMap = new int[width, height];
        s.itemMap = new int[width, height];
        s.blockMap = new int[width, height];
        for(int x = 0;x < width; x++){
            for(int y = 0; y < height; y++){
                
                s.tileObjMap[width - x - 1,height - y - 1] = tileObjMap[x,y];
                s.itemMap[width - x - 1,height - y - 1] = itemMap[x,y];
                s.blockMap[width - x - 1,height - y - 1] = blockMap[x,y];
            }
        }
        return s;
    }

    //1,2,3
    //4,5,6
    //7,8,9
    //->
    //7,4,1
    //8,5,2
    //9,6,3
    public BlockStyle RotateOnce(){
        BlockStyle s = new BlockStyle();
        s.width = height;
        s.height = width;
        s.tileObjMap = new int[height, width];
        s.itemMap = new int[height, width];
        s.blockMap = new int[height, width];
        for(int x = 0;x < width; x++){
            for(int y = 0; y < height; y++){
                
                s.tileObjMap[height - y - 1,x] = tileObjMap[x,y];
                s.itemMap[height - y - 1,x] = itemMap[x,y];
                s.blockMap[height - y - 1,x] = blockMap[x,y];
            }
        }
        return s;
    }
    //1,2,3
    //4,5,6
    //7,8,9
    //->
    //3,6,9
    //2,5,8
    //1,4,7
    public BlockStyle ReverseRotate(){
        BlockStyle s = new BlockStyle();
        s.width = height;
        s.height = width;
        s.tileObjMap = new int[height, width];
        s.itemMap = new int[height, width];
        s.blockMap = new int[height, width];
        for(int x = 0;x < width; x++){
            for(int y = 0; y < height; y++){
                s.tileObjMap[y,width - x - 1] = tileObjMap[x,y];
                s.itemMap[y,width - x - 1] = itemMap[x,y];
                s.blockMap[y,width - x - 1] = blockMap[x,y];
            }
        }
        return s;
    }
}