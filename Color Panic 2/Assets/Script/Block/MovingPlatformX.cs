using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformX : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float distance = 5;
    private Vector3 InitialPosition;
    private bool left = true;

    void Start()
    {
        InitialPosition = this.transform.position;
    }

    private void FixedUpdate() {
        if (left){
            this.transform.position = new Vector3(this.transform.position.x+speed, InitialPosition.y, InitialPosition.z);
        } else {
            this.transform.position = new Vector3(this.transform.position.x-speed, InitialPosition.y, InitialPosition.z);
        }
        
        if (this.transform.position.x - InitialPosition.x > 5){
            left = false;
        } else if (this.transform.position.x - InitialPosition.x < -5){
            left = true;
        }
    }

}
