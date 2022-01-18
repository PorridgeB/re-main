using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : Room
{
    public ObjectPool enemyPool;
    public RunInfoHistory runInfoHistory;
    public List<EnemySpawn> spawns;
    [SerializeField]
    private float budget;

    public void SetBudget(float value)
    {
        budget = value * runInfoHistory.Current.difficulty;
    }

    public override void Generate()
    {
        base.Generate();
        GenerateStuff();
    }

    public void GenerateStuff()
    {
        SetBudget(50);
        foreach (EnemySpawn s in spawns) 
        {
            Enemy enemy = enemyPool.GetRandom().GetComponent<Enemy>();
            //if (s.GetEnemyType() != EnemyType.None)
            //{
            //    while (enemy.type != s.GetEnemyType())
            //    {
            //        enemy = enemyPool.GetRandom().GetComponent<Enemy>();
            //    }
            //}
            Instantiate(enemy, s.transform.position, new Quaternion(), transform);
            budget -= enemy.unitCost;
            if (budget <= 0)
            {
                return;
            }
        }
    }
}
