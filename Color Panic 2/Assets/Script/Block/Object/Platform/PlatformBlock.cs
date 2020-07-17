using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBlock : ObjectBlock
{
    public PlatformBlock(TileGameObject prefab) : base(prefab)
    {
    }

    protected override bool Spawn(int x, int y)
    {
        if (base.Spawn(x, y))
        {
            SetupCenter(x, y,true);
            return true;
        }
        return false;
    }

    private void SetupCenter(int x, int y, bool b)
    {
        var data = Data<CBD_Platform>(); 
        if(Manager.GridObject[x-1, y] is PlatformBlock && Manager.GridObject[x + 1, y] is PlatformBlock)
        {
            if (b)
            {
                ((PlatformBlock)Manager.GridObject[x - 1, y]).SetupCenter(x - 1, y, false);
                ((PlatformBlock)Manager.GridObject[x + 1, y]).SetupCenter(x + 1, y, false);
            }
            
            data.changeSprite(data.Center[0]);
        } else if(Manager.GridObject[x - 1, y] is PlatformBlock && !(Manager.GridObject[x + 1, y] is PlatformBlock))
        {
            if (b)
            {
                ((PlatformBlock)Manager.GridObject[x - 1, y]).SetupCenter(x - 1, y, false);
            }
            
            data.changeSprite(data.Center[1]);
        } else if(!(Manager.GridObject[x - 1, y] is PlatformBlock) && Manager.GridObject[x + 1, y] is PlatformBlock)
        {
            if (b)
            {
                ((PlatformBlock)Manager.GridObject[x + 1, y]).SetupCenter(x + 1, y, false);
            }
            
            data.changeSprite(data.Center[2]);
        } else if(!(Manager.GridObject[x - 1, y] is PlatformBlock) && !(Manager.GridObject[x + 1, y] is PlatformBlock))
        {
            
            data.changeSprite(data.Center[3]);
        }
     
    }

}
