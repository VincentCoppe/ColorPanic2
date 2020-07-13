using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBlock : ObjectBlock
{

    public JumperBlock(TileGameObject prefab) : base(prefab) { }

    public override long Save()
    {
        var data = Data<CBD_Jumper>();
        return base.Save() | ((long) (data.JumperTransform.localEulerAngles.z)<<8);
    }

    protected override bool Spawn(int x, int y)
    {
        if (base.Spawn(x, y))
        {
            ObjectType = Data<CBD_Jumper>().ObjectType;
            return true;
        }
        return false;
    }


}
