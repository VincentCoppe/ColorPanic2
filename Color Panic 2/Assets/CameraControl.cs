using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraControl : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] TMP_Text Win;
    
    void Update()
    {
        float PlayerLoc = player.transform.localPosition.x;
        float CameraLoc = this.transform.localPosition.x;
        float Distance = CameraLoc - PlayerLoc;
        if (Distance < -17) {
            player.transform.localPosition = new Vector3(player.transform.localPosition.x+2, player.transform.localPosition.y, player.transform.localPosition.z);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x+35.31f, this.transform.localPosition.y, this.transform.localPosition.z);
        } else if (Distance > 17) {
            player.transform.localPosition = new Vector3(player.transform.localPosition.x-2, player.transform.localPosition.y, player.transform.localPosition.z);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x-35.31f, this.transform.localPosition.y, this.transform.localPosition.z);
        }

        if (player.win){
            Win.transform.gameObject.SetActive(true);
        }

    }
}
