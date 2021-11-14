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
        input.Set(0.5f, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
