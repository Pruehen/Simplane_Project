using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    float hp = 1;
    float enginePower;
    float mobility;
    int score;
    public float Hp { get { return hp; } }
    public float EnginePower { get { return enginePower; } }
    public float Mobility { get { return mobility; } }
    public int Score { get { return score; } }



    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == PlaneStatus.instance.enemyStatus.PlaneName)
        {
            hp = (PlaneStatus.instance.enemyStatus.Hp);
            enginePower = (PlaneStatus.instance.enemyStatus.EnginePower);
            mobility = (PlaneStatus.instance.enemyStatus.Mobility);
            score = (PlaneStatus.instance.enemyStatus.Score);
        }
    }

    private void Awake()
    {
        if (this.gameObject.tag == "Player")
        {
            hp = (PlaneStatus.instance.playerStatus.Hp);
            enginePower = (PlaneStatus.instance.playerStatus.EnginePower);
            mobility = (PlaneStatus.instance.playerStatus.Mobility);
            score = (PlaneStatus.instance.enemyStatus.Score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            PlaneDown();
        }
    }

    void PlaneDown()
    {
        UIManager.instance.UpScore(score);
        GameObject.Destroy(gameObject);
    }

    public void Damage(float dmg)
    {
        hp -= dmg;
    }
}
