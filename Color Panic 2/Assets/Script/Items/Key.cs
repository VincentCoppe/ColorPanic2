using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            other.gameObject.SendMessage("IncreaseKeys");
            gameObject.SetActive(false);
        }
    }
}
