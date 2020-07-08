using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTapis : ObjectBlock
{
    public BlockTapis(TileGameObject prefab) : base(prefab) { }

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

    public override long Save()
    {
        var data = Data<CBD_Tapis>();
        return base.Save() | ((long)data.Center.localEulerAngles.z << 8);
    }

    protected override bool Spawn(int x, int y)
    {
        if (base.Spawn(x, y))
        {
            
            ObjectType = Data<CBD_Tapis>().ObjectType;
            CalculateNeighbours(x, y, true);
            return true;

        }
        return false;
    }

    public void CalculateNeighbours(int x, int y, bool first)
    {
        (int, int)[] Neighbours = Manager.Get8Neighbours(x, y);

        List<ObjectEnum> BeNeighbours = new List<ObjectEnum>();
        foreach ((int, int) Neighbour in Neighbours)
        {
            ObjectEnum a;
            if(first && Manager.GridObject[Neighbour.Item1, Neighbour.Item2] is BlockTapis)
            {
                CalculateNeighbours(Neighbour.Item1, Neighbour.Item2, false);
                a = ObjectEnum.Tapis;
            } else
            {
                a = ObjectEnum.Jumper;
            }
            BeNeighbours.Add(a);
        }
        BlockTapis block;
        if (Manager.GridObject[x, y] is BlockTapis)
        {
            block = (BlockTapis)Manager.GridObject[x, y];
            block?.CalculateEdge(BeNeighbours);
        }

    }

    public void CalculateEdge(List<ObjectEnum> Neighbours)
    {
        var data = Data<CBD_Tapis>();
        for (int i = 0; i < data.Corners.Length; i++)
        {
            bool a = false;

            foreach (int n in Placement[i])
            {
                if (Neighbours[n] != ObjectEnum.Tapis) a = true;
            }
       
            data.Corners[i].gameObject.SetActive(a);
        }
  
    }

}
