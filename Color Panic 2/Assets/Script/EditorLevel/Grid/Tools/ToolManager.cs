using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] private TileGameObject tile = null;
    [SerializeField] private Size size = null;
    [SerializeField] private HoverBlock hoverBlock = null;
    public static Tool Tool;

    protected void SetTool(Tool tool) {
        Tool?.SetBgColor(new Color(1,1,1));
        Tool = tool;
        Tool.SetBgColor(new Color(0,1,0));
    }

    public void setTile(TileGameObject t)
    {
        tile = t;
    }

    public void Action(GridManager gridManager, (int,int) mouse) {
        Tool.Action(gridManager, tile, size.size, mouse);
        
    }

    public void EndAction() {
        Tool.EndAction();
    }

    public void DisplayHover(GridManager gridManager, (int,int) mouse) {
        hoverBlock.DisplayCells(gridManager, tile, Tool.GetBlocksToHover(gridManager, size.size, mouse));
    }

    public void CleanHover() {
        hoverBlock.CleanCells();
    }

}
