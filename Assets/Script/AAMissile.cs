using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAMissile : MonoBehaviour
{
    GameObject target;
    public GameObject SetTarget { set { target = value; } }

    Rigidbody rb;
    Explosion explosionPrefab;
    RocketTail rocketTeil;

    float dmg;
    float velocity;
    float mobility;

    // Start is called before the first frame update
    void Start()
    {
        Fire();
        explosionPrefab = transform.GetChild(1).GetComponent<Explosion>();//미사일 하위에 있는 폭발 오브젝트의 클래스 컴포넌트
        rocketTeil = transform.GetChild(2).GetComponent<RocketTail>();
    }

    public void Init(float d, float v, float m)
    {
        dmg = d;
        velocity = v;
        mobility = m;
    }
    
    void Fire()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 40, ForceMode.Impulse);
        Invoke("SelfDestroyMissile", 6f);
    }

    void SelfDestroyMissile()
    {
        GameObject.Destroy(gameObject);
    }
    void HitMissile()
    {
        explosionPrefab.transform.SetParent(null);
        explosionPrefab.gameObject.SetActive(true);

        rocketTeil.transform.SetParent(null);
        rocketTeil.StopBurn();

        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.up * velocity * 100 * Time.deltaTime, ForceMode.Acceleration);

        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir = target.transform.position + target.transform.up * Mathf.Clamp(dir.magnitude * 0.4f, 0, 50) - transform.position;

            float toTargetAngle = Mathf.Clamp(CaleAngle(transform.up) - CaleAngle(dir), -1, 1);//시커 추적률

            transform.rotation *= Quaternion.AngleAxis(-toTargetAngle * mobility * Time.deltaTime, Vector3.forward);//미사일 기동성


            if (CaleAngle(transform.up) - CaleAngle(dir) > 120 || CaleAngle(transform.up) - CaleAngle(dir) < -120) // 타겟과 120도 이상 각도가 벌어질 시 타겟 소실
            {
                target = null;
            }
        }
    }

    float CaleAngle(Vector3 vec)
    {
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90;
        return angle;
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag != "Ammo")
        {
            target.GetComponent<Status>().Damage(dmg);
            HitMissile();
        }
    }
}
