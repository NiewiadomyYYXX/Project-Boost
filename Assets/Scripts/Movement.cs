using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotationTune = 100;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        a = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!a.isPlaying)
            {
                a.PlayOneShot(mainEngine);
            }
        }
        else
        {
            a.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationTune);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationTune);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
