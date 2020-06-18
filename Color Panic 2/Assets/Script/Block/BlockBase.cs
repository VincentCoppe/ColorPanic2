using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockBase
{
    
    protected GridManager Manager = null;
    protected BlockEnum _block = BlockEnum.Air;

    protected TileGameObject _prefab;
    protected TileGameObject _instance;

    public GameObject GameObject { get; protected set; }

    public (int, int)[] Get8Neighbours(int x, int y)
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

    public (int, int)[] Get4Neighbours(int x, int y)
    {
        (int, int)[] d = new (int, int)[]
        {
            (0,1), (-1,0), (1,0), (0,-1)
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

    public BlockBase(TileGameObject prefab) {
        _prefab = prefab;
        _block = prefab.Block;
    }

    public virtual long Save() {
        return (long) _block << 24 | 255<<0;
    }

    public virtual void Init(long data) { }


    public void SpawnTiles(int x, int y, GridManager manager, Color[] Colors) {
        Manager = manager;
        Spawn(x, y, Colors);
    }

    public virtual void UpdateColors(Color[] Colors) { }
    

    protected virtual bool Spawn(int x, int y, Color[] Colors) {
        if (Manager.Grid[x, y] != BlockEnum.Air) return false;
        
        Manager.Grid[x, y] = _block;
        GameObject = Manager.Instantiate(_prefab.gameObject);
        _instance = GameObject.GetComponent<TileGameObject>();
        UpdateColors(Colors);
        Manager.GridObject[x, y] = this;
        GameObject.transform.localPosition = Manager.GridToPosition(x, y) + new Vector3(0.5f, 0.5f, 0);

        return true;
    }


    public virtual void DestroyTiles(int x, int y) { }


    protected T Data<T>() where T : CustomBlockData {
        return (T) _instance.Data;
    }

}
