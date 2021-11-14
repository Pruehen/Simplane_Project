using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private PlaneMove player;

    Vector3 playerSpeed;
    Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Cloudmove();
    }

    void Cloudmove()
    {
        transform.position = player.transform.position;

        Vector3 deltaOffset = material.mainTextureOffset;
        playerSpeed = player.Getspeed * Time.deltaTime * 0.015f;
        deltaOffset.Set(deltaOffset.x + playerSpeed.x, deltaOffset.y + playerSpeed.y, 0);


        if (deltaOffset.x >= 2 || deltaOffset.x <= -2) { deltaOffset.x = 0; }
        if (deltaOffset.y >= 2 || deltaOffset.y <= -2) { deltaOffset.y = 0; }

        material.mainTextureOffset = deltaOffset;
    }
}
