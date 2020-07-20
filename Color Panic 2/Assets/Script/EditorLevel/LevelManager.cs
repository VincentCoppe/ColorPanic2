using System;
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
                _gridManagers[x, y].Setup();
                _gridManagers[x,y].transform.rotation = Quaternion.identity;
                _loaded[x,y] = false;
                if (ToolManager != null)
                {
                    precedentLevelSelected = A00;
                    ToolsHistory.SetCurrentGM(CurrentGM);

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
        //CurrentGM.gameObject.SetActive(false);
        CurrentGM = _gridManagers[activeX, activeY];
        CurrentGM.gameObject.SetActive(true);
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
            _gridManagers[x, y].gameObject.SetActive(true);
            CurrentGM.gameObject.SetActive(false);
            CurrentGM = _gridManagers[x, y];
            Camera.main.transform.position = new Vector3(4 + 50 * x, 2.5f + 30 * y, -10);
            ToolsHistory.ResetHistory();
            ToolsHistory.SetCurrentGM(CurrentGM);

        }
    }

    public void ChangeLevelIG(int x, int y){
        activeX += x;
        activeY += y;
        CurrentGM.gameObject.SetActive(false);
        CurrentGM = _gridManagers[activeX, activeY];
        CurrentGM.gameObject.SetActive(true);
    }
    
}