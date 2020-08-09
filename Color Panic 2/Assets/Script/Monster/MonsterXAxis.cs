using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterXAxis : MonoBehaviour
{   
    private bool left = false;
    private Transform m_RightCheck;
    private Transform m_LeftCheck; 
    private SpriteRenderer sr;
    const float k_GroundedRadius = 0.2f;
    [SerializeField] private LayerMask m_WhatIsGround; 
    [SerializeField] private float speed = 0.1f; 
    [SerializeField] private bool gravity = true;
    
    
    private Rigidbody2D m_Rigidbody2D;


    private void Awake()
    {
        m_LeftCheck = transform.Find("LeftWallCheck");
        m_RightCheck = transform.Find("RightWallCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (gravity == false) m_Rigidbody2D.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        HandleRightCheck();
        HandleLeftCheck();
        if (left){
            this.transform.localPosition = new Vector3(this.transform.localPosition.x+speed, this.transform.localPosition.y, this.transform.localPosition.z);
        } else {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x-speed, this.transform.localPosition.y, this.transform.localPosition.z);
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
}