using UnityEngine;

public class CBD_Jumper : CustomBlockData {
    [SerializeField] private Transform _jumperTransform = null;

    public Transform JumperTransform { get { return _jumperTransform; } }
}