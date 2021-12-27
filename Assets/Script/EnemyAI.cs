using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    VirtualJoystick v_joystick;
    Mwr mwr;
    GameObject player;
    GameObject target = null;

    [SerializeField]
    AAMissile missilePrf;


    Vector3 controllVector = new Vector3(0, 0, 0);
    Vector3 initVector = new Vector3(0.5f, 0.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        v_joystick = this.GetComponent<VirtualJoystick>();
        mwr = this.GetComponent<Mwr>();
        player = GameObject.Find("Player");

        InvokeRepeating("EnemyRader", 0, 0.5f);
        Init();
    }

    void Init()
    {
        initVector = new Vector3(Random.Range(-1f, 1f), 0.5f, 0);
        controllVector = initVector;
    }

    float missileFireStack = 0;
    float missileFireStackLimit = 7;
    // Update is called once per frame
    void Update()
    {
        TrakingTarget();
        MissileEvade();

        if(isTrackingTarget)
        {
            missileFireStack += Time.deltaTime;

            if(missileFireStack > missileFireStackLimit && TargetAngle(target) < 15 && TargetAngle(target) > -15 && TargetDistance(target) < 200 && TargetDistance(target) > 60)
            {
                AAMissile missile = Instantiate(missilePrf, this.transform.position, this.transform.rotation);
                missile.SetTarget = target;
                missile.Init(50, 80, 30, "Player");
                missileFireStack = 0;
            }
        }

        v_joystick.SetJoystick(controllVector);
    }

    bool isTrackingTarget = false;
    void TrakingTarget()//적기 추적
    {
        if (target != null)
        {
            controllVector.x = TargetAngle(target) / 45;
            controllVector.y = 0.5f;
            isTrackingTarget = true;
        }
        else if (target == null && isTrackingTarget)
        {
            Init();
            isTrackingTarget = false;
        }
    }

    bool isMissileEvade = false;
    void MissileEvade()//미사일 회피
    {
        if (mwr.ClosedAngle != 0)
        {
            float mwrMissileAngle = mwr.ClosedAngle;
            controllVector.y = 1;
            if (mwrMissileAngle > 0 && mwrMissileAngle < 180)
            {
                controllVector.x = 1;
            }
            else if (mwrMissileAngle >= 180 && mwrMissileAngle < 360)
            {
                controllVector.x = -1;
            }
            isMissileEvade = true;
        }
        else if (mwr.ClosedAngle == 0 && isMissileEvade)
        {
            Init();
            isMissileEvade = false;
        }
    }

    void EnemyRader()//적 탐색용 레이더. 0.5초에 한번씩 불러옴
    {
        Vector3 vec = player.transform.position - this.transform.position;
        float targetAngle = Mathf.Clamp(CaleAngle(this.transform.up) - CaleAngle(vec), -90, 90);

        if(targetAngle < 90)
        {
            target = player;
        }
        else
        {
            target = null;
        }
    }


    float CaleAngle(Vector3 vec)
    {
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        return angle;
    }

    float TargetDistance(GameObject target)
    {
        Vector3 vec = player.transform.position - this.transform.position;
        float distance = vec.magnitude;
        return distance;
    }
    float TargetAngle(GameObject target)
    {
        Vector3 vec = target.transform.position - this.transform.position;
        float targetAngle = Mathf.Clamp(CaleAngle(this.transform.up) - CaleAngle(vec), -90, 90);
        return targetAngle;
    }
}
