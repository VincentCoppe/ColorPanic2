using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformY : MonoBehaviour
{
    private bool top = false;
    private Transform m_TopCheck;
    private Transform m_BotCheck; 
    const float k_Radius = 0.4f;
    [SerializeField] private LayerMask m_WhatIsGround; 
    [SerializeField] private float speed = 0.25f; 

    void Start()
    {
        m_TopCheck = transform.Find("TopWallCheck");
        m_BotCheck = transform.Find("BotWallCheck");
    }

    private void FixedUpdate() {
        HandleTopCheck();
        HandleBotCheck();
        if (top){
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y+speed, this.transform.localPosition.z);
        } else {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y-speed, this.transform.localPosition.z);
        }
    }

    private void HandleTopCheck(){
        Collider2D[] collidersR = Physics2D.OverlapCircleAll(m_TopCheck.position, k_Radius, m_WhatIsGround);
        for (int i = 0; i < collidersR.Length; i++)
        {
            if (collidersR[i].gameObject != gameObject)
            {   
                top = false;
            }
        }
    }

    private void HandleBotCheck(){
        Collider2D[] collidersL = Physics2D.OverlapCircleAll(m_BotCheck.position, k_Radius, m_WhatIsGround);
        for (int i = 0; i < collidersL.Length; i++)
        {
            if (collidersL[i].gameObject != gameObject)
            {
                top = true;
            }
        }
    }

}
