using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] public Animator animator = null;
    public string SavedPowers;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            Vector3 pos = transform.localPosition;
            other.gameObject.SendMessage("SetRespawn",this);
        }
    }

    public void Activation(bool activation){
        this.GetComponent<Animator>().SetBool("Activate", activation);
    }



}
