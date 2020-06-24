using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBlock : BlockBase
{
    public CheckpointBlock(TileGameObject prefab) : base(prefab)
    {
    }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Checkpoint)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            Object.Destroy(GameObject);
        }
    }

    public override void Init(long data)
    {
        base.Init(data);
    }

    public override void UpdateColors(Color[] Colors)
    {
        base.UpdateColors(Colors);
    }

}
