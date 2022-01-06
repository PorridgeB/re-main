using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    GoundMelee,
    GroundRanged,
    FlyingMelee,
    FlyingRanged,
    Boss,
    None
}

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private EnemyType preferedEnemyType;

    public EnemyType GetEnemyType()
    {
        return preferedEnemyType;
    }
}
