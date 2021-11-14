using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    private Joystick joystick;
    private VirtualJoystick virtualjoystick;

    Rigidbody rb;

    float enginePower;
    float mobility;

    float PowerConstant = 1;
    float mobilityConstant = 2;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.tag == "Player")
        {
            joystick = GameObject.FindObjectOfType<Joystick>();
        }
        else
        {
            virtualjoystick = this.gameObject.GetComponent<VirtualJoystick>();
        }
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        enginePower = gameObject.GetComponent<Status>().EnginePower;
        mobility = gameObject.GetComponent<Status>().Mobility;
        if (joystick == null)
        {
            virtualjoystick = this.gameObject.GetComponent<VirtualJoystick>();
            Debug.Log(gameObject.name);
        }
    }

    void FixedUpdate()
    {
        MoveControl();
    }


    private void MoveControl()
    {
        if (gameObject.tag == "Player")
        {
            Vector3 upMovement = transform.up * Time.deltaTime * joystick.Vertical;
            Vector3 rightMovement = -Vector3.forward * Time.deltaTime * joystick.Horizontal;


            rb.AddForce((transform.up * 15 * Time.deltaTime + upMovement * 10) * enginePower * PowerConstant);
            rb.AddForce(transform.right * 1f * Time.deltaTime * joystick.Horizontal * mobility * mobilityConstant);
            rb.AddTorque(rightMovement * mobility * mobilityConstant);
        }
        else
        {
            Vector3 upMovement = transform.up * Time.deltaTime * virtualjoystick.Vertical;
            Vector3 rightMovement = -Vector3.forward * Time.deltaTime * virtualjoystick.Horizontal;


            rb.AddForce((transform.up * 15 * Time.deltaTime + upMovement * 10) * enginePower * PowerConstant);
            rb.AddForce(transform.right * 1f * Time.deltaTime * virtualjoystick.Horizontal * mobility * mobilityConstant);
            rb.AddTorque(rightMovement * mobility * mobilityConstant);
        }
    }


    public Vector3 Getspeed
    {
        get { return rb.velocity; }
    }
}
