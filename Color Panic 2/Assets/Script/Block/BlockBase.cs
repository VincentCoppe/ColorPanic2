using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockBase
{

    public (int, int)[] Get8Neighbours(int x, int y, GridManager Manager)
    {
        Dictionary<int, (int, int)> d = new Dictionary<int, (int, int)>()
        {
            {0, (-1,1)},{1, (0,1)},{2,(1,1)},{3,(-1,0)},{4,(1,0)},{5,(-1,-1)},{6,(0,-1)},{7,(1,-1)}
        };
        (int, int)[] result = new(int, int)[8];
        for (int i = 0; i < 8; i++)
        {
            int xx = x + d[i].Item1;
            int yy = y + d[i].Item2;
            if (xx < 0 || yy < 0 || xx > Manager.Grid.GetLength(0) - 1 || yy > Manager.Grid.GetLength(1) - 1)
            {
                xx = x;
                yy = y;
            }
            result[i] = (xx, yy);
        }
        return result;
    }

    public (int, int)[] Get4Neighbours(int x, int y, GridManager Manager)
    {
        Dictionary<int, (int, int)> d = new Dictionary<int, (int, int)>()
        {
            {0, (0,1)},{1,(-1,0)},{2,(1,0)},{3,(0,-1)}
        };
        (int, int)[] result = new(int, int)[4];
        for (int i = 0; i < 4; i++)
        {
            int xx = x + d[i].Item1;
            int yy = y + d[i].Item2;
            if (xx < 0 || yy < 0 || xx > Manager.Grid.GetLength(0) - 1 || yy > Manager.Grid.GetLength(1) - 1)
            {
                xx = x;
                yy = y;
            }
            result[i] = (xx, yy);
        }
        return result;
    }

    public abstract void Serialize();

    public abstract void Deserialize();

    public abstract void SpawnTiles(GridManager manager,int x, int y,GameObject toSpawn);

    public abstract void DestroyTiles(int x, int y, GridManager manager);
}
