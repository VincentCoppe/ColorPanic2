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

    private Dictionary<(int, int), BlockBase> selectedBlocks = new Dictionary<(int, int), BlockBase>();
    private (int, int)[] rect = new (int, int)[2];

    public void ClickIcon() {
        SetTool(this);
        selectedBlocks = new Dictionary<(int, int), BlockBase>();
        rect = new (int, int)[2];
    }
    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        if(Input.GetMouseButtonDown(0) && selectedBlocks.Count != 0) {
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
        else if(Input.GetMouseButtonDown(0)) {
            selectedBlocks = new Dictionary<(int, int), BlockBase>();
            rect = new (int, int)[2];
            rect[0] = mouse;        
        }
        else if (Input.GetMouseButton(0)) {
            Vector3[] vectors = new Vector3[4];
            vectors[0] = gridManager.GridToPosition(rect[0].Item1, rect[0].Item2) + Vector3.up - Vector3.forward;
            vectors[1] = new Vector3((gridManager.GridToPosition(mouse.Item1, mouse.Item2) + Vector3.right).x, (gridManager.GridToPosition(rect[0].Item1, rect[0].Item2) + Vector3.up).y, -1);
            vectors[2] = gridManager.GridToPosition(mouse.Item1, mouse.Item2) + Vector3.right - Vector3.forward;
            vectors[3] = new Vector3(gridManager.GridToPosition(rect[0].Item1, rect[0].Item2).x, gridManager.GridToPosition(mouse.Item1, mouse.Item2).y, -1);
            lineRenderer.SetPositions(vectors);     
        }
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C)) {
        }
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V)) {
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
