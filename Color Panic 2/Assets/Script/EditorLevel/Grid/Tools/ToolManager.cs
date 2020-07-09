using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] private TileGameObject tile = null;
    [SerializeField] private Size size = null;
    [SerializeField] private HoverBlock hoverBlock = null;
    private Sprite sprite;
    public static ITool Tool;

    protected void SetTool(ITool tool) {
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
        //sprite = Resources.Load<Sprite>("Sprite/Tools/tools.png");
        //Cursor.SetCursor(sprite.texture, Vector2.zero, CursorMode.ForceSoftware);
        hoverBlock.DisplayCells(gridManager, Tool.GetBlocksToHover(gridManager, tile, size.size, mouse));
    }

    public void CleanHover() {
        hoverBlock.CleanCells();
    }

}
