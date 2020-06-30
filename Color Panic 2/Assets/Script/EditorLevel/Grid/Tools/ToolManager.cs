using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] private TileGameObject tile = null;
    [SerializeField] private Size size = null;
    [SerializeField] private HoverBlock hoverBlock = null;
    [SerializeField] private Transform hoverErase = null;
    [SerializeField] private Sprite sprite = null;
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
        //Cursor.SetCursor(sprite.texture, Vector2.zero, CursorMode.ForceSoftware);
        if(!(Tool is Erase))
            hoverBlock.DisplayCells(gridManager, tile.gameObject, Tool.GetBlocksToHover(gridManager, size.size, mouse));
        else 
            hoverBlock.DisplayCells(gridManager, hoverErase.gameObject, Tool.GetBlocksToHover(gridManager, size.size, mouse));
    }

    public void CleanHover() {
        hoverBlock.CleanCells();
    }

}
