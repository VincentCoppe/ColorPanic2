using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockSpike : BlockBase
{
    public BlockSpike(TileGameObject prefab) : base(prefab) { }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Spike)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }

    protected override bool Spawn(int x, int y)
    {
        if (base.Spawn(x, y)) {
            CalculateOrientation(x,y);
            return true;
        }
        return false;
    }

    public void CalculateOrientation(int x, int y)
    {
        List<Vector3> Orientation = new List<Vector3>()
        {
            new Vector3(0,0,180),new Vector3(0,0,270),new Vector3(0,0,90),new Vector3(0,0,0)
        };


        var data = Data<CBD_Spike>();

        (int, int)[] Neighbours = Manager.Get4Neighbours(x, y);
        for(int i = 0; i < Neighbours.Length; i++)
        {
            if(Manager.Grid[Neighbours[i].Item1,Neighbours[i].Item2] == (BlockEnum)1)
            {
                data.SpikeTransform.localEulerAngles = Orientation[i];
            }

        }
        
    }

    public override long Save()
    {
        var data = Data<CBD_Spike>();
        return base.Save() | ((long) (data.SpikeTransform.localEulerAngles.z/2)<<16);
        
       
    }


}
