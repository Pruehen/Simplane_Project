using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    float hp;
    float enginePower;
    float mobility;
    public float Hp { get { return hp; } set { hp = value; } }
    public float EnginePower { get { return enginePower; } set { enginePower = value; } }
    public float Mobility { get { return mobility; } set { mobility = value; } }


    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        enginePower = 100;
        mobility = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
