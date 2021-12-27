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

    float PowerConstant = 1.5f;
    float mobilityConstant = 1;
    float liftPower = 2;

    // Start is called before the first frame update
    void Start()
    {
        Fire();
        explosionPrefab = transform.GetChild(1).GetComponent<Explosion>();//미사일 하위에 있는 폭발 오브젝트의 클래스 컴포넌트
        rocketTeil = transform.GetChild(2).GetComponent<RocketTail>();
    }

    string targetTag = null;
    public void Init(float d, float v, float m, string tag)
    {
        dmg = d;
        velocity = v;
        mobility = m;
        targetTag = tag;
    }
    
    void Fire()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * velocity/2 * PowerConstant, ForceMode.VelocityChange);
        Invoke("SelfDestroyMissile", 6f);
    }

    void SelfDestroyMissile()
    {
        rocketTeil.transform.SetParent(null);
        rocketTeil.StopBurn();

        GameObject.Destroy(gameObject);
    }
    void HitMissile()
    {
        explosionPrefab.transform.SetParent(null);
        explosionPrefab.gameObject.SetActive(true);

        SelfDestroyMissile();
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        rb.AddForce(transform.up * velocity * 100 * PowerConstant * Time.deltaTime, ForceMode.Acceleration);//추력

        float aoa = CaleAngle(this.transform.up) - CaleAngle(rb.velocity);
        rb.AddForce(transform.right * rb.velocity.magnitude * -aoa * PowerConstant * liftPower * Time.deltaTime, ForceMode.Acceleration);//양력

        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            if ((CaleAngle(transform.up) - CaleAngle(dir)) > 90 || (CaleAngle(transform.up) - CaleAngle(dir)) < -90) // 타겟과 90도 이상 각도가 벌어질 시 타겟 소실
            {
                target = null;
                return;
            }

            float distance = Mathf.Clamp(dir.magnitude * 40 / velocity, 0, 200);
            dir += target.transform.up * distance;

            float toTargetAngle = Mathf.Clamp((CaleAngle(transform.up) - CaleAngle(dir)) * 10, -1, 1);//시커 추적률

            transform.rotation *= Quaternion.AngleAxis(-toTargetAngle * mobility * mobilityConstant * Time.deltaTime, Vector3.forward);//미사일 기동성
        }
    }

    float CaleAngle(Vector3 vec)
    {
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90;
        return angle;
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == targetTag)
        {
            target.GetComponent<Status>().Damage(dmg);
            HitMissile();
        }
    }
}
