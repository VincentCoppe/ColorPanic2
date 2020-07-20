using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{


    [SerializeField] private Sprite[] Background = new Sprite[3];
    [SerializeField] private SpriteRenderer BackgroundImage;
    private LevelManager Parent;
    private bool drawing = false;
    public ThemeEnum Theme = ThemeEnum.Dungeon;
    LineRenderer LineRenderer = null;

    
    public ColorPicker Colors = null;
    public ToolManager toolManager = null;
    public BlockEnum[,] Grid { get; private set; } = null;
    public BlockBase[,] GridObject { get; private set; } = null;
    private float cellSize = 1f;


    // Start is called before the first frame update
    public void Setup(LevelManager p)
    {
        Parent = p;
        setupLevelToDraw();
        setupBackground();
    }

    private void setupBackground()
    {
        //print((int)Theme);
        BackgroundImage.sprite = Background[(int)Theme];
        BackgroundImage.size = new Vector2(50, 30);
        BackgroundImage.transform.localPosition = new Vector3(- 1, 0, 0);
      //  BackgroundImage.color = Colors.neutral[0];
        
    }

    public Vector3 GridToPosition(int x, int y)
    {
        return new Vector3(x, y) * (cellSize) - new Vector3(Mathf.FloorToInt(15 * Camera.main.aspect), Mathf.FloorToInt(15), 0f);
    }

    internal void Clear()
    {
        for (int y = 0; y < GridObject.GetLength(1); y++)
        {
            for (int x = 0; x <GridObject.GetLength(0); x++)
            {
                BlockBase blockToErase = GridObject[x, y];
                if (blockToErase != null)
                {
                    blockToErase.DestroyTiles(x, y);
                }
            }
        }
    }

    public Vector3 GridToPosition2(int x, int y)
    {
        return new Vector3(x, y) * (cellSize) - new Vector3(Mathf.FloorToInt(15 * Camera.main.aspect), Mathf.FloorToInt(15), 0f) + transform.position;
    }


    private void setupLevelToDraw()
    {

        Grid = new BlockEnum[50, 30];
        GridObject = new BlockBase[50, 30];
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
        return Instantiate(toSpawn, transform);
    }

    public void ChangeTheme(ThemeEnum theme)
    {
        Theme = theme;
        setupBackground();
        for (int x = 0; x < Grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                if (Grid[x, y] == BlockEnum.Ground)
                {
                    BlockGround block = (BlockGround)GridObject[x, y];
                    block.ChangeTheme(x,y);
                }
            }
        }
        
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
        if (toolManager != null && Parent.CurrentGM.Item1.Equals(this))
        {
            (int,int) mouse = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
            {
                if(Input.GetMouseButtonDown(0)) {
                    drawing = mouse != (-1,-1);
                }
                if (drawing && mouse != (-1,-1)) {
                    toolManager.Action(this,mouse);
                }
                if(mouse != (-1,-1) && Input.GetMouseButtonUp(0)){
                    drawing = false;
                }
                if(Input.GetMouseButtonUp(0) && drawing && mouse == (-1,-1)) {
                    toolManager.EndAction();
                    drawing = false;
                }
            
            }
            if(mouse != (-1,-1)) {
                toolManager.DisplayHover(this, mouse);
            } else {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
                toolManager.CleanHover();
            }
            if(Input.GetKey(KeyCode.LeftControl) && (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.X)) || Input.GetMouseButtonDown(1)) {
                toolManager.Action(this, mouse);
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
