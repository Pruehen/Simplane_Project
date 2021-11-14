using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    Maingun maingun;

    bool isFireing = false;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "MaingunButton")
        {
            maingun = GameObject.Find("Maingun").GetComponent<Maingun>();
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
}
