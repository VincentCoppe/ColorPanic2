using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformXYBlock : ObjectBlock
{
    public PlatformXYBlock(TileGameObject prefab) : base(prefab)
    {
    }

    protected override bool Spawn(int x, int y)
    {
        if (x == 0 || x == 49) return false;
        else if (Manager.Grid[x - 1, y] == BlockEnum.Air && Manager.Grid[x + 1, y] == BlockEnum.Air)
        {
            Manager.Grid[x - 1, y] = BlockEnum.Object;
            Manager.Grid[x + 1, y] = BlockEnum.Object;
            return base.Spawn(x, y);

        } return false;
    }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Object)
        {
            Manager.Grid[x - 1, y] = BlockEnum.Air;
            Manager.Grid[x + 1, y] = BlockEnum.Air;
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }

}
