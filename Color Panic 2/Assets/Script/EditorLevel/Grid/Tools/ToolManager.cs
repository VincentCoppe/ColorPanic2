using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private TileGameObject tile = null;
    [SerializeField] private Size size = null;
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
        Tool?.Action(gridManager, tile, size.size, mouse);
    }

}
