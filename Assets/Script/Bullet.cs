using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
    Rigidbody rb;
    float dmg;
    float lifeTime = 0;


    public void Init(float setDmg)
    {
        dmg = setDmg;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void DestroyBullet()
    {
        lifeTime = 0;
        rb.velocity = Vector3.zero;
        BulletManager.ReturnBullet(this);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime > 1)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag != "Ammo")
        {
            target.GetComponent<Status>().Damage(dmg);
            DestroyBullet();
        }
    }
}
