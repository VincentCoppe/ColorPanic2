using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    private LevelData _levelData;
    [SerializeField] private GridManager _prefab;
    [SerializeField] private ColorPicker ColorPicker;
    [SerializeField] private ToolManager ToolManager;
    [SerializeField] private GameObject A00;
    [SerializeField] public ToolsHistory ToolsHistory;
    private GridManager[,] _gridManagers;
    private Vector3 PlayerPlaced = new Vector3(-1,-1,-1);
    private bool FinishPlaced = false;
    private GameObject precedentLevelSelected = null;
    public (GridManager,(int,int)) CurrentGM;
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
        FindObjectOfType<LoadScenes>().LoadLevelManager();
    }

    private GridManager GetGrid(int x, int y) {
        if (x >= 0 && y >= 0 && x < _levelData.Width && y < _levelData.Height)
            return _gridManagers[x,y];
        return null;
        
    }

    public void setPlayerPlaced(Vector3 vector)
    {
        PlayerPlaced = vector;
    }

    public void setFinishPlaced()
    {
        FinishPlaced = !FinishPlaced;
    }

    public Vector3 getPlayerPlaced()
    {
        return PlayerPlaced ;
    }

    public bool getFinishPlaced()
    {
        return FinishPlaced;
    }

    public void ChangeTheme(TMP_Dropdown dropdown)
    {
        
        foreach(GridManager manager in _gridManagers)
        {
            manager.ChangeTheme((ThemeEnum)dropdown.value);
        }
    }

    public void ChangeThemeint(int a)
    {

        foreach (GridManager manager in _gridManagers)
        {
            manager.ChangeTheme((ThemeEnum)a);
        }
    }

    public void Clear()
    {
        foreach (GridManager manager in _gridManagers)
        {
            manager.Clear();
        }
    }

    public void ClearHistory()
    {
        ToolsHistory.ResetHistory();
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
                _gridManagers[x, y].Setup(this);
                _gridManagers[x,y].transform.rotation = Quaternion.identity;
                _loaded[x,y] = false;
                if (ToolManager != null)
                {
                    precedentLevelSelected = A00;
                    ToolsHistory.SetCurrentGM(CurrentGM.Item1);

                }
                _gridManagers[x, y].gameObject.SetActive(false);
            }
        }
        CurrentGM = (_gridManagers[0, 0],(0,0));
        ActivateCurrentGM(0, 0);
        
    }

    public void ChangeLevel(GameObject button)
    {
        if (precedentLevelSelected != button)
        {
            int x = Int32.Parse(button.name.Split(',')[0].Split('(')[1]);
            int y = Int32.Parse(button.name.Split(',')[1].Split(')')[0]);
            button.GetComponent<Image>().color = Color.green;
            if(precedentLevelSelected!=null)
                precedentLevelSelected.GetComponent<Image>().color = Color.white;
            precedentLevelSelected = button;
            ActivateCurrentGM(x, y);
            Camera.main.transform.position = new Vector3(4 + 50 * x, 2.5f + 30 * y, -10);
            ToolsHistory.ResetHistory();
            ToolsHistory.SetCurrentGM(CurrentGM.Item1);

        }
    }

    public void ActivateCurrentGM(int x, int y)
    {
        activeX = x;
        activeY = y;
        (int, int)[] Result2 = Get4Neighbours(CurrentGM.Item2.Item1, CurrentGM.Item2.Item2);
        foreach ((int, int) r in Result2)
        {
            _gridManagers[r.Item1, r.Item2].gameObject.SetActive(false);
        }
        CurrentGM.Item1.gameObject.SetActive(false);
        (int,int)[] Result1 = Get4Neighbours(x, y);
        foreach((int,int) r in Result1)
        {
            _gridManagers[r.Item1, r.Item2].gameObject.SetActive(true);
        }
        _gridManagers[x, y].gameObject.SetActive(true);

        CurrentGM = (_gridManagers[x, y],(x,y));

    }

    public void ChangeLevelIG(int x, int y){
        activeX += x;
        activeY += y;
        CurrentGM.Item1.gameObject.SetActive(false);
        CurrentGM = (_gridManagers[activeX, activeY],(activeX,activeY));
        CurrentGM.Item1.gameObject.SetActive(true);
    }



    public (int, int)[] Get4Neighbours(int x, int y)
    {
        (int, int)[] d = new(int, int)[]
        {
            (0,1), (-1,0), (1,0), (0,-1)
        };
        (int, int)[] result = new(int, int)[4];
        for (int i = 0; i < 4; i++)
        {
            int xx = x + d[i].Item1;
            int yy = y + d[i].Item2;
            if (xx < 0 || yy < 0 || xx > 7 || yy > 7)
            {
                xx = x;
                yy = y;
            }
            result[i] = (xx, yy);
        }
        return result;
    }

}