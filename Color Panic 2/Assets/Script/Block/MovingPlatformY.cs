using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformY : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float distance = 5;
    private Vector3 InitialPosition;
    private bool top = true;

    void Start()
    {
        InitialPosition = this.transform.position;
    }

    private void FixedUpdate() {
        if (top){
            this.transform.position = new Vector3(InitialPosition.x, this.transform.position.y+speed, InitialPosition.z);
        } else {
            this.transform.position = new Vector3(InitialPosition.x, this.transform.position.y-speed, InitialPosition.z);
        }

        if (this.transform.position.y - InitialPosition.y > 5){
            top = false;
        } else if (this.transform.position.y - InitialPosition.y < -5){
            top = true;
        }
    }

}
