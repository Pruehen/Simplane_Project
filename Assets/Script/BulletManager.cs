using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    [SerializeField] 
    private GameObject bulletPrefab; 
    
    Queue<Bullet> bulletQueue = new Queue<Bullet>();


    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        Initialize(100);
    }

    private void Initialize(int initcount)
    {
        for(int i = 0; i < initcount; i++)
        {
            bulletQueue.Enqueue(CreateNewBullet());
        }
    }

    private Bullet CreateNewBullet()
    {
        var newBullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        newBullet.gameObject.SetActive(false);
        newBullet.transform.SetParent(transform);

        return newBullet;
    }

    public static Bullet GetBullet()
    {
        if(instance.bulletQueue.Count > 0)
        {
            var bullet = instance.bulletQueue.Dequeue();
            bullet.transform.SetParent(null);
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            var newBullet = instance.CreateNewBullet();
            newBullet.transform.SetParent(null);
            newBullet.gameObject.SetActive(true);
            return newBullet;
        }
    }

    public static void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(instance.transform);
        instance.bulletQueue.Enqueue(bullet);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
