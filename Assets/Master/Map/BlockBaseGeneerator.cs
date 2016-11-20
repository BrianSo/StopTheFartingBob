using UnityEngine;
using System.Collections;

public class BlockBaseGeneerator : MapGenerator {

	public override void Styling(){
        Use(new BorderStyler()); // Get a fence

		Use(new BlockLinearStyler(BlockStyles.allStyles[random.Next(0,BlockStyles.allStyles.Length)]));
		Use(new BlockLinearStyler(BlockStyles.allStyles[random.Next(0,BlockStyles.allStyles.Length)]));
		Use(new BlockLinearStyler(BlockStyles.allStyles[random.Next(0,BlockStyles.allStyles.Length)]));
		Use(new BlockLinearStyler(BlockStyles.allStyles[random.Next(0,BlockStyles.allStyles.Length)]));
		Use(new BlockLinearStyler(BlockStyles.allStyles[random.Next(0,BlockStyles.allStyles.Length)]));
		Use(new BlockLinearStyler(BlockStyles.allStyles[random.Next(0,BlockStyles.allStyles.Length)]));
		Use(new BlockLinearStyler(BlockStyles.allStyles[random.Next(0,BlockStyles.allStyles.Length)]));
		Use(new BlockLinearStyler(BlockStyles.allStyles[random.Next(0,BlockStyles.allStyles.Length)]));
		Use(new BlockLinearStyler(new BlockStyle3()));
		Use(new BlockLinearStyler(new BlockStyle2()));
		Use(new BlockStyler(new BlockStyle3()));
		Use(new BlockStyler(new BlockStyle2()));
		Use(new BlockStyler(new BlockStyle3()));
		Use(new BlockStyler(new BlockStyle2()));
		
		Use(new StartingPointStyler()); // Get a fence
    }
}
