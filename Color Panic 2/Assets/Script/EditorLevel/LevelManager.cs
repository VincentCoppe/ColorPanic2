using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private LevelData _levelData;
    private GridManager _prefab;
    private GridManager[,] _gridManagers;
    private bool[,] _loaded;

    private int activeX, activeY;

    public void LoadLevel(LevelData levelData) {
        _levelData = levelData;
        CreateGridManagers();
    }

    public void LoadGrid(int x, int y) {
        GetGrid(activeX, activeY)?.gameObject.SetActive(false);
        GetGrid(activeX-1, activeY)?.gameObject.SetActive(false);
        GetGrid(activeX+1, activeY)?.gameObject.SetActive(false);
        GetGrid(activeX, activeY-1)?.gameObject.SetActive(false);
        GetGrid(activeX, activeY+1)?.gameObject.SetActive(false);

        GetGrid(x, y)?.gameObject.SetActive(true);
        GetGrid(x-1, y)?.gameObject.SetActive(true);
        GetGrid(x+1, y)?.gameObject.SetActive(true);
        GetGrid(x, y-1)?.gameObject.SetActive(true);
        GetGrid(x, y+1)?.gameObject.SetActive(true);

        Initialize(x, y);
        Initialize(x-1, y);
        Initialize(x+1, y);
        Initialize(x, y-1);
        Initialize(x, y+1);

        activeX = x;
        activeY = y;
    }

    private GridManager GetGrid(int x, int y) {
        if (x >= 0 && y >= 0 && x < _levelData.Width && y < _levelData.Height)
            return _gridManagers[x,y];
        return null;
    }

    private void Initialize(int x, int y) {
        var grid = GetGrid(x, y);
        if (grid) {
            if (!_loaded[x,y]) {
                for (int dx = 0; dx < 50; x++)
                {
                    for (int dy = 0; dy < 30; dy++)
                    {
                        _levelData.Blocks[x,y][dx,dy].SpawnTiles(dx, dy, grid, _levelData.Colors);
                    }
                }
            }
            _loaded[x,y] = true;
        }
    }

    private void CreateGridManagers()
    {
        if (_gridManagers != null) {
            foreach (var grid in _gridManagers)
            {
                Destroy(grid);
            }
        }
        _gridManagers = new GridManager[_levelData.Width, _levelData.Height];
        _loaded = new bool[_levelData.Width, _levelData.Height];
        for (int x = 0; x < _levelData.Width; x++)
        {
            for (int y = 0; y < _levelData.Height; y++)
            {
                _gridManagers[x,y] = Instantiate(_prefab.gameObject, transform).GetComponent<GridManager>();
                _gridManagers[x,y].transform.localPosition = new Vector2(x * 50, y * 30);
                _gridManagers[x,y].transform.rotation = Quaternion.identity;
                _loaded[x,y] = false;
            }
        }
    }
}