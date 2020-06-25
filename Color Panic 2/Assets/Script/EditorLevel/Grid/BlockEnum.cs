using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockEnum
{
    Air,
    Ground,
    Spike,
    Player,
    Trinket,
    Checkpoint,
    Powerup,
    Object,
    Warp,
    End
}

public static class BlockEnumExt {
    public static BlockBase NewBlock(this BlockEnum block, TileGameObject prefab) {
        switch(block) {
            case BlockEnum.Ground: return new BlockGround(prefab);
            case BlockEnum.Spike: return new BlockSpike(prefab);
            case BlockEnum.Powerup: return new BlockPowerup(prefab);
            case BlockEnum.Checkpoint: return new CheckpointBlock(prefab);
            case BlockEnum.Object:
                switch (((CBD_Object)prefab.Data).ObjectType)
                {
                    case ObjectEnum.Jumper: return new JumperBlock(prefab);
                    case ObjectEnum.Tapis: return new BlockTapis(prefab);
                    default: return null;
                }
               
            default: return null;
        }
    }
}