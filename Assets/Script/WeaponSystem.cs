using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    GameObject hardPoint1;
    [SerializeField]
    GameObject hardPoint2;

    int hardPointNum = 1;

    [SerializeField]
    AAMissile MissilePrefab;
    AAMissile Missile;
    AudioSource SeekerSound;
    GameObject Seeker;

    List<GameObject> EnemyList;
    GameObject target;
    string TagName = "Enemy";

    WeaponStatus status;


    bool isFireReady;
    bool isFire;
    bool activeSeeker = false;

    public bool IsFireReady { set { isFireReady = value; } }
    public bool IsFire { set { isFire = value; } }

    void Start()
    {
        SeekerSound = gameObject.GetComponent<AudioSource>();
        Seeker = GameObject.Find("Seeker");
        Seeker.SetActive(false);

        status = gameObject.GetComponent<WeaponStatus>();
    }

    GameObject SetTarget()
    {
        EnemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));//적 배열
        Vector3 vec = EnemyList[0].transform.position - transform.position;//적 - 플레이어 벡터
        float targetAngle = Mathf.Abs(CaleAngle(gameObject.transform.up) - CaleAngle(vec));//적 - 플레이어 각도
        float heatFactor = 10000 / (targetAngle * Mathf.Pow(vec.magnitude, 2));//히트팩터. 각도차 작을수록, 거리 가까울수록 커짐.

        if (vec.magnitude < 200 && targetAngle < 90)
        {
            target = EnemyList[0];//배열 1번째 객체로 타겟설정
        }


        foreach (GameObject found in EnemyList)//적배열 전체 탐색
        {
            Vector3 vec2 = found.transform.position - transform.position;
            float targetAngle2 = Mathf.Abs(CaleAngle(gameObject.transform.up) - CaleAngle(vec2));
            float heatFactor2 = 10000 / (targetAngle2 * Mathf.Pow (vec2.magnitude, 2));

            if (target == null && vec2.magnitude < 200 && targetAngle2 < 60)//타겟이 비었고, 거리 각도가 일정 이내일 경우 타겟 설정
            {
                target = found;
            }

            if (heatFactor2 > heatFactor)//새로 탐색한 히트팩터가 더 클 경우, 타겟의 거리, 각도가 일정 이내일 경우 타겟변경
            {
                if (vec2.magnitude < 160 && targetAngle2 < 60)
                {
                    target = found;
                    heatFactor = heatFactor2;
                }
            }
        }

        activeSeeker = true;
        return target;
    }

    float CaleAngle(Vector3 vec)
    {
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        return angle;
    }


    void SeekerControl()
    {
        if (target != null)
        {
            Seeker.SetActive(true);
            Seeker.transform.position = target.transform.position;
        }
        else if (target == null)
        {
            Seeker.SetActive(false);
            SeekerSound.Stop();
            activeSeeker = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFireReady == true)//발사 버튼이 눌릴 경우
        {
            target = SetTarget();
            SeekerSound.Play();
            isFireReady = false;
        }

        if (activeSeeker == true)
        {
            SeekerControl();
        }

        if (isFire == true)//발사 버튼을 뗄 경우
        {
            if (target == null)//타겟이 빈 경우에는 트리거 잠금
            {
                isFire = false;
                return;
            }

            Missile = Instantiate(MissilePrefab);
            if (hardPointNum == 1)//발사 때마다 발사 하드포인트 변경
            {
                Missile.gameObject.transform.position = hardPoint1.transform.position;
                hardPointNum++;
            }
            else if (hardPointNum == 2)
            {
                Missile.gameObject.transform.position = hardPoint2.transform.position;
                hardPointNum--;
            }
            Missile.gameObject.transform.rotation = transform.rotation;
            Missile.SetTarget = target;//미사일 타겟 입력
            Missile.Init(status.Mis_Dmg, status.Mis_Velocity, status.Mis_Mobility);//미사일 스펙 입력
            target = null;
            isFire = false;
        }
    }
}
