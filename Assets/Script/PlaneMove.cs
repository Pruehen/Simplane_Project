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

    float PowerConstant = 1f;
    float mobilityConstant = 4;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.tag == "Player")
        {
            joystick = GameObject.FindObjectOfType<Joystick>();
        }
    }

    void Start()
    {
        Invoke("Init", Time.deltaTime);
    }

    void Init()
    {
        enginePower = gameObject.GetComponent<Status>().EnginePower;
        mobility = gameObject.GetComponent<Status>().Mobility;


        if (gameObject.tag == "Enemy")
        {
            virtualjoystick = null;
            virtualjoystick = this.gameObject.GetComponent<VirtualJoystick>();
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


            rb.AddForce((transform.up * 30 * Time.deltaTime + upMovement * 10) * enginePower * PowerConstant);
            rb.AddForce(transform.right * 2 * Time.deltaTime * joystick.Horizontal * mobility * mobilityConstant);
            rb.AddTorque(rightMovement * mobility * mobilityConstant);
        }
        else if (gameObject.tag == "Enemy" && virtualjoystick != null)
        {
            Vector3 upMovement = transform.up * Time.deltaTime * virtualjoystick.Vertical;
            Vector3 rightMovement = -Vector3.forward * Time.deltaTime * virtualjoystick.Horizontal;


            rb.AddForce((transform.up * 30 * Time.deltaTime + upMovement * 10) * enginePower * PowerConstant);
            rb.AddForce(transform.right * 1f * Time.deltaTime * virtualjoystick.Horizontal * mobility * mobilityConstant);
            rb.AddTorque(rightMovement * mobility * mobilityConstant);
        }
    }


    public Vector3 Getspeed
    {
        get { return rb.velocity; }
    }
}
