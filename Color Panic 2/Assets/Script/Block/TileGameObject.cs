using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGameObject : MonoBehaviour
{
    public BlockEnum Block;
    
    [SerializeField] private CustomBlockData data;
    public CustomBlockData Data { get { return data; } }
}
