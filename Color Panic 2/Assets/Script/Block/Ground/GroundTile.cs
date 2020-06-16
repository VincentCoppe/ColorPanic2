using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : TileGameObject
{
    [SerializeField] private BlockGround block;

   
    public override void SetBlock()
    {
        Block = block;
    }
}
