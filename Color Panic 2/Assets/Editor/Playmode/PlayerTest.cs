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

    }
}
