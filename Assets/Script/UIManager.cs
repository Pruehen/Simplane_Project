using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    RectTransform compass;
    GameObject player;

    [SerializeField]
    Text scoreText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        compass = GameObject.Find("Compass").GetComponent<RectTransform>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CompassSystem();

        ScoreTextSystem();
    }

    void CompassSystem()
    {
        compass.rotation = Quaternion.Euler(0, 0, -player.transform.rotation.z);
    }

    int score;
    void ScoreTextSystem()
    {
        scoreText.text = "Score : " + score * 10;
    }

    public void UpScore(int s)
    {
        score += s * 10;
    }
}
