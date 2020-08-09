using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformX : MonoBehaviour
{
    [SerializeField] private float speed = 0.25f;
    private Transform m_RightCheck;
    private Transform m_LeftCheck; 
    private bool left = false;
    const float k_GroundedRadius = 0.2f;
    [SerializeField] private LayerMask m_WhatIsGround; 
    private GameObject target;

    void Start()
    {
        m_LeftCheck = transform.Find("LeftWallCheck");
        m_RightCheck = transform.Find("RightWallCheck");
        target = null;
    }

    private void FixedUpdate() {
        HandleRightCheck();
        HandleLeftCheck();
        if (left){
            this.transform.localPosition = new Vector3(this.transform.localPosition.x+speed, this.transform.localPosition.y, this.transform.localPosition.z);
            if (target != null) target.transform.localPosition = new Vector3(target.transform.localPosition.x+speed, target.transform.localPosition.y, target.transform.localPosition.z);
        } else {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x-speed, this.transform.localPosition.y, this.transform.localPosition.z);
            if (target != null) target.transform.localPosition = new Vector3(target.transform.localPosition.x-speed, target.transform.localPosition.y, target.transform.localPosition.z);
        }
    }

    private void HandleRightCheck(){    
        Collider2D[] collidersR = Physics2D.OverlapCircleAll(m_RightCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < collidersR.Length; i++)
        {
            if (collidersR[i].gameObject != gameObject)
            {   
                left = false;
            }
        }
    }

    private void HandleLeftCheck(){
        Collider2D[] collidersL = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < collidersL.Length; i++)
        {
            if (collidersL[i].gameObject != gameObject)
            {
                left = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && ((other.gameObject.transform.localPosition.y - this.gameObject.transform.localPosition.y) > 1.1f)){
            target = other.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            target = null;
        }
    }


}
