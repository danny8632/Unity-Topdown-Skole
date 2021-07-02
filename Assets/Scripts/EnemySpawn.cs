using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject enemyPrefab;

    private bool hasSpawnedAgain = false;

    public void spawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, new Quaternion());
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.waypoints = waypoints;
    }

    private void Start()
    {
        spawnEnemy();
    }

    private void Update()
    {
        if(GameController.instance.IsFlagTaken() && hasSpawnedAgain == false)
        {
            spawnEnemy();
            hasSpawnedAgain = true;
        }
    }
}
