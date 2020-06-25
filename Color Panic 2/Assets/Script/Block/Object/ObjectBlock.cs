using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBlock : BlockBase
{

    public ObjectEnum ObjectType = ObjectEnum.Jumper;

    public override void Init(long data) { }

    public ObjectBlock(TileGameObject prefab) : base(prefab){ }

    public override long Save()
    {
        var data = Data<CBD_Object>();
        return base.Save() | ((long)(data.ObjectType) << 16);
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



}
