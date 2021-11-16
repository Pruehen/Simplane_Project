using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    Maingun maingun;
    WeaponSystem weaponSystem;

    bool isFireing = false;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "MaingunButton")
        {
            maingun = GameObject.Find("Maingun").GetComponent<Maingun>();
        }
        else if(gameObject.name == "MissileButton")
        {
            weaponSystem = GameObject.Find("WeaponSystem").GetComponent<WeaponSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnClick() 
    {
        if (gameObject.name == "MaingunButton")
        {
            isFireing = !isFireing;
            maingun.SetisFireing(isFireing);
        }
    }

    public void OnPointDown()
    {
        if (gameObject.name == "MissileButton")
        {
            weaponSystem.IsFireReady = true;
        }
    }

    public void OnPointUp()
    {
        if (gameObject.name == "MissileButton")
        {
            weaponSystem.IsFire = true;
        }
    }
}
