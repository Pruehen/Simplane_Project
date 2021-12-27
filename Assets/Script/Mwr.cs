using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mwr : MonoBehaviour
{
    List<GameObject> MissileList;
    string TagName = "Missile";
    float closedAngle;
    float serchLimit = 200;
    public float ClosedAngle { get { return closedAngle; } }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MwrLoop", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MwrLoop()
    {
        MissileList = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));
        if (MissileList.Count != 0)
        {
            float targetAngle = 0;
            Vector3 vec = MissileList[0].transform.position - this.transform.position;

            if(vec.magnitude < serchLimit)
            {
                targetAngle = CaleAngle(this.transform.up) - CaleAngle(vec);
            }
            
            foreach (GameObject found in MissileList)
            {
                Vector3 vec2 = found.transform.position - this.transform.position;
                if (vec2.magnitude < vec.magnitude)
                {
                    targetAngle = CaleAngle(this.transform.up) - CaleAngle(vec2);
                }
            }
            closedAngle = targetAngle;
        }
        else
        {
            closedAngle = 0;
        }
    }

    float CaleAngle(Vector3 vec)
    {
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg + 180;
        return angle;
    }
}
