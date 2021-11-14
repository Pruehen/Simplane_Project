using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    RectTransform compass;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        compass = GameObject.Find("Compass").GetComponent<RectTransform>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CompassSystem();
    }

    void CompassSystem()
    {
        compass.rotation = Quaternion.Euler(0, 0, -player.transform.rotation.z);
    }

    void MaingunFireing()
    {

    }
}
