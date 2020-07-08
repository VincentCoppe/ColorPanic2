using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class PlayerControllerTest
{
    [Test]
    public void PlayerPowerTest(){
        PlayerController pc = new PlayerController();
        MyPower power = new MyPower();
        pc.power = power;
        Power red = new Red();
        pc.power.AddPower(red);
        Assert.True(pc.power.LastPower == "Red");
    }

    [Test]
    public void PlayerKeyTest(){
        PlayerController pc = new PlayerController();
        Assert.True(pc.keys == 0);
    }

    [Test]
    public void PlayerKeyIncreaseTest(){
        PlayerController pc = new PlayerController();
        pc.IncreaseKeys();
        Assert.True(pc.keys == 1);
    }
}
