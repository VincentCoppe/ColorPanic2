﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rect : ToolManager, ITool
{
    [SerializeField] private Image image = null;
    [SerializeField] private ToolsHistory toolsHistory = null;
    [SerializeField] private Sprite iconCursor = null;
    private HashSet<(int, int)> rect = new HashSet<(int, int)>();
    private (int, int) start = (-1, -1);

    public void ClickIcon() {
        SetTool(this);
    }
    public void SetBgColor(Color color) {
        image.color = color;
    }
    public Sprite GetCursor() {
        return iconCursor;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse2) {       
        if(Input.GetMouseButtonDown(0)) {
            start = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            rect = new HashSet<(int, int)>();
        } else if (Input.GetMouseButtonUp(0) && start != (-1,-1)) {
            HashSet<(int, int)> res = new HashSet<(int, int)>();
            foreach ((int, int) blockToPrint in rect)
            {
                if(block.Block.NewBlock(block).SpawnTiles(blockToPrint.Item1, blockToPrint.Item2, gridManager, gridManager.Colors.neutral)) {
                    res.Add(blockToPrint);
                }
            }
            start = (-1, -1);
            toolsHistory.AddToUndoDraw(res);
            rect = new HashSet<(int, int)>();
        } else if (Input.GetMouseButton(0) && start != (-1, -1)) {
            rect = new HashSet<(int, int)>();
            rect.Add(start);
            (int, int) mouse = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            size = start.Item2-mouse.Item2 != 0 ? Math.Abs(start.Item2-mouse.Item2) : 1;
            int memSize = size;
            (int, int) memMouse = mouse;
            (int, int) memStart = start;
            
            while(size > 0) {
                mouse.Item1 = memMouse.Item1;
                while(mouse.Item1 > start.Item1 || mouse.Item1 < start.Item1) {
                    rect.Add((mouse.Item1, start.Item2));
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item1 = mouse.Item1 > start.Item1 ? mouse.Item1-1 : mouse.Item1+1;
                }
                rect.Add((mouse.Item1, start.Item2));
                rect.Add((mouse.Item1, mouse.Item2));
                if(mouse.Item2 > start.Item2) {
                    mouse.Item2--;
                    start.Item2++;
                } else {
                    mouse.Item2++;
                    start.Item2--;
                }
                size--;
            }
            size = memSize;
            mouse = memMouse;
            start = memStart;
        }        
    }

    public void EndAction() {
        rect = new HashSet<(int, int)>();
    }
    
    public Dictionary<(int, int), GameObject> GetBlocksToHover(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        Dictionary<(int, int), GameObject> res = new Dictionary<(int, int), GameObject>();
        foreach ((int, int) blockToPrint in rect) {
            res[blockToPrint] = block.gameObject;
        }
        return res.Count != 0 ? res : new Dictionary<(int, int), GameObject>{[mouse] = block.gameObject};
    }
}