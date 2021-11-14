using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
    Rigidbody rb;
    public void Shoot()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Invoke("DestroyBullet", 2f);
    }

    public void DestroyBullet()
    {
        rb.velocity = Vector3.zero;
        BulletManager.ReturnBullet(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag != "Ammo")
        {
            DestroyBullet();
        }
    }
}
