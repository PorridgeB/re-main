using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class Trap : Room
{
    [SerializeField]
    private List<Doorway> doors;
    private bool activated;
    private bool complete;
    public ObjectPool enemyPool;
    public RunInfoHistory runInfoHistory;
    public List<EnemySpawn> spawns;
    [SerializeField]
    private float budget;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    public void SetBudget(float value)
    {
        budget = value * runInfoHistory.Current.difficulty;
    }

    public override void Generate()
    {
        base.Generate();
        foreach (Doorway d in GetComponentsInChildren<Doorway>())
        {
            d.Open();
            doors.Add(d);
        }
    }

    private void Update()
    {
        if (!activated)
        {
            activated = PlayerInRoom();
            if (activated)
            {
                CloseAll();
                SpawnEncounter();
            }
        }
        else if (!complete)
        {
            if (RoomComplete())
            {
                OpenAll();
                complete = true;
            }
        }
    }

    private bool RoomComplete()
    {
        foreach (GameObject g in enemies)
        {
            if (g != null)
            {
                return false;
            }
        }
        return true;
    }

    public void SpawnEncounter()
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
            enemies.Add(Instantiate(enemy.gameObject, s.transform.position, new Quaternion(), transform));
            budget -= enemy.unitCost;
            if (budget <= 0)
            {
                return;
            }
        }
    }

    private bool PlayerInRoom()
    {
        foreach (Doorway d in doors)
        {
            if (d.playerInRoom)
            {
                return true;
            }
        }
        return false;
    }
    private void CloseAll()
    {
        foreach (Doorway d in doors)
        {
            d.Close();
        }
    }

    private void OpenAll()
    {
        foreach (Doorway d in doors)
        {
            d.Open();
        }
    }
}
