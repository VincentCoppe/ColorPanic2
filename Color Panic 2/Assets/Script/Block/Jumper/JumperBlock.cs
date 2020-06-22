using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBlock : BlockBase
{

    public JumperBlock(TileGameObject prefab) : base(prefab) { }

    protected override bool Spawn(int x, int y, Color[] Colors)
    {
        if (base.Spawn(x, y,Colors)) {
            return true;
        }
        return false;
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
        var data = Data<CBD_Jumper>();
        return base.Save() | ((long) (data.JumperTransform.localEulerAngles.z/2)<<16);
    }

}
