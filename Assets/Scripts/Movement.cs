using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotThrust = 100f;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem PartBoost;
    [SerializeField] ParticleSystem PartLeft;
    [SerializeField] ParticleSystem PartRight;

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
        ProcessRotation();
    }

    void ProcessThrust()
    {
        //Thrust
        if(Input.GetKey(KeyCode.Space))
        {
            StartThursting();
        }
        else
        {
            StopThursting();
        }
    }

    void StartThursting()
    {
        if (!PartBoost.isPlaying)
        {
            PartBoost.Play();
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine, 1);
        }
    }

    void StopThursting()
    {
        PartBoost.Stop();
        audioSource.Stop();
    }

    void ProcessRotation()
    {
        //Left
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }

        //Right
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        //Stop
        else
        {
            StopRotating();
        }
    }


    void RotateLeft()
    {
        if (!PartLeft.isPlaying)
        {
            PartLeft.Play();
        }
        ApplyRotation(rotThrust);
    }

    void RotateRight()
    {
        if (!PartRight.isPlaying)
        {
            PartRight.Play();
        }
        ApplyRotation(-rotThrust);
    }

    void StopRotating()
    {
        PartRight.Stop();
        PartLeft.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //Unfreeze so physics can take over
    }
}
