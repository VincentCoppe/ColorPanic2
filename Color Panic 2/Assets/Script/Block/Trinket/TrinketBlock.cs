using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketBlock : BlockBase
{
    public TrinketBlock(TileGameObject prefab) : base(prefab) { }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Trinket)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }
}
