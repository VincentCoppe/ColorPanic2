using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlockGround : BlockBase
{
    private CBD_Ground _data;

    private int[][] Placement = new int[][] {
        new int[]{0,1,3},
        new int[]{1},
        new int[]{1,2,4},
        new int[]{3},
        new int[]{4},
        new int[]{3,5,6},
        new int[]{6},
        new int[]{4,6,7},
    };

    public BlockGround(TileGameObject prefab) : base(prefab) {}

    public override void Init(long data) { }

    public override void DestroyTiles(int x, int y)
    {

        if (Manager.Grid[x, y] == (BlockEnum)1)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            CalculateNeighbours(x, y, true);
            DestroySpikes(x, y);
            UnityEngine.Object.Destroy(GameObject);
        }
    }

    private void DestroySpikes(int x, int y)
    {
        (int, int)[] neighbours = Get4Neighbours(x, y);
        
        foreach((int, int) neighbour in neighbours)
        {
            if (Manager.Grid[neighbour.Item1, neighbour.Item2] == BlockEnum.Spike)
            {
                bool Destroy = true;
                foreach ((int, int) n in Get4Neighbours(neighbour.Item1, neighbour.Item2))
                {
                    if (n != (x, y) && Manager.Grid[n.Item1, n.Item2] == BlockEnum.Ground)
                    {
                        Destroy = false;
                    }
                }
                if (Destroy) Manager.GridObject[neighbour.Item1, neighbour.Item2].DestroyTiles(neighbour.Item1, neighbour.Item2);
                else
                {
                    BlockSpike a = (BlockSpike)Manager.GridObject[neighbour.Item1, neighbour.Item2];
                    a.CalculateOrientation(neighbour.Item1, neighbour.Item2);
                }
            }
        }

    }

    public override long Save()
    {
        return base.Save();
    }

    protected override bool Spawn(int x, int y)
    {
        if (base.Spawn(x, y)) {
            CalculateNeighbours(x, y, true);
            return true;
        }
        return false;
    }

    public void CalculateNeighbours(int x, int y, bool first)
    {
        (int, int)[] Neighbours = Get8Neighbours(x, y);

        List<BlockEnum> BeNeighbours = new List<BlockEnum>();
        foreach ((int, int) Neighbour in Neighbours)
        {
            if (first && Manager.Grid[Neighbour.Item1, Neighbour.Item2] == (BlockEnum)1) CalculateNeighbours(Neighbour.Item1, Neighbour.Item2, false);

            BeNeighbours.Add(Manager.Grid[Neighbour.Item1, Neighbour.Item2]);

        }
        BlockGround block;
        if (Manager.Grid[x, y] == (BlockEnum)1)
        {
            block = (BlockGround) Manager.GridObject[x, y];
            block?.CalculateEdge(BeNeighbours);
        }

    }

    public void CalculateEdge(List<BlockEnum> Neighbours)
    {
        var data = Data<CBD_Ground>();
        for (int i = 0; i < data.Corners.Length; i++)
        {
            bool a = false;

            foreach (int n in Placement[i])
            {
                if (Neighbours[n] != (BlockEnum)1) a = true;
            }
            data.Corners[i].gameObject.SetActive(a);
        }
    }

   
}
