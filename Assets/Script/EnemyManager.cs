using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject createdEnemy = GameObject.Instantiate(enemyPrefab);
            createdEnemy.transform.position = new Vector3(Random.Range(-200, 200), Random.Range(-200, 200));
        }

        InvokeRepeating("CreateEnemy", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        GameObject createdEnemy = GameObject.Instantiate(enemyPrefab);
        createdEnemy.transform.position = new Vector3(Random.Range(-200, 200), Random.Range(-200, 200));
    }
}
