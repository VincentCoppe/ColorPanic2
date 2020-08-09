using UnityEngine;

public class CBD_Jumper : CBD_Object {
    [SerializeField] private Transform _jumperTransform = null;

    public Transform JumperTransform { get { return _jumperTransform; } }
}