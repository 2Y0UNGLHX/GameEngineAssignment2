using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    MT_1,
    MT_2,
    MT_None
}


public class MonsterFactory : MonoBehaviour
{
    public GameObject[] monsterPrefabs;

    public GameObject CreateMonster(MonsterType type)
    {
        switch (type)
        {
            case MonsterType.MT_1:
                return Instantiate(monsterPrefabs[0]);
            case MonsterType.MT_2:
                return Instantiate(monsterPrefabs[1]);
            case MonsterType.MT_None:
                return null;
            default:
                return null;
        }
    }
}
