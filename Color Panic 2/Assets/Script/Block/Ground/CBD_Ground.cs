using UnityEngine;

public class CBD_Ground : CustomBlockData { 
    [SerializeField] private SpriteRenderer[] _corners = new SpriteRenderer[8];
    [SerializeField] private SpriteRenderer[] _center = new SpriteRenderer[2];

    public SpriteRenderer[] Center { get { return _center; } }
    public SpriteRenderer[] Corners { get { return _corners; } }

    public Sprite[] Brick1 = new Sprite[5];
    public Sprite[] Brick2 = new Sprite[5];
    public Sprite[] BrickCorner = new Sprite[2];

    public Sprite[] Station = new Sprite[2];
    public Sprite[] StationCorner = new Sprite[2];

}