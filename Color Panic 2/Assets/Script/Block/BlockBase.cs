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
    protected Color[] Colors;
    public GameObject GameObject { get; protected set; }
    public TileGameObject Prefab { get { return _prefab; } }
    public BlockEnum Block { get { return _block; } }

   

    public BlockBase(TileGameObject prefab) {
        _prefab = prefab;
        _block = prefab.Block;
    }

    public virtual long Save() {
        return (long) _block << 24 | 255<<0;
    }

    public virtual void Init(long data) { }


    public bool SpawnTiles(int x, int y, GridManager manager, Color[] colors) {
        Manager = manager;
        Colors = colors;
        return Spawn(x, y);
    }

    public virtual void UpdateColors(Color[] colors) { Colors = colors; }
    public virtual void ChangeTheme(int x, int y) { }

    protected virtual bool Spawn(int x, int y) {
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
