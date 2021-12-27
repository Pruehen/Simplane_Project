using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    private Vector3 input;

    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SetJoystick(Vector3 vector3)
    {
        vector3 = new Vector3(Mathf.Clamp(vector3.x, -1, 1), Mathf.Clamp(vector3.y, -1, 1));
        input = vector3;
    }
}
