using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : TileGameObject
{
    [SerializeField] private BlockSpike block;

    public override void SetBlock()
    {
        Block = block;
    }
}
