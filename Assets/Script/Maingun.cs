using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maingun : MonoBehaviour
{
    [SerializeField]
    GameObject parentObject;

    WeaponStatus status;

    AudioSource audioSource;
    public AudioSource stopAudioSource;

    bool isFireing = false;

    float dmg;
    float velocity;
    float rpm;

    void Init(float d, float v, float r)
    {
        dmg = d;
        velocity = v;
        rpm = r;
    }

    public void SetisFireing(bool inputdata)
    {
        isFireing = inputdata;

        if (isFireing == true) { StartFireing(); }
        else { StopFireing(); }
    }

    private void Awake()
    {
        status = WeaponStatus.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        Init(status.vulkan_Status.Gun_Dmg, status.vulkan_Status.Gun_Velocity, status.vulkan_Status.Gun_Rpm);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFireing == true) { Fireing(); }
        else if (spread > 0)
        {
            spread -= Time.deltaTime;
        }    
    }

    void StartFireing()
    {
        audioSource.Play();
    }

    void StopFireing()
    {
        audioSource.Stop();
        stopAudioSource.Play();
    }


    float fireDelay = 0;
    float spread = 0;

    void Fireing()
    {
        fireDelay += Time.deltaTime;


        if (fireDelay >= (60 / rpm))
        {
            var newBullet = BulletManager.GetBullet();
            Transform FirePosition = transform;
            newBullet.transform.position = FirePosition.position;//탄 위치, 회전 조정
            newBullet.transform.rotation = FirePosition.rotation * Quaternion.AngleAxis(Random.Range(-4 * spread, 4 * spread), new Vector3(0, 0, 1));

            newBullet.GetComponent<Rigidbody>().velocity = Vector3.zero;//탄 속도 0으로 조정
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.up * velocity * 0.2f + parentObject.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);//탄 발사
            newBullet.Init(dmg);

            if (spread < 2)
            {
                spread += Time.deltaTime;
            }

            fireDelay = 0;
        }
    }
}
