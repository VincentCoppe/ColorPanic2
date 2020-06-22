using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerHorizontal : MonoBehaviour
{
    public float MoveVector;
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            other.gameObject.SendMessage("MoveX",MoveVector);
        }
    }
}
