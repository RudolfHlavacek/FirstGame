using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;
    Animator objectwithAnim;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        objectwithAnim = GameObject.FindGameObjectWithTag("Animobject").GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(new Vector3(50, 0, 0));
            objectwithAnim.SetBool("Walk", true); // Spuštění animace
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(new Vector3(-50, 0, 0));
            objectwithAnim.SetBool("Walk", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(new Vector3(0, 0, 50));
            objectwithAnim.SetBool("Walk", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(new Vector3(0, 0, -50));
            objectwithAnim.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            objectwithAnim.SetBool("Walk", false);
        }
    }
}
