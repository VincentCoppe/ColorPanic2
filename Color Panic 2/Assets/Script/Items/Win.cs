using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            //other.gameObject.SendMessage("Win");
            Debug.Log("You win !");
            gameObject.SetActive(false);
        }
    }
}
