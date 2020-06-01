using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerWithCamera : MonoBehaviour
{
    GameObject mainCamera;
    Rigidbody rigidBody;

    readonly Vector3 VERTICAL_VELOCITY = new Vector3(0, 30, 0);
    readonly string JUMPABLE_TAG = "JumpPlatform";
    readonly string CAMERA_NAME = "MainCamera";

    const int SPEED_FACTOR = 50;
    const int CAMERA_DISTANCE = 10;
    const int CAMERA_HEIGHT = 7;
    const int MAX_VELOCITY = 20;

    bool canJump = false;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        mainCamera = GameObject.Find(CAMERA_NAME);
    }

    void Update()
    {
        UpdateCameraPosition();
        ProccessUserInput();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(JUMPABLE_TAG))
        {
            canJump = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(JUMPABLE_TAG))
        {
            canJump = false;
        }
    }

    void ProccessUserInput()
    {
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            rigidBody.velocity += VERTICAL_VELOCITY;
            canJump = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.velocity += new Vector3(-1 * SPEED_FACTOR * Time.deltaTime, 0, 0);
            if (rigidBody.velocity.x < -MAX_VELOCITY)
            {
                rigidBody.velocity = new Vector3(-MAX_VELOCITY, rigidBody.velocity.y, rigidBody.velocity.z);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.velocity += new Vector3(0, 0, 1 * SPEED_FACTOR * Time.deltaTime);
            if (rigidBody.velocity.z > MAX_VELOCITY)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, MAX_VELOCITY);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.velocity += new Vector3(1 * SPEED_FACTOR * Time.deltaTime, 0, 0);
            if (rigidBody.velocity.x > MAX_VELOCITY)
            {
                rigidBody.velocity = new Vector3(MAX_VELOCITY, rigidBody.velocity.y, rigidBody.velocity.z);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.velocity += new Vector3(0, 0, -1 * SPEED_FACTOR * Time.deltaTime);
            if (rigidBody.velocity.z < -MAX_VELOCITY)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -MAX_VELOCITY);
            }
        }
    }

    void UpdateCameraPosition()
    {
        mainCamera.transform.position = gameObject.transform.position + new Vector3(0, CAMERA_HEIGHT, -CAMERA_DISTANCE);
    }

}
