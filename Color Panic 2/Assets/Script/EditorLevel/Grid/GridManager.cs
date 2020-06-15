using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private GroundTile tile = null;

    BlockEnum[,] Grid = null;
    GameObject[,] GridObject = null;
    LineRenderer LineRenderer = null;
    

    private float cellSize = 1f;


    // Start is called before the first frame update
    void Start()
    {
        setupLevelToDraw(); 
    }

    private Vector3 GridToPosition(int x, int y)
    {
        return new Vector3(x, y) * (cellSize) - new Vector3(Mathf.FloorToInt(Camera.main.orthographicSize* Camera.main.aspect), Mathf.FloorToInt(Camera.main.orthographicSize), 0f);
    }


    private void setupLevelToDraw()
    {

        Grid = new BlockEnum[Mathf.FloorToInt(Camera.main.orthographicSize * 2 * 1.4f), Mathf.FloorToInt(Camera.main.orthographicSize * 2)];
        GridObject = new GameObject[Mathf.FloorToInt(Camera.main.orthographicSize * 2 * 1.4f), Mathf.FloorToInt(Camera.main.orthographicSize * 2)];
        LineRenderer = GetComponent<LineRenderer>();

        LineRenderer.SetPosition(0, GridToPosition(0, 0));
        LineRenderer.SetPosition(1, GridToPosition(0, Grid.GetLength(1)));
        LineRenderer.SetPosition(2, GridToPosition(Grid.GetLength(0), Grid.GetLength(1)));
        LineRenderer.SetPosition(3, GridToPosition(Grid.GetLength(0), 0));

        
    }


    private void SpawnTitles(int x, int y)
    {
        
        if(Grid[x,y] == (BlockEnum)0)
        {
            Grid[x, y] = BlockEnum.Ground;
            GameObject ground = Instantiate(tile.gameObject, transform);
            GridObject[x,y] = ground;
            CalculateNeighbours(x,y,true);
            ground.transform.localPosition = GridToPosition(x, y) + new Vector3(0.5f, 0.5f, 0);
            
        }
    }

    private (int,int)[] GetNeighbours(int x,int y)
    {
        Dictionary<int, (int, int)> d = new Dictionary<int, (int, int)>()
        {
            {0, (-1,1)},{1, (0,1)},{2,(1,1)},{3,(-1,0)},{4,(1,0)},{5,(-1,-1)},{6,(0,-1)},{7,(1,-1)}
        };
        (int, int)[] result = new(int, int)[8];
        for(int i = 0; i < 8; i++)
        {
            result[i] = (x + d[i].Item1, y + d[i].Item2);
        }
        return result;
    }

    private void CalculateNeighbours(int x,int y,bool first)
    {
        (int,int)[] Neighbours = GetNeighbours(x, y);
        
        List<BlockEnum> BeNeighbours = new List<BlockEnum>();
        foreach((int,int) Neighbour in Neighbours)
        {
            if (first && Grid[Neighbour.Item1,Neighbour.Item2] == (BlockEnum)1) CalculateNeighbours(Neighbour.Item1, Neighbour.Item2, false);

            BeNeighbours.Add(Grid[Neighbour.Item1, Neighbour.Item2]);
                
        }
        GroundTile tile;
        if (Grid[x, y] == (BlockEnum)1)
        {
            tile = GridObject[x, y].GetComponent<GroundTile>();
            tile.calculateNeighbours(BeNeighbours);
        }
 
    }

    private void DestroyTitles(int x, int y)
    {

        if (Grid[x,y] == (BlockEnum)1)
        {
            Grid[x, y] = BlockEnum.Air;
            Destroy(GridObject[x, y]);
            CalculateNeighbours(x, y,true);
        }
    }


    public (int,int) PositionToGrid(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x) + Mathf.FloorToInt(Camera.main.orthographicSize * Camera.main.aspect);
        int y = Mathf.FloorToInt(position.y) + Mathf.FloorToInt(Camera.main.orthographicSize);
        return (x, y);
    }

    public void Update()
    {
        if(Input.GetMouseButton(0))
        {
            (int, int) test = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            try
            {
                SpawnTitles(test.Item1, test.Item2);
            } catch (IndexOutOfRangeException e){

            }
        } if(Input.GetMouseButton(1))
        {
            (int, int) test = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            try
            {
                DestroyTitles(test.Item1, test.Item2);
            }
            catch (IndexOutOfRangeException){ }
            
        }
    }

}
