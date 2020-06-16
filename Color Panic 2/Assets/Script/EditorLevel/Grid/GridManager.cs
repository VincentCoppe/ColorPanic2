using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private TileGameObject tile = null;

    BlockEnum[,] _grid = null;
    GameObject[,] _gridObject = null;
    LineRenderer LineRenderer = null;
    
    public BlockEnum[,] Grid { get { return _grid; } }
    public GameObject[,] GridObject { get { return _gridObject; } }
    private float cellSize = 1f;


    // Start is called before the first frame update
    void Start()
    {
        setupLevelToDraw(); 
    }

    public Vector3 GridToPosition(int x, int y)
    {
        return new Vector3(x, y) * (cellSize) - new Vector3(Mathf.FloorToInt(Camera.main.orthographicSize* Camera.main.aspect), Mathf.FloorToInt(Camera.main.orthographicSize), 0f);
    }


    private void setupLevelToDraw()
    {

        _grid = new BlockEnum[Mathf.FloorToInt(Camera.main.orthographicSize * 2 * 1.4f), Mathf.FloorToInt(Camera.main.orthographicSize * 2)];
        _gridObject = new GameObject[Mathf.FloorToInt(Camera.main.orthographicSize * 2 * 1.4f), Mathf.FloorToInt(Camera.main.orthographicSize * 2)];
        LineRenderer = GetComponent<LineRenderer>();

        LineRenderer.SetPosition(0, GridToPosition(0, 0));
        LineRenderer.SetPosition(1, GridToPosition(0, Grid.GetLength(1)));
        LineRenderer.SetPosition(2, GridToPosition(Grid.GetLength(0), Grid.GetLength(1)));
        LineRenderer.SetPosition(3, GridToPosition(Grid.GetLength(0), 0));
    }


    public (int,int) PositionToGrid(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x) + Mathf.FloorToInt(Camera.main.orthographicSize * Camera.main.aspect);
        int y = Mathf.FloorToInt(position.y) + Mathf.FloorToInt(Camera.main.orthographicSize);
        return (x, y);
    }

    public GameObject Instantiate(GameObject toSpawn)
    {
        return Instantiate(toSpawn, transform);
    }

    public void Destroy(int x,int y)
    {
        Destroy(GridObject[x, y]);
    }

    public void Update()
    {
        if(Input.GetMouseButton(0))
        {
            (int, int) test = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            try
            {

                
                tile.Block.SpawnTiles(this, test.Item1, test.Item2,tile.gameObject);
                
            } catch (IndexOutOfRangeException e){

            }
        } if(Input.GetMouseButton(1))
        {
            (int, int) test = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            try
            {
                GameObject blockToDestroy = GridObject[test.Item1, test.Item2];
                if (blockToDestroy)
                {
                    BlockBase block = blockToDestroy.GetComponent<TileGameObject>().Block;
                    block.DestroyTiles(test.Item1, test.Item2, this);
                }
                
            }
            catch (IndexOutOfRangeException){ }
            
        }
    }

}
