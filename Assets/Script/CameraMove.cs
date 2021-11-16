using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 offset = new Vector3(0, 40, -10);
    float followSpeed = 0.05f;


    private GameObject player;
    private PlaneMove playerMove;
    private Camera camera;

    void Awake()
    {
        player = GameObject.Find("Player");
        playerMove = player.GetComponent<PlaneMove>();
        camera = gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 camera_pos = player.transform.position + player.transform.up * offset.y + new Vector3(0, 0, offset.z);
        Vector3 lerp_pos = Vector3.Lerp(transform.position, camera_pos, followSpeed);
        transform.position = lerp_pos;

        Quaternion camera_rot = player.transform.localRotation;
        Quaternion lerp_rot = Quaternion.Lerp(transform.rotation, camera_rot, 0.1f);
        transform.rotation = lerp_rot;

        float playerSpeed = playerMove.Getspeed.magnitude;
        camera.orthographicSize = 100 + (playerSpeed * 2);

        //transform.LookAt(player.transform);
    }
}
