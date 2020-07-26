using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterYAxis : MonoBehaviour
{   
    private bool top = false;
    private Transform m_TopCheck;
    private Transform m_BotCheck; 
    private SpriteRenderer sr;
    const float k_Radius = 0.4f;
    [SerializeField] private LayerMask m_WhatIsGround; 
    [SerializeField] private float speed = 0.05f; 
    
    
    private Rigidbody2D m_Rigidbody2D;


    private void Awake()
    {
        m_TopCheck = transform.Find("TopCheck");
        m_BotCheck = transform.Find("BotCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.gravityScale = 0;
    }

    private void FixedUpdate()
    {
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