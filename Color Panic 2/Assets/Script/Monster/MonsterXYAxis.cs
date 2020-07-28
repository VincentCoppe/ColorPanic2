using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterXYAxis : MonoBehaviour
{   
    private bool left = false;
    private bool top = false;
    private Transform m_TopCheck;
    private Transform m_BotCheck; 

    private Transform m_RightCheck;
    private Transform m_LeftCheck; 
    private SpriteRenderer sr;
    const float k_GroundedRadius = 0.2f;
    [SerializeField] private LayerMask m_WhatIsGround; 
    [SerializeField] private float speedX = 0.1f; 
    [SerializeField] private float speedY = 0.1f; 
    
    
    private Rigidbody2D m_Rigidbody2D;


    private void Awake()
    {
        m_LeftCheck = transform.Find("LeftWallCheck");
        m_RightCheck = transform.Find("RightWallCheck");
        m_TopCheck = transform.Find("TopCheck");
        m_BotCheck = transform.Find("BotCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        m_Rigidbody2D.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        HandleRightCheck();
        HandleLeftCheck();
        HandleTopCheck();
        HandleBotCheck();
        if (left){
            this.transform.localPosition = new Vector3(this.transform.localPosition.x+speedX, this.transform.localPosition.y, this.transform.localPosition.z);
        } else {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x-speedX, this.transform.localPosition.y, this.transform.localPosition.z);
        }

        if (top){
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y+speedY, this.transform.localPosition.z);
        } else {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y-speedY, this.transform.localPosition.z);
        }
        sr.flipX = left;
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

    private void HandleTopCheck(){
        Collider2D[] collidersR = Physics2D.OverlapCircleAll(m_TopCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < collidersR.Length; i++)
        {
            if (collidersR[i].gameObject != gameObject)
            {   
                top = false;
            }
        }
    }

    private void HandleBotCheck(){
        Collider2D[] collidersL = Physics2D.OverlapCircleAll(m_BotCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < collidersL.Length; i++)
        {
            if (collidersL[i].gameObject != gameObject)
            {
                top = true;
            }
        }
    }
}