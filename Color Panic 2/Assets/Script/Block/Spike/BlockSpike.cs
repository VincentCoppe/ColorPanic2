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
        throw new System.NotImplementedException();
    }

    protected override bool Spawn(int x, int y)
    {
        if (CheckGroundNeighbours(x, y) && base.Spawn(x, y)) {
            CalculateOrientation(x,y);
            return true;
        }
        return false;
    }

    private bool CheckGroundNeighbours(int x, int y)
    {
        (int, int)[] Neighbours = Get4Neighbours(x, y);
        foreach((int,int) Neighbour in Neighbours)
        {
            if (Manager.Grid[Neighbour.Item1, Neighbour.Item2] == BlockEnum.Ground) return true;
        }
        return false;
    }

    private void CalculateOrientation(int x, int y)
    {
        List<Vector3> Orientation = new List<Vector3>()
        {
            new Vector3(0,0,180),new Vector3(0,0,270),new Vector3(0,0,0),new Vector3(0,0,90)
        };


        var data = Data<CBD_Spike>();

        (int, int)[] Neighbours = Get4Neighbours(x, y);
        for(int i = 0; i < Neighbours.Length; i++)
        {
            if(Manager.Grid[Neighbours[i].Item1,Neighbours[i].Item2] == (BlockEnum)1)
            {
                data.SpikeTransform.localEulerAngles = Orientation[i];
            } if(Manager.Grid[Neighbours[i].Item1, Neighbours[i].Item2] == (BlockEnum)2)
            {
                data.SpikeTransform.localEulerAngles = Orientation[i] + new Vector3(0, 0, 90);
                break;
            }

        }
        
    }


}
