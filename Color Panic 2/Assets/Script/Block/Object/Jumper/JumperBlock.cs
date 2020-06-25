using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBlock : ObjectBlock
{

    public JumperBlock(TileGameObject prefab) : base(prefab) { }

    public override long Save()
    {
        var data = Data<CBD_Jumper>();
        return base.Save() | ((long) (data.JumperTransform.localEulerAngles.z/2)<<8);
    }

    protected override bool Spawn(int x, int y, Color[] Colors)
    {
        if (base.Spawn(x, y, Colors))
        {
            ObjectType = Data<CBD_Tapis>().ObjectType;
            return true;
        }
        return false;
    }


}
