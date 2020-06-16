using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockSpike : BlockBase
{

    [SerializeField] private Transform SpikeTransform = null;
    GridManager Manager = null;


    public override void Deserialize()
    {
        throw new System.NotImplementedException();
    }

    public override void DestroyTiles(int x, int y, GridManager manager)
    {
        throw new System.NotImplementedException();
    }

    public override void Serialize()
    {
        throw new System.NotImplementedException();
    }

    public override void SpawnTiles(GridManager manager, int x, int y, GameObject toSpawn)
    {
        Manager = manager;
        if(Manager.Grid[x,y] == 0 && CheckGroundNeighbours(x, y))
        {
            Manager.Grid[x, y] = BlockEnum.Spike;
            GameObject spike = Manager.Instantiate(toSpawn);
            Manager.GridObject[x, y] = spike.GetComponent<TileGameObject>();
            CalculateOrientation(x,y);
            spike.transform.localPosition = Manager.GridToPosition(x, y) + new Vector3(0.5f, 0.5f, 0);
        }
    }

    private bool CheckGroundNeighbours(int x, int y)
    {
        (int, int)[] Neighbours = Get4Neighbours(x, y, Manager);
        foreach((int,int) Neighbour in Neighbours)
        {
            if (Manager.Grid[Neighbour.Item1, Neighbour.Item2] == (BlockEnum)1) return true;
        }
        return false;
    }

    private void CalculateOrientation(int x, int y)
    {
        List<Vector3> Orientation = new List<Vector3>()
        {
            new Vector3(0,0,180),new Vector3(0,0,270),new Vector3(0,0,0),new Vector3(0,0,90)
        };


        (int, int)[] Neighbours = Get4Neighbours(x, y, Manager);
        for(int i = 0; i < Neighbours.Length; i++)
        {
            if(Manager.Grid[Neighbours[i].Item1,Neighbours[i].Item2] == (BlockEnum)1)
            {
                SpikeTransform.localEulerAngles = Orientation[i];
            } if(Manager.Grid[Neighbours[i].Item1, Neighbours[i].Item2] == (BlockEnum)2)
            {
                SpikeTransform.localEulerAngles = Orientation[i] + new Vector3(0, 0, 90);
                break;
            }

        }
        
    }


}
