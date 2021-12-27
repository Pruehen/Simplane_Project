using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneStatus : MonoBehaviour
{
    public static PlaneStatus instance;

    public Plane_Status playerStatus = new Plane_Status(500, 120, 150, 0, "Player");
    public Plane_Status enemyStatus = new Plane_Status(100, 100, 50, 1, "Enemy");

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public struct Plane_Status
    {
        float hp;
        float enginePower;
        float mobility;
        int score;
        string planeName;

        public float Hp { get { return hp; } }
        public float EnginePower { get { return enginePower; } }
        public float Mobility { get { return mobility; } }
        public int Score { get { return score; } }

        public string PlaneName { get { return planeName; } }

        public Plane_Status(float h, float e, float m, int s,  string name)
        {
            hp = h;
            enginePower = e;
            mobility = m;
            score = s;
            planeName = name;
        }
    }
}
