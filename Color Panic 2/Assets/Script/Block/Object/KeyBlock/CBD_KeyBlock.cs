using UnityEngine;

public class CBD_KeyBlock : CBD_Object
{
    [SerializeField] private bool _door = false;

    public bool Door { get { return _door; } }
}