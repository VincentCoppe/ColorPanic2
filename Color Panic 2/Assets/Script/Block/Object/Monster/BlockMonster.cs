using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMonster : ObjectBlock
{
    public BlockMonster(TileGameObject prefab) : base(prefab)
    {
    }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Object)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }

    public override long Save()
    {
        var data = Data<CBD_Monsters>();
        
        return base.Save() | ((long)data.MonsterEnum << 8);
    }


}
