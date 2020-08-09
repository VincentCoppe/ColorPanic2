using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBD_Platform : CBD_Object
{
    [SerializeField] private Sprite[] _center = null;
    [SerializeField] private SpriteRenderer sprite = null;

    public Sprite[] Center { get { return _center; }}

    public void changeSprite(Sprite a)
    {
        sprite.sprite = a;
    }
}
