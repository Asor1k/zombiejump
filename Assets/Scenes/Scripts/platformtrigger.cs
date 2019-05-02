using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class platformtrigger : MonoBehaviour {

    PlatformGenerator generator;
    Phisics phisics;
     public Vector3 chekpoint;
    public bool isActive = true;
    public LooseManager looseManager;

    bool wasPressed = false;

	void Start ()
    {
        foreach (Phisics ph in FindObjectsOfType<Phisics>())
        {
            if (ph.tag == "Hero") phisics = ph;
        }
        looseManager = FindObjectOfType<LooseManager>();
        pos = transform.position;
        
        
        generator = FindObjectOfType<PlatformGenerator>();
        
    }


    private void OnCollisionExit(Collision collision)
    {
        isCheckPointing = false;
        phisics.isLanded = false;
    }

    void GoToCheck()
    {
        chekpoint = new Vector3(pos.x + (-phisics.side * (transform.localScale.x / 2 - 0.1f)), phisics.transform.position.y, 0);
        phisics.plTriger = this;
        isCheckPointing = true;
        phisics.animator.SetBool("isCheckPointing", true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Invoke("GoToCheck", 0.05f);
        //Camera.allCameras[0].transform.Translate(new Vector3(transform.position.x, transform.position.y, -1));
        if(collision.collider.transform.position.y < transform.position.y)
        {
            Lost();
            return;
        }
        if (transform.tag == "Respawn")
        {
           // isCheckPointing = true;
            generator.DrowText();
            return;
        }
        if(wasPressed)
        {
            return;
        }
        phisics.Stop();
        generator.CreatePlatform();
        wasPressed = true;
        generator.score++;
        phisics.isLanded = true;
        generator.DrowText();
    }


    void  Lost()
    {
        looseManager.Loose();
    }
    Vector3 pos;
    bool isMovingRight = true;

    public float time = 1;
    public bool isCheckPointing = false;

    void Update ()
    {
        if(isCheckPointing)
        phisics.transform.position = Vector3.Lerp(phisics.transform.position, chekpoint, Time.fixedDeltaTime * time);
        if (Vector3.Distance(phisics.transform.position, chekpoint) <= 0.1f)
        {
            isCheckPointing = false;
            phisics.animator.SetBool("isCheckPointing", false);
        }
        if (isActive)
        {
            if(phisics.transform.position.y<transform.position.y)
            {
               Lost();
            }
        }


       /* curLerpTime += Time.deltaTime;
        float Perc = curLerpTime / time;
        if (isMovingRight)
        {
            transform.position = Vector3.Lerp(pos, pos + Vector3.right, Perc);
        }
        else
        {
            transform.position =  Vector3.Lerp(pos, pos - Vector3.right, Perc);
        }
        if (transform.position == pos + Vector3.right * 1)
        {
            curLerpTime = 0;
            isMovingRight = false;
            pos = transform.position;
        }
        if (transform.position == pos - Vector3.right * 1)
        {
            pos = transform.position;
            curLerpTime = 0;
            isMovingRight = true;
        }
        */
    }
}
