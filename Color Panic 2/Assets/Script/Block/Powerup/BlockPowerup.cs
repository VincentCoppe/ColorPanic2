using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPowerup : BlockBase
{
    private PowerupEnum _color = PowerupEnum.Orange;
    private CBD_Powerup _data;
    public PowerupEnum Color { get { return _color; } }


    public BlockPowerup(TileGameObject prefab) : base(prefab) {
    }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Powerup)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }

    

    public override long Save()
    { 
        var data = Data<CBD_Powerup>();
        _color = data.Color;
         return base.Save() | ((long)Color<<16);
    }
}
