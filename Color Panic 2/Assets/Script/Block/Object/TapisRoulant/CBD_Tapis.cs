using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBD_Tapis : CBD_Object
{
    [SerializeField] private SpriteRenderer[] _corners = new SpriteRenderer[8];
    [SerializeField] private Transform _center = null;

    public Transform Center { get { return _center; } }
    public SpriteRenderer[] Corners { get { return _corners; } }
}
