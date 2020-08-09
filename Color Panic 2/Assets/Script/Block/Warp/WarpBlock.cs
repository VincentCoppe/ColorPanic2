using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBlock : BlockBase
{
    public WarpBlock(TileGameObject prefab) : base(prefab)
    {
    }

    protected override bool Spawn(int x, int y)
    {
        if (checkSpawn(x,y))
            if (base.Spawn(x, y)){
                CalculateOrientation(x,y);
                return true;
            }
        return false;
        
    }

    private void CalculateOrientation(int x, int y)
    {
        var data = Data<CBD_Warp>();
        if (y == 0)
            data.WarpTransform.localEulerAngles = new Vector3(0, 0, 0);
        else if (x == Manager.Grid.GetLength(0)-1)
            data.WarpTransform.localEulerAngles = new Vector3(0, 0, 90);
        else if (y == Manager.Grid.GetLength(1)-1)
            data.WarpTransform.localEulerAngles = new Vector3(0, 0, 180);
        else if (x == 0)
            data.WarpTransform.localEulerAngles = new Vector3(0, 0, 270);
    }

    private bool checkSpawn(int x, int y)
    {
        return 
            (x == 0 && ((Manager.Grid[Manager.Grid.GetLength(0) - 1, y] == BlockEnum.Air) || (Manager.Grid[Manager.Grid.GetLength(0) - 1, y] == BlockEnum.Warp))) ||
            (y == 0 && ((Manager.Grid[x, Manager.Grid.GetLength(1) - 1] == BlockEnum.Air) || (Manager.Grid[x, Manager.Grid.GetLength(1) - 1] == BlockEnum.Warp))) ||
            (x == Manager.Grid.GetLength(0) - 1 && ((Manager.Grid[0, y] == BlockEnum.Air) || (Manager.Grid[0, y] == BlockEnum.Warp))) ||
            (y == Manager.Grid.GetLength(1) - 1 && ((Manager.Grid[x, 0] == BlockEnum.Air) || (Manager.Grid[x, 0] == BlockEnum.Warp)));
    }

    public override long Save()
    {
        var data = Data<CBD_Warp>();
        return base.Save() | ((long)(data.WarpTransform.localEulerAngles.z / 2) << 16);
    }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Warp)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }
}
