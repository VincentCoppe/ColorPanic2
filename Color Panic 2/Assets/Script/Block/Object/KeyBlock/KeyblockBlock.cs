using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyblockBlock : ObjectBlock
{
    public KeyblockBlock(TileGameObject prefab) : base(prefab)
    {
    }

    public override long Save()
    {
        var data = Data<CBD_KeyBlock>();
        return base.Save() | ((long)((data.Door)?100:0) << 8);
    }
}
