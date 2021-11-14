using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maingun : MonoBehaviour
{
    [SerializeField]
    GameObject parentObject;

    AudioSource audioSource;
    public AudioSource stopAudioSource;

    bool isFireing = false;
    public void SetisFireing(bool inputdata)
    {
        isFireing = inputdata;

        if (isFireing == true) { StartFireing(); }
        else { StopFireing(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFireing == true) { Fireing(); }
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
    float fireRPM = 1200;
    void Fireing()
    {
        fireDelay += Time.deltaTime;
        if (fireDelay >= (60/fireRPM))
        {
            var newBullet = BulletManager.GetBullet();
            Transform FirePosition = transform;
            newBullet.transform.position = FirePosition.position;
            newBullet.transform.rotation = FirePosition.rotation;

            newBullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.up * 300 + parentObject.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
            newBullet.Shoot();
            
            fireDelay = 0;
        }
    }
}
