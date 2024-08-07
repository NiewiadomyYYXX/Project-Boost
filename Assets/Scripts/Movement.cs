using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotationTune = 100;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem LeftEngine;
    [SerializeField] ParticleSystem RightEngine;
    [SerializeField] ParticleSystem MainEngine;

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
            if (!MainEngine.isPlaying)
            {
                MainEngine.Play();
            }
        }
        else
        {
            a.Stop();
            MainEngine.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationTune);
            if (!RightEngine.isPlaying)
            {
                RightEngine.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationTune);
            if (!LeftEngine.isPlaying)
            {
                LeftEngine.Play();
            }
        }
        else
        {
            RightEngine.Stop();
            LeftEngine.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
