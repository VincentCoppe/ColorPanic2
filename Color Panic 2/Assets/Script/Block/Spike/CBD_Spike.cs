using UnityEngine;

public class CBD_Spike : CustomBlockData {
    [SerializeField] private Transform _spikeTransform = null;

    public Transform SpikeTransform { get { return _spikeTransform; } }
}