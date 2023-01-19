using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 0f;
    public float gravity = -9.81f;
    public float sensitivityX = 1.0f;

    

    //Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        //moving
        
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Rigidbody ourBody = this.GetComponent<Rigidbody>();
        if (x != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f,x/2, 0f);

            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }

        Vector3 move = transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            speed += Time.deltaTime * 10;
        }
        else
        {
            speed -= Time.deltaTime * 100;
        }


        if(speed > 40)
        {
            speed = 40;
        }
        if(speed < 0)
        {
            speed = 0;
        }
        
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }
    }
}
