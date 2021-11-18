using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    float gun_Dmg = 10;
    float gun_Velocity = 1000;
    float gun_Rpm = 1200;

    float mis_Dmg = 100;
    float mis_Velocity = 100;
    float mis_Mobility = 40;
    public float Mis_Dmg { get { return mis_Dmg; } }
    public float Mis_Velocity { get { return mis_Velocity; } }
    public float Mis_Mobility { get { return mis_Mobility; } }


    Maingun maingun;

    // Start is called before the first frame update
    void Start()
    {
        maingun = transform.Find("Maingun").GetComponent<Maingun>();
        maingun.Init(gun_Dmg, gun_Velocity, gun_Rpm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
