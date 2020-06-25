using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rect : ToolManager, Tool
{
    [SerializeField] private Image image = null;
    [SerializeField] private ToolsHistory toolsHistory = null;
    private HashSet<(int, int)> rect = new HashSet<(int, int)>();
    private (int, int) start = (-1, -1);

    public void ClickIcon() {
        SetTool(this);
    }
    public void SetBgColor(Color color) {
        image.color = color;
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
        } else if (start != (-1, -1)) {
            rect = new HashSet<(int, int)>();
            rect.Add(start);
            (int, int) mouse = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            while(mouse.Item1 >= start.Item1) {
                int mem = mouse.Item2;
                while(mouse.Item2 >= start.Item2) {
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item2--;
                }
                while(mouse.Item2 <= start.Item2) {
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item2++;
                }
                mouse.Item2 = mem;
                mouse.Item1--;
            }
            while(mouse.Item1 <= start.Item1) {
                int mem = mouse.Item2;
                while(mouse.Item2 >= start.Item2) {
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item2--;
                }
                while(mouse.Item2 <= start.Item2) {
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item2++;
                }
                mouse.Item2 = mem;
                mouse.Item1++;
            }
        }

        
    }
}