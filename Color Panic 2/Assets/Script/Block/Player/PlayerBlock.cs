using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : BlockBase
{
    public PlayerBlock(TileGameObject prefab) : base(prefab) { }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Player)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }
}
