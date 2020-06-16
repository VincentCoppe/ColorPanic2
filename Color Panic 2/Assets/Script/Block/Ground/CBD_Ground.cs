using UnityEngine;

public class CBD_Ground : CustomBlockData { 
    [SerializeField] private SpriteRenderer[] _corners = new SpriteRenderer[8];
    
    public SpriteRenderer[] Corners { get { return _corners; } }
}