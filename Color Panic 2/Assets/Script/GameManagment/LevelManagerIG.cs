using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerIG : MonoBehaviour {

    private LevelData _levelData;
    [SerializeField] private GridManager _prefab;
    [SerializeField] private ColorPicker ColorPicker;
    [SerializeField] private ToolManager ToolManager;
    [SerializeField] private GameObject A00;
    private GridManager[,] _gridManagers;
    private bool PlayerPlaced = false;
    private bool FinishPlaced = false;
    private GameObject precedentLevelSelected = null;
    public GridManager CurrentGM;
    private bool[,] _loaded;
    public GridManager[,] GridManagers {  get { return _gridManagers; } }
    private int activeX, activeY;
    
    public void Start()
    {
        newLevel();
    }

    private void newLevel()
    {
        
        CreateGridManagers();
    }

    private GridManager GetGrid(int x, int y) {
        if (x >= 0 && y >= 0 && x < _levelData.Width && y < _levelData.Height)
            return _gridManagers[x,y];
        return null;
        
    }

    public void setPlayerPlaced()
    {
        PlayerPlaced = !PlayerPlaced;
    }

    public void setFinishPlaced()
    {
        FinishPlaced = !FinishPlaced;
    }

    public bool getPlayerPlaced()
    {
        return PlayerPlaced;
    }

    public bool getFinishPlaced()
    {
        return FinishPlaced;
    }

    public void ChangeTheme(int theme)
    {
        foreach(GridManager manager in _gridManagers)
        {
            manager.ChangeTheme((ThemeEnum)theme);
        }
    }

    public void Clear()
    {
        foreach (GridManager manager in _gridManagers)
        {
            manager.Clear();
        }
    }

    public void CreateGridManagers()
    {
        if (_gridManagers != null) {
            foreach (var grid in _gridManagers)
            {
                Destroy(grid);
            }
        }
        _gridManagers = new GridManager[8, 8];
        _loaded = new bool[8, 8];
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                _gridManagers[x,y] = Instantiate(_prefab.gameObject, transform).GetComponent<GridManager>();
                _gridManagers[x,y].transform.localPosition = new Vector2(x * 50, y * 30);
                _gridManagers[x, y].Colors = ColorPicker;
                _gridManagers[x, y].toolManager = ToolManager;
                _gridManagers[x, y].Setup();
                _gridManagers[x,y].transform.rotation = Quaternion.identity;
                _loaded[x,y] = false;
                if (ToolManager != null)
                {
                    precedentLevelSelected = A00;

                }
                _gridManagers[x, y].gameObject.SetActive(false);
            }
        }
        CurrentGM = _gridManagers[0, 0];
        CurrentGM.gameObject.SetActive(true);
        activeX = 0;
        activeY = 0;
    }

    public void SetCurrentGameManager(int x, int y){
        activeX = x;
        activeY = y;
        CurrentGM = _gridManagers[activeX, activeY];
        CurrentGM.gameObject.SetActive(true);
    }

    public void ChangeLevelIG(int x, int y){
        activeX += x;
        activeY += y;
        CurrentGM.gameObject.SetActive(false);
        CurrentGM = _gridManagers[activeX, activeY];
        CurrentGM.gameObject.SetActive(true);
    }
    
}