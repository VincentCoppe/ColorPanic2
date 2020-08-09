using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class PlayerControllerTest
{
    [Test]
    public void Player_AddPower_Red(){
        PlayerController pc = new PlayerController();
        MyPower power = new MyPower();
        pc.power = power;
        Power red = new Red();
        pc.power.AddPower(red);
        Assert.True(pc.power.LastPower == "Red");
    }

    [Test]
    public void Player_AddPower_Green(){
        PlayerController pc = new PlayerController();
        MyPower power = new MyPower();
        pc.power = power;
        Power red = new Green();
        pc.power.AddPower(red);
        Assert.True(pc.power.LastPower == "Green");
    }

    [Test]
    public void Player_RemovePower(){
        PlayerController pc = new PlayerController();
        MyPower power = new MyPower();
        pc.power = power;
        pc.power.LastPower = "Green";
        pc.power.ResetPowers();
        Assert.True(pc.power.LastPower == null);
    }

    [Test]
    public void Player_HavePower_Red(){
        PlayerController pc = new PlayerController();
        MyPower power = new MyPower();
        pc.power = power;
        Power red = new Red();
        pc.power.AddPower(red);
        Assert.True(pc.power.HavePower("Red") == true);
    }

    [Test]
    public void Player_HavePower_Green(){
        PlayerController pc = new PlayerController();
        MyPower power = new MyPower();
        pc.power = power;
        Power red = new Green();
        pc.power.AddPower(red);
        Assert.True(pc.power.HavePower("Green") == true);
    }

    [Test]
    public void Player_Initial_Keys(){
        PlayerController pc = new PlayerController();
        Assert.True(pc.keys == 0);
    }

    [Test]
    public void Player_KeyIncrease(){
        PlayerController pc = new PlayerController();
        pc.IncreaseKeys();
        Assert.True(pc.keys == 1);
    }

    [Test]
    public void Player_Recharge_Green(){
        PlayerController pc = new PlayerController();
        pc.Djump = false;
        pc.Recharge("Green");
        Assert.True(pc.Djump == true);
    }

    [Test]
    public void Player_Recharge_Red(){
        PlayerController pc = new PlayerController();
        pc.dash = false;
        pc.Recharge("Red");
        Assert.True(pc.dash == true);
    }

}
