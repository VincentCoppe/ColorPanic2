﻿using UnityEngine;
using System.Collections;

public class CBD_Powerup : CustomBlockData
{

    [SerializeField] private PowerupEnum _color = PowerupEnum.Orange;

    public PowerupEnum Color { get { return _color; } }
}
