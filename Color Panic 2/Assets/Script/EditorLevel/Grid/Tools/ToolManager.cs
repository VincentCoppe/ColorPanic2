using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] private TileGameObject ground = null;
    [SerializeField] private GridManager gridManager = null;
    public static Tool Tool;

    protected void SetTool(Tool tool) {
        Tool?.SetBgColor(new Color(1,1,1));
        Tool = tool;
        Tool.SetBgColor(new Color(0,1,0));
    }

    public void Action() {
        Tool?.Action(gridManager, ground);
    }

}
