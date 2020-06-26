using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] public Animator animator = null;
    [SerializeField] public Color Viridian;
    [SerializeField] public Color Red;
    [SerializeField] public Color Green;
    public string SavedPowers;

    //When the player touch the checkpoint, set it as the new respawn
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            Vector3 pos = transform.localPosition;
            other.gameObject.SendMessage("SetRespawn",this);
        }
    }
    
    //Animation of checkpoint
    public void Activation(bool activation){
        this.GetComponent<Animator>().SetBool("Activate", activation);
        SetColor(activation);
    }

    //Change color depending on the power saved in the checkpoint
    private void SetColor(bool activation){
        Renderer rend = gameObject.GetComponent<Renderer>();
        if (!activation) {
            rend.material.color = Color.white;
            return;
        } 
        switch(SavedPowers){
            case "Green" : rend.material.color = Green; break;
            case "Red" : rend.material.color = Red; break;
            case "Viridian" : rend.material.color = Viridian; break;
        }
    }



}
