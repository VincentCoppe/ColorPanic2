using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBD_Monsters : CBD_Object
{
    [SerializeField] private MonsterEnum _monsterEnum;

    public MonsterEnum MonsterEnum {  get { return _monsterEnum; } }
}
