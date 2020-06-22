﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            ApplyEffect();
            gameObject.SetActive(false);
        }
    }

    public abstract void ApplyEffect();
}
