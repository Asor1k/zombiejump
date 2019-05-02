using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformGenerator : MonoBehaviour {
    public Transform prePlatformTr;
    public Transform nextPlatformTr;
    public GameObject platform;
    private Camera mainCamera;
    public Vector3 intend;
    Phisics phisics;
    public int score = 0;

    public Text scoreText;
    public Text maxScoreText;


    bool isFirst = true;

    private void Awake()
    {
        phisics = FindObjectOfType<Phisics>();
    }

    void Start ()
    {
        mainCamera = FindObjectOfType<Camera>();
        intend = mainCamera.transform.position;
        GameObject instance;
        Debug.Log(Screen.height);
        Debug.Log(Screen.width);
        instance = Instantiate(platform, new Vector3(prePlatformTr.position.x + Random.Range(4f, 7f), prePlatformTr.position.y - Random.Range(3, 5.1f), 0f), Quaternion.identity);
        phisics.side = -1;
        
        nextPlatformTr = instance.transform;
    }


    public void MoveCamera()
    {
        isMovingCamera = true;
    }
    
    internal void DrowText()
    {
        if (PlayerPrefs.GetInt("maxScore") < score)
        {
            PlayerPrefs.SetInt("maxScore", score);
        }

        scoreText.text = score.ToString();
         //maxScoreText.text = "Max score: " + PlayerPrefs.GetInt("maxScore").ToString();
    }

    internal void CreatePlatform()
    {
        prePlatformTr = nextPlatformTr;
        GameObject instance;
        if (Random.Range(0,2) == 1)
        {
            instance = Instantiate(platform, new Vector3(prePlatformTr.position.x + Random.Range(4f, 7f), prePlatformTr.position.y - Random.Range(3, 5.1f), 0f), Quaternion.identity);
            phisics.side = -1;
        }
        else
        {
            instance = Instantiate(platform, new Vector3(prePlatformTr.position.x - Random.Range(4f, 7f), prePlatformTr.position.y - Random.Range(3, 5.1f), 0f), Quaternion.identity);
            phisics.side = 1;
        }
       
        nextPlatformTr = instance.transform;
    
       // MoveCamera();
    }
    bool isMovingCamera = false;
    public float time = 1;
    void Update ()
    {
        Vector3 targetVector = new Vector3(prePlatformTr.position.x - (intend.x) * phisics.side, prePlatformTr.position.y + intend.y, -10);
        if (isMovingCamera)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetVector, Time.deltaTime * time);
        }
        if (Vector3.Distance(mainCamera.transform.position, targetVector) < 0.1f) isMovingCamera = false;


    }
}


  