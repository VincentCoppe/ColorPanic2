﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{


    [SerializeField] private SpriteRenderer BackgroundImage = null;
    [SerializeField] private TileGameObject Ground = null;
    [SerializeField] private TileGameObject Spike = null;





    BlockEnum[,] _grid = null;
    BlockBase[,] _gridObject = null;
    LineRenderer LineRenderer = null;

    
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
        BackgroundImage.size = new Vector2(Camera.main.orthographicSize * 2 * 1.4f, Camera.main.orthographicSize * 2);
        BackgroundImage.transform.localPosition = new Vector3(- Camera.main.orthographicSize*2/5 + 1, 0, 0);
    }

    public Vector3 GridToPosition(int x, int y)
    {
        return new Vector3(x, y) * (cellSize) - new Vector3(Mathf.FloorToInt(Camera.main.orthographicSize * Camera.main.aspect), Mathf.FloorToInt(Camera.main.orthographicSize), 0f);
    }

    public Vector3 GridToPosition2(int x, int y)
    {
        return new Vector3(x, y) * (cellSize) - new Vector3(Mathf.FloorToInt(Camera.main.orthographicSize * Camera.main.aspect), Mathf.FloorToInt(Camera.main.orthographicSize), 0f) + transform.position;
    }


    private void setupLevelToDraw()
    {

        _grid = new BlockEnum[Mathf.FloorToInt(Camera.main.orthographicSize * 2 * 1.4f), Mathf.FloorToInt(Camera.main.orthographicSize * 2)];
        _gridObject = new BlockBase[Mathf.FloorToInt(Camera.main.orthographicSize * 2 * 1.4f), Mathf.FloorToInt(Camera.main.orthographicSize * 2)];
        LineRenderer = GetComponent<LineRenderer>();

        LineRenderer.SetPosition(0, GridToPosition2(0, 0));
        LineRenderer.SetPosition(1, GridToPosition2(0, Grid.GetLength(1)));
        LineRenderer.SetPosition(2, GridToPosition2(Grid.GetLength(0), Grid.GetLength(1)));
        LineRenderer.SetPosition(3, GridToPosition2(Grid.GetLength(0), 0));
    }


    public (int,int) PositionToGrid(Vector3 position)
    {
       
        int x = Mathf.FloorToInt(position.x) + Mathf.FloorToInt(Camera.main.orthographicSize * Camera.main.aspect) - Mathf.FloorToInt(transform.position.x);
        int y = Mathf.FloorToInt(position.y) + Mathf.FloorToInt(Camera.main.orthographicSize) - Mathf.FloorToInt(transform.position.y);
        return (x, y);
    }

    public GameObject Instantiate(GameObject toSpawn)
    {
        return Instantiate(toSpawn, transform);
    }

    public void Update()
    {
        if(Input.GetMouseButton(0))
        {
            
            (int, int) test = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            try
            {
                Ground.Block.NewBlock(Ground).SpawnTiles(test.Item1, test.Item2, this);
                
            } catch (IndexOutOfRangeException e){

            }
        }
        if (Input.GetMouseButton(2))
        {
            (int, int) test = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            try
            {

                Spike.Block.NewBlock(Spike).SpawnTiles(test.Item1, test.Item2, this);

            }
            catch (IndexOutOfRangeException e)
            {

            }
        }
        if (Input.GetMouseButton(1))
        {
            (int, int) test = PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            try
            {
                BlockBase block = GridObject[test.Item1, test.Item2];
                block?.DestroyTiles(test.Item1, test.Item2);
                
            }
            catch (IndexOutOfRangeException){ }
            
        }
    }

}
