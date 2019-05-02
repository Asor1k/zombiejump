using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Phisics : MonoBehaviour {

   [SerializeField] private LineRenderer lineRenderer;
    PlatformGenerator generator;
    public float dotSeparation = 3;
    public Animator animator;
    public float dotShift = 3;
    bool isGrowing = true;
    Rigidbody rb;
    public float force = 0;
    bool isPressed = false;
    public int side = 1;
    public float gradus = 0;
    public Image arrow;
   // public Slider slider;
    public float speed;
    public Image image;
    Color startImg;
    public bool isLanded = true;
    bool isRotatingLeft = true;
    Vector3 preTr;
    public platformtrigger plTriger;

    void Start ()
    {
    //    startImg = image.color;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        generator = FindObjectOfType<PlatformGenerator>();
    }


    public void Stop()
    {
        StartCoroutine("LandedCour");
    }

    float AngleToRotate(float angle)
    {
        if (angle == transform.rotation.y)
            return 0;
        return angle - transform.rotation.y;
    }


    IEnumerator LandedCour()
    {
        
        yield return new WaitForSeconds(0.1f);
        Quaternion target;
        if (side == 1)
            target = Quaternion.Euler(0, 180, 0);
        else
            target = Quaternion.Euler(0, 0, 0);

        transform.rotation = target;
      //  rb.isKinematic = true;
        generator.MoveCamera();
        isLanded = true;
    }

    


    public void ButtonDown()
    {
        isPressed = true;
       // rb.isKinematic = false;
        
    }

    public void ButtonUp()
    {
        lineRenderer.enabled = false;
        isPressed = false;
        if (isLanded)
        {
            animator.SetBool("IsFalling", true);

            rb.AddForce(Vector3.left * force * side);
            plTriger.isActive = false;

            //force = 5;
            Invoke("Speed", 0.05f);
        }
            //  image.color = startImg;
    }

    void Speed()
    {
        animator.SetBool("IsFalling", false);
    }


    float x1;
    float y1;


    void Update ()
    {
        if (isLanded)
        {
            lineRenderer.enabled = true;
            for (int i = 1; i < lineRenderer.positionCount; i++)
            {
                x1 = force * Time.fixedDeltaTime * (dotSeparation * i + dotShift);
                y1 = Time.fixedDeltaTime * (dotSeparation * i + dotShift) - (-Physics2D.gravity.y / 2f * Time.fixedDeltaTime * Time.fixedDeltaTime * (dotSeparation * i + dotShift) * (dotSeparation * i + dotShift));
                Vector3 pos = new Vector3(x1, y1);
                
                lineRenderer.SetPosition(i,pos);
            }
            if (isGrowing)
            {
                force += 0.05f;
               
            }
            else
            {
                force -= 0.05f;
                for (int i = 1; i < lineRenderer.positionCount; i++)
                {
                   // lineRenderer.SetPosition(i, new Vector3( -force * Time.deltaTime * Time.deltaTime, - 9.81f * Time.deltaTime * Time.deltaTime));
                }
            }
            if (force >= 10) isGrowing = false;
            if (force <= 3.5f) isGrowing = true;
                
        }
        //slider.value = force;
       
   
    }



}



/*if (isRotatingLeft)
                arrow.transform.Rotate(new Vector3(0, 0, 10 * speed*Time.deltaTime));
            else
            {
                arrow.transform.Rotate(new Vector3(0, 0, -10 * speed*Time.deltaTime));
            }
                if (arrow.transform.rotation.eulerAngles.z >= 150) isRotatingLeft = false;
                if (arrow.transform.rotation.eulerAngles.z <= 30) isRotatingLeft = true;
           */
        