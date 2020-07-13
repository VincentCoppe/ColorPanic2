using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRect : ToolManager, ITool
{
    
    [SerializeField] private Image image = null;
    [SerializeField] private ToolsHistory toolsHistory = null;
    [SerializeField] private LineRenderer lineRenderer = null;
    [SerializeField] private Sprite iconCursor = null;

    private Dictionary<(int, int), BlockBase> selectedBlocks = new Dictionary<(int, int), BlockBase>();
    private (int, int)[] rect = new (int, int)[2];

    public void ClickIcon() {
        SetTool(this);
        selectedBlocks = new Dictionary<(int, int), BlockBase>();
        rect = new (int, int)[] {(-1,-1), (-1,-1)};
        lineRenderer.positionCount = 0;
    }
    public void SetBgColor(Color color) {
        if(color == new Color(1,1,1)) {
            lineRenderer.positionCount = 0;
        }
        image.color = color;
    }

    public Sprite GetCursor() {
        return iconCursor;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        if(Input.GetMouseButtonDown(1)) {
            selectedBlocks = new Dictionary<(int, int), BlockBase>();
            rect = new (int, int)[] {(-1,-1), (-1,-1)};
            lineRenderer.positionCount = 0;    
        }
        else if(Input.GetMouseButtonDown(0) && selectedBlocks.Count == 0) {
            rect[0] = mouse;
        }
        else if (Input.GetMouseButton(0) && selectedBlocks.Count == 0) {
            Vector3[] vectors = new Vector3[4];
            lineRenderer.positionCount = 4;
            vectors[0] = gridManager.GridToPosition(rect[0].Item1, rect[0].Item2) + (rect[0].Item1 <= mouse.Item1 ? Vector3.zero : Vector3.right) + (rect[0].Item2 <= mouse.Item2 ? Vector3.zero : Vector3.up) - Vector3.forward + gridManager.transform.position;
            vectors[1] = new Vector3((gridManager.GridToPosition(mouse.Item1, mouse.Item2) + (rect[0].Item1 <= mouse.Item1 ? Vector3.right : Vector3.zero)).x, (gridManager.GridToPosition(rect[0].Item1, rect[0].Item2) + (rect[0].Item2 <= mouse.Item2 ? Vector3.zero : Vector3.up)).y, -1) + gridManager.transform.position;
            vectors[2] = gridManager.GridToPosition(mouse.Item1, mouse.Item2) + (rect[0].Item1 <= mouse.Item1 ? Vector3.right : Vector3.zero) + (rect[0].Item2 <= mouse.Item2 ? Vector3.up : Vector3.zero) - Vector3.forward + gridManager.transform.position;
            vectors[3] = new Vector3(gridManager.GridToPosition(rect[0].Item1, rect[0].Item2).x + (rect[0].Item1 <= mouse.Item1 ? Vector3.zero : Vector3.right).x, gridManager.GridToPosition(mouse.Item1, mouse.Item2).y + (rect[0].Item2 <= mouse.Item2 ? Vector3.up : Vector3.zero).y, -1) + gridManager.transform.position;
            lineRenderer.SetPositions(vectors);     
        }
        else if(Input.GetMouseButtonUp(0) && selectedBlocks.Count == 0) {
            rect[1] = mouse;
        }
        if((Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C)) || (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.X)) && rect[1] != (-1,-1)) {
            if(rect[0].Item1 > rect[1].Item1) {
                int mem = rect[0].Item1;
                rect[0].Item1 = rect[1].Item1;
                rect[1].Item1 = mem;
            }
            if(rect[0].Item2 > rect[1].Item2) {
                int mem = rect[0].Item2;
                rect[0].Item2 = rect[1].Item2;
                rect[1].Item2 = mem;
            }
            (int, int) mid = ((rect[1].Item1-rect[0].Item1)/2, (rect[1].Item2-rect[0].Item2)/2);
            for(int y = rect[0].Item2; y <= rect[1].Item2; y++) {
                for(int x = rect[0].Item1; x <= rect[1].Item1; x++) {
                    if(gridManager.Grid[x, y] != BlockEnum.Air) {
                        selectedBlocks[(x+mid.Item1-rect[1].Item1, y+mid.Item2-rect[1].Item2)] = gridManager.GridObject[x, y];
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.X)) {
                lineRenderer.positionCount = 0;
                foreach ((int, int) blockToErase in selectedBlocks.Keys) {
                    BlockBase blockErase = gridManager.GridObject[blockToErase.Item1-mid.Item1+rect[1].Item1, blockToErase.Item2-mid.Item2+rect[1].Item2];
                    blockErase?.DestroyTiles(blockToErase.Item1-mid.Item1+rect[1].Item1, blockToErase.Item2-mid.Item2+rect[1].Item2);
                }
            }
        }
        else if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V) && selectedBlocks.Count != 0) {
            HashSet<(int, int)> res = new HashSet<(int, int)>();
            foreach((int, int) reached in selectedBlocks.Keys) {
                try {
                    if(selectedBlocks[reached].Block.NewBlock(selectedBlocks[reached].Prefab).SpawnTiles(mouse.Item1+reached.Item1, mouse.Item2+reached.Item2, gridManager, gridManager.Colors.neutral)) {
                        res.Add((reached.Item1+mouse.Item1, reached.Item2+mouse.Item2));
                    }
                } catch (IndexOutOfRangeException) {}
            }
            toolsHistory.AddToUndoDraw(res);
        }
        
    }

    public void EndAction(){}
    
    public Dictionary<(int, int), GameObject> GetBlocksToHover(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        Dictionary<(int, int), GameObject> res = new Dictionary<(int, int), GameObject>();
        foreach ((int, int) blockToPrint in selectedBlocks.Keys) {
            res[(mouse.Item1+blockToPrint.Item1, mouse.Item2+blockToPrint.Item2)] = selectedBlocks[blockToPrint].Prefab.gameObject;
        }
        return res;
    }
}
