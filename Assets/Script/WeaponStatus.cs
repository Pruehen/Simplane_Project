using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    public static WeaponStatus instance;

    Maingun maingun;


    public AAM_Status aam9_Status = new AAM_Status(100, 80, 80, 300, 60);
    public Vulkan_Status vulkan_Status = new Vulkan_Status(20, 1000, 1500);
    public AAM_Status aam7_Status = new AAM_Status(200, 100, 60, 400, 10);

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public struct AAM_Status
    {
        float mis_Dmg;
        float mis_Velocity;
        float mis_Mobility;
        float mis_MaxRange;
        float mis_MaxAngle;
        public float Mis_Dmg { get { return mis_Dmg; } }
        public float Mis_Velocity { get { return mis_Velocity; } }
        public float Mis_Mobility { get { return mis_Mobility; } }
        public float Mis_MaxRange { get { return mis_MaxRange; } }
        public float Mis_MaxAngle { get { return mis_MaxAngle; } }

        public AAM_Status(float d, float v, float m, float mr, float ma)
        {
            mis_Dmg = d;
            mis_Velocity = v;
            mis_Mobility = m;
            mis_MaxRange = mr;
            mis_MaxAngle = ma;
        }
    }

    public struct Vulkan_Status
    {
        float gun_Dmg;
        float gun_Velocity;
        float gun_Rpm;
        public float Gun_Dmg { get { return gun_Dmg; } }
        public float Gun_Velocity { get { return gun_Velocity; } }
        public float Gun_Rpm { get { return gun_Rpm; } }

        public Vulkan_Status(float d, float v, float r)
        {
            gun_Dmg = d;
            gun_Velocity = v;
            gun_Rpm = r;
        }
    }
}



