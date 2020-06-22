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

}
