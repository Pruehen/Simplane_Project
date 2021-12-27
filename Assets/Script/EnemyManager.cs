using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        for (int i = 0; i < 5; i++)
        {
            CreateEnemy();
        }
    }

    float level = 1;
    float levelcount;

    // Update is called once per frame
    void Update()
    {
        levelcount += Time.deltaTime;

        if (levelcount > (100/(10+level)))
        {
            CreateEnemy();
            level++;
            levelcount = 0;

            Debug.Log(level);
        }
    }

    void CreateEnemy()
    {
        GameObject createdEnemy = Instantiate(enemyPrefab);
        createdEnemy.name = "Enemy";
        createdEnemy.transform.position = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100)).normalized * Random.Range(200, 400) + player.transform.position;
    }
}
