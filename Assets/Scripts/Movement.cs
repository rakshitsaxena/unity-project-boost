using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcecssRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
             if(!audioSource.isPlaying)
             {
               audioSource.PlayOneShot(mainEngine);
             }
        } else 
        {
            audioSource.Stop();
        }       
    }

    void ProcecssRotation()
    {
        /*
        You want the player not to rotate both right and left. Btw we 
        have given precedence to Left, if both keys are pressed.
        */
        if(Input.GetKey(KeyCode.A))
        {       
            ApplyRotation();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-1);
        }
    }

    void ApplyRotation(int isLeft = 1)
    {
        rb.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime * isLeft);
        rb.freezeRotation = false; //physics system taking over
    }
}
