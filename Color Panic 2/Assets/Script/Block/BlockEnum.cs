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
    Warp,
    End,
    Powerup,
    Object
}

public static class BlockEnumExt {
    public static BlockBase NewBlock(this BlockEnum block, TileGameObject prefab) {
        switch(block) {
            case BlockEnum.Ground: return new BlockGround(prefab);
            case BlockEnum.Spike: return new BlockSpike(prefab);
            case BlockEnum.Powerup: return new BlockPowerup(prefab);
            case BlockEnum.Checkpoint: return new CheckpointBlock(prefab);
            case BlockEnum.Warp: return new WarpBlock(prefab);
            case BlockEnum.End: return new EndBlock(prefab);
            case BlockEnum.Player: return new PlayerBlock(prefab);
            case BlockEnum.Trinket: return new TrinketBlock(prefab);
            case BlockEnum.Object:
                switch (((CBD_Object)prefab.Data).ObjectType)
                {
                    case ObjectEnum.Jumper: return new JumperBlock(prefab);
                    case ObjectEnum.Tapis: return new BlockTapis(prefab);
                    case ObjectEnum.SandBlock: return new SandBlock(prefab);
                    case ObjectEnum.Key: return new KeyBlockEditor(prefab);
                    case ObjectEnum.KeyBlock: return new KeyblockBlock(prefab);
                    case ObjectEnum.Platform: return new PlatformBlock(prefab);
                    case ObjectEnum.Monsters: return new BlockMonster(prefab);
                    case ObjectEnum.PlatformX: return new PlatformXYBlock(prefab);
                    case ObjectEnum.PlatformY: return new PlatformXYBlock(prefab);
                    default: return null;
                }
               
            default: return null;
        }
    }
}