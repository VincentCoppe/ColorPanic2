using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Palette : MonoBehaviour
{
    [SerializeField] protected TileGameObject _tileToChange;
    [SerializeField] protected MasterPalette MasterPalette;
    public TileGameObject TileToChange { get { return _tileToChange; } }

    public virtual void ChangeTile(ToolManager toolManager) {
        toolManager.setTile(TileToChange);
        MasterPalette.updateActivePalette(GetComponent<Image>());
    }
}
