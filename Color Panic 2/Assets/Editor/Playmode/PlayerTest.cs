using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTest
    {
        [Test]
        public void Player_MoveX()
        {
            GameObject go = new GameObject();
            go.AddComponent<Rigidbody2D>();
            PlayerController pc = go.AddComponent<PlayerController>(); 
            pc.MoveX(-1);
            Vector3 v = new Vector3(1,0,0);
            Assert.True(pc.gameObject.transform.localPosition == v);
        }

        [Test]
        public void Player_MoveY()
        {
            GameObject go = new GameObject();
            go.AddComponent<Rigidbody2D>();
            PlayerController pc = go.AddComponent<PlayerController>(); 
            pc.MoveY(-1);
            Vector3 v = new Vector3(0,1,0);
            Assert.True(pc.gameObject.transform.localPosition == v);
        }

        [Test]
        public void Player_Respawn()
        {
            GameObject go = new GameObject();
            go.AddComponent<Rigidbody2D>();
            PlayerController pc = go.AddComponent<PlayerController>(); 
            pc.MoveY(-1);
            Vector3 v = new Vector3(0,0,0);
            Assert.True(pc.respawn == v);
        }

        [Test]
        public void Player_Set_Respawn()
        {
            GameObject go1 = new GameObject();
            GameObject go2 = new GameObject();
            go1.AddComponent<Rigidbody2D>();
            go2.AddComponent<Animator>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            Checkpoint ch = go2.AddComponent<Checkpoint>();
            ch.gameObject.transform.localPosition = new Vector3(10,10,10);
            pc.SetRespawn(ch);
            pc.MoveY(-1);
            Vector3 v = new Vector3(10,10,10);
            Assert.True(pc.respawn == v);
        }

        [Test]
        public void Player_Gravity_Scale()
        {
            GameObject go1 = new GameObject();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            Assert.True(pc.gravity > 0);
        }

        [Test]
        public void Player_Gravity_Scale_Reverse()
        {
            GameObject go1 = new GameObject();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            pc.GravityReverse();
            Assert.True(pc.gravity < 0);
        }

        [Test]
        public void Player_Open_Without_Keys()
        {
            GameObject go1 = new GameObject();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            GameObject go2 = new GameObject();
            go2.AddComponent<Door>(); 
            pc.Open(go2);
            Assert.True(pc.keys == 0);
        }

        [Test]
        public void Player_Open_With_Keys()
        {
            GameObject go1 = new GameObject();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            pc.IncreaseKeys();
            pc.IncreaseKeys();
            GameObject go2 = new GameObject();
            go2.AddComponent<Door>(); 
            pc.Open(go2);
            Assert.True(pc.keys == 1);
        }

        [Test]
        public void Player_Opens_With_Keys()
        {
            GameObject go1 = new GameObject();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            pc.IncreaseKeys();
            pc.IncreaseKeys();
            GameObject go2 = new GameObject();
            go2.AddComponent<Door>(); 
            GameObject go3 = new GameObject();
            go3.AddComponent<Door>(); 
            pc.Open(go2);
            pc.Open(go3);
            Assert.True(pc.keys == 0);
        }

        [Test]
        public void Player_Teleport_Right()
        {
            GameObject go1 = new GameObject();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            pc.Teleport();
            Assert.True(pc.gameObject.transform.localPosition.x == 2.5f);
        }

        [Test]
        public void Player_Teleport_Left()
        {
            GameObject go1 = new GameObject();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            pc.FacingRight = false;
            pc.Teleport();
            Assert.True(pc.gameObject.transform.localPosition.x == -2.5f);
        }

        [Test]
        public void Player_No_Movement()
        {
            GameObject go1 = new GameObject();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            Assert.True(rigid.velocity == new Vector2(0,0));
        }

        [Test]
        public void Player_Push()
        {
            GameObject go1 = new GameObject();
            go1.AddComponent<Animator>();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            rigid.velocity = new Vector2(10,10);
            Assert.True(rigid.velocity == new Vector2(10,10));
        }

        [Test]
        public void Player_Reset_Movement()
        {
            GameObject go1 = new GameObject();
            go1.AddComponent<Animator>();
            Rigidbody2D rigid = go1.AddComponent<Rigidbody2D>();
            PlayerController pc = go1.AddComponent<PlayerController>(); 
            rigid.velocity = new Vector2(10,10);
            pc.ResetMovement();
            Assert.True(rigid.velocity == new Vector2(0,0));
        }

    }
}
