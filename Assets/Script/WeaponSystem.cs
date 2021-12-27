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
    GameObject RaderLock;

    List<GameObject> EnemyList;
    GameObject target;
    string TagName = "Enemy";

    WeaponStatus status;

    bool isFireReady = false;
    bool isFire = false;
    bool activeSeeker = false;

    public bool IsFireReady { set { isFireReady = value; } }
    public bool IsFire { set { isFire = value; } }

    void Start()
    {
        SeekerSound = gameObject.GetComponent<AudioSource>();
        Seeker = GameObject.Find("Seeker");
        RaderLock = GameObject.Find("RaderLock");
        RaderLock.transform.SetParent(null);
        Seeker.SetActive(false);
        RaderLock.SetActive(false);

        status = WeaponStatus.instance;

        InvokeRepeating("Rader", 0, 0.5f);
    }

    GameObject SetTarget(float maxRange, float maxAngle)//미사일의 공통 탐색 알고리즘
    {
        GameObject target = null;

        EnemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));//적 배열
        Vector3 vec = EnemyList[0].transform.position - transform.position;//적 - 플레이어 벡터
        float targetAngle = Mathf.Abs(CaleAngle(gameObject.transform.up) - CaleAngle(vec));//적 - 플레이어 각도
        float heatFactor = 1 / (targetAngle * Mathf.Pow(vec.magnitude, 3));//히트팩터. 각도차 작을수록, 거리 가까울수록 커짐.

        if (vec.magnitude < maxRange && targetAngle < maxAngle)
        {
            target = EnemyList[0];//배열 1번째 객체로 타겟설정
        }


        foreach (GameObject found in EnemyList)//적배열 전체 탐색
        {
            Vector3 vec2 = found.transform.position - transform.position;
            float targetAngle2 = Mathf.Abs(CaleAngle(gameObject.transform.up) - CaleAngle(vec2));
            float heatFactor2 = 1 / (targetAngle2 * Mathf.Pow (vec2.magnitude, 3));

            if (target == null && vec2.magnitude < maxRange && targetAngle2 < maxAngle)//타겟이 비었고, 거리 각도가 일정 이내일 경우 타겟 설정
            {
                target = found;
            }

            if (heatFactor2 > heatFactor)//새로 탐색한 히트팩터가 더 클 경우, 타겟의 거리, 각도가 일정 이내일 경우 타겟변경
            {
                if (vec2.magnitude < maxRange && targetAngle2 < maxAngle)
                {
                    target = found;
                    heatFactor = heatFactor2;
                }
            }
        }

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
        if (target != null)
        {
            RaderLock.transform.position = target.transform.position;
        }

        coolcount1 += Time.deltaTime;
        coolcount2 += Time.deltaTime;

        if (weaponSelect == 1)
        {
            HeatTrakingMissile();
        }
        else if (weaponSelect == 2)
        {
            SemiactiveHomingMissile();
        }
    }

    void Rader()
    {
        GameObject radertarget = SetTarget(300, 80);
        if (radertarget != null)
        {
            RaderLock.SetActive(true);
            RaderLock.transform.position = radertarget.transform.position;
        }
        Invoke("RaderForget", 0.48f);
    }
    void RaderForget()
    {
        RaderLock.transform.rotation = this.transform.rotation;
        RaderLock.SetActive(false);
    }


    float missile1Cooldown = 2;
    float coolcount1;
    float missile2Cooldown = 4;
    float coolcount2;

    void HeatTrakingMissile()
    {
        if (isFireReady == true)//발사 버튼이 눌릴 경우
        {
            if (coolcount1 > missile1Cooldown)//미사일 쿨다운이 끝났을 때만 탐색 실행
            {
                target = SetTarget(status.aam9_Status.Mis_MaxRange, status.aam9_Status.Mis_MaxAngle);
                SeekerSound.Play();
                activeSeeker = true;
            }
            isFireReady = false;
        }

        if (activeSeeker == true)
        {
            SeekerControl();
        }

        if (isFire == true)//발사 버튼을 뗄 경우
        {
            if (target == null)//타겟이 비었을 경우 트리거 잠금
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
            Missile.Init(status.aam9_Status.Mis_Dmg, status.aam9_Status.Mis_Velocity, status.aam9_Status.Mis_Mobility, "Enemy");//미사일 스펙 입력
            target = null;
            isFire = false;
            coolcount1 = 0;
        }
    }

    void SemiactiveHomingMissile()
    {
        if (isFireReady == true)//발사 버튼이 눌릴 경우
        {
            if (coolcount2 > missile2Cooldown)//미사일 쿨다운이 끝났을 때만 탐색 실행
            {
                target = SetTarget(status.aam7_Status.Mis_MaxRange, status.aam7_Status.Mis_MaxAngle);
            }

            if (target == null)//타겟이 빈 경우에는 트리거 잠금
            {
                isFireReady = false;
                return;
            }

            Missile = Instantiate(MissilePrefab);
            if (hardPointNum == 3)//발사 때마다 발사 하드포인트 변경
            {
                Missile.gameObject.transform.position = hardPoint1.transform.position;
                hardPointNum++;
            }
            else if (hardPointNum == 4)
            {
                Missile.gameObject.transform.position = hardPoint2.transform.position;
                hardPointNum--;
            }
            Missile.gameObject.transform.rotation = transform.rotation;
            Missile.SetTarget = target;//미사일 타겟 입력
            Missile.Init(status.aam7_Status.Mis_Dmg, status.aam7_Status.Mis_Velocity, status.aam7_Status.Mis_Mobility, "Enemy");//미사일 스펙 입력

            coolcount2 = 0;
            isFireReady = false;
        }


        if (isFire == true)//발사 버튼을 뗄 경우
        {
            target = null;
            isFire = false;
            if (Missile != null)
            {
                Missile.SetTarget = null;
            }
        }
    }

    int weaponSelect = 1;

    public void SelectWeapon()
    {
        weaponSelect++;
        hardPointNum = 3;
        if(weaponSelect > 2)
        {
            weaponSelect = 1;
            hardPointNum = 1;
        }
        Debug.Log(weaponSelect);
    }
}
