using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyEffect", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyEffect()
    {
        GameObject.Destroy(gameObject);
    }
}
