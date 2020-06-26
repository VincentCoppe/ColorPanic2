using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{


    [SerializeField] private SpriteRenderer BackgroundImage = null;
    [SerializeField] private ToolManager toolManager = null;
    [SerializeField] private GameObject LevelDrawer = null;

    private bool wasDrawing = false;


    BlockEnum[,] _grid = null;
    BlockBase[,] _gridObject = null;
    LineRenderer LineRenderer = null;

    
    public ColorPicker Colors = null;
    public BlockEnum[,] Grid { get { return _grid; } }
    public BlockBase[,] GridObject { get { return _gridObject; } }
    private float cellSize = 1f;


    // Start is called before the first frame update
    void Start()
    {
        setupLevelToDraw();
        setupBackground();
    }

    private void setupBackground()
    {
        BackgroundImage.size = new Vector2(50, 30);
        BackgroundImage.transform.localPosition = new Vector3(- 1, 0, 0);
        BackgroundImage.color = Colors.neutral[0];
        
    }

    public Vector3 GridToPosition(int x, int y)
    {
        return new Vector3(x, y) * (cellSize) - new Vector3(Mathf.FloorToInt(15 * Camera.main.aspect), Mathf.FloorToInt(15), 0f);
    }

    public Vector3 GridToPosition2(int x, int y)
    {
        return new Vector3(x, y) * (cellSize) - new Vector3(Mathf.FloorToInt(15 * Camera.main.aspect), Mathf.FloorToInt(15), 0f) + transform.position;
    }


    private void setupLevelToDraw()
    {

        _grid = new BlockEnum[50, 30];
        _gridObject = new BlockBase[50, 30];
        LineRenderer = GetComponent<LineRenderer>();

        LineRenderer.SetPosition(0, GridToPosition2(0, 0));
        LineRenderer.SetPosition(1, GridToPosition2(0, Grid.GetLength(1)));
        LineRenderer.SetPosition(2, GridToPosition2(Grid.GetLength(0), Grid.GetLength(1)));
        LineRenderer.SetPosition(3, GridToPosition2(Grid.GetLength(0), 0));
    }


    public (int,int) PositionToGrid(Vector3 position)
    {
       
        int x = Mathf.FloorToInt(position.x) + Mathf.FloorToInt(15 * Camera.main.aspect) - Mathf.FloorToInt(transform.position.x);
        int y = Mathf.FloorToInt(position.y) + Mathf.FloorToInt(15) - Mathf.FloorToInt(transform.position.y);
        if (x<50 && x>=0 && y < 30 && y>=0)
            return (x, y);
        return (-1,-1);
    }

    public GameObject Instantiate(GameObject toSpawn)
    {
        GameObject Spawned = Instantiate(toSpawn, transform);
        Spawned.transform.SetParent(LevelDrawer.transform);
        return Spawned;
    }


    
    public void ChangeColor()
    {
        for(int x=0; x<Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                if(Grid[x,y] == BlockEnum.Ground)
                {
                    BlockGround block = (BlockGround)GridObject[x, y];
                    block.UpdateColors(Colors.Red);
                }
            }
        }
        BackgroundImage.color = Colors.Red[0];
    }
    
    public void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            (int,int) mouse = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (mouse != (-1,-1)) {
                toolManager.Action(this,mouse);
                wasDrawing = true;
            }
            if(mouse != (-1,-1) && Input.GetMouseButtonUp(0)){
                wasDrawing = false;
            }
            if(Input.GetMouseButtonUp(0) && wasDrawing && mouse == (-1,-1)) {
                toolManager.EndAction();
                wasDrawing = false;
            }
            
        }
    }

    public (int, int)[] Get8Neighbours(int x, int y)
    {
        Dictionary<int, (int, int)> d = new Dictionary<int, (int, int)>()
        {
            {0, (-1,1)},{1, (0,1)},{2,(1,1)},{3,(-1,0)},{4,(1,0)},{5,(-1,-1)},{6,(0,-1)},{7,(1,-1)}
        };
        (int, int)[] result = new (int, int)[8];
        for (int i = 0; i < 8; i++)
        {
            int xx = x + d[i].Item1;
            int yy = y + d[i].Item2;
            if (xx < 0 || yy < 0 || xx > Grid.GetLength(0) - 1 || yy > Grid.GetLength(1) - 1)
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
        (int, int)[] result = new (int, int)[4];
        for (int i = 0; i < 4; i++)
        {
            int xx = x + d[i].Item1;
            int yy = y + d[i].Item2;
            if (xx < 0 || yy < 0 || xx > Grid.GetLength(0) - 1 || yy > Grid.GetLength(1) - 1)
            {
                xx = x;
                yy = y;
            }
            result[i] = (xx, yy);
        }
        return result;
    }

}
