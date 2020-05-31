using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollMovementInput : MonoBehaviour
{
    GameObject boll;
    GameObject mainCamera;
    Rigidbody bollRigidBody;
    Vector3 VERTICAL_VELOCITY = new Vector3(0, 30, 0);

    bool canJump = false;
    const int speed = 50;
    const int cameraDistance = 10;
    const int cameraHight = 7;
    const int maxVelocity = 20;

    void Start()
    {
        Debug.Log("Starting Game...");
        boll = this.gameObject;
        bollRigidBody = boll.GetComponent<Rigidbody>();
        mainCamera = GameObject.Find("MainCamera");
    }

    void ProccessUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            bollRigidBody.velocity += VERTICAL_VELOCITY;
        }

        if (Input.GetKey(KeyCode.A))
        {
            bollRigidBody.velocity += new Vector3(-1 * speed * Time.deltaTime, 0, 0);
            if (bollRigidBody.velocity.x < -maxVelocity)
            {
                bollRigidBody.velocity = new Vector3(-maxVelocity, bollRigidBody.velocity.y, bollRigidBody.velocity.z);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            bollRigidBody.velocity += new Vector3(0, 0, 1 * speed * Time.deltaTime);
            if (bollRigidBody.velocity.z > maxVelocity)
            {
                bollRigidBody.velocity = new Vector3(bollRigidBody.velocity.x, bollRigidBody.velocity.y, maxVelocity);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            bollRigidBody.velocity += new Vector3(1 * speed * Time.deltaTime, 0, 0);
            if (bollRigidBody.velocity.x > maxVelocity)
            {
                bollRigidBody.velocity = new Vector3(maxVelocity, bollRigidBody.velocity.y, bollRigidBody.velocity.z);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            bollRigidBody.velocity += new Vector3(0, 0, -1 * speed * Time.deltaTime);
            if (bollRigidBody.velocity.z < -maxVelocity)
            {
                bollRigidBody.velocity = new Vector3(bollRigidBody.velocity.x, bollRigidBody.velocity.y, -maxVelocity);
            }
        }
    }

    void UpdateCameraPosition()
    {
        mainCamera.transform.position = boll.transform.position + new Vector3(0, cameraHight, -cameraDistance);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
        ProccessUserInput();
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "JumpPlatform")
        {
            canJump = true;
            Debug.Log("Setting canJump true");
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "JumpPlatform")
        {
            canJump = false;
            Debug.Log("Setting canJump false");
        }
    }
}
