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
            default: return null;
        }
    }
}