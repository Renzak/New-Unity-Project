using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerWithCamera : MonoBehaviour
{
    Rigidbody rigidBody;
    public GameObject mainCamera;
    public AudioClip jumpSound;

    public Vector3 VERTICAL_VELOCITY = new Vector3(0, 30, 0);
    readonly string JUMPABLE_TAG = "JumpPlatform";

    public int SPEED_FACTOR = 50;
    public int MAX_VELOCITY = 20;

    bool canJump = false;
    int collidingJumpPlatformsCount = 0;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProccessUserInput();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(JUMPABLE_TAG))
        {
            canJump = true;
            collidingJumpPlatformsCount++;

            var emptyObject = new GameObject();
            emptyObject.transform.parent = collision.gameObject.transform;
            gameObject.transform.parent = emptyObject.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(JUMPABLE_TAG))
        {
            collidingJumpPlatformsCount--;
            gameObject.transform.parent = null;

            if (collidingJumpPlatformsCount == 0)
            {
                canJump = false;
            }
        }
    }

    void ProccessUserInput()
    {
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            rigidBody.velocity += VERTICAL_VELOCITY;
            canJump = false;
            gameObject.GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.velocity += Quaternion.Euler(0, -90, 0) * new Vector3(transform.position.x - mainCamera.transform.position.x, 0, transform.position.z - mainCamera.transform.position.z).normalized * Time.deltaTime * SPEED_FACTOR;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.velocity += new Vector3(transform.position.x - mainCamera.transform.position.x, 0, transform.position.z - mainCamera.transform.position.z).normalized * Time.deltaTime * SPEED_FACTOR;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.velocity += Quaternion.Euler(0, -90, 0) * -new Vector3(transform.position.x - mainCamera.transform.position.x, 0, transform.position.z - mainCamera.transform.position.z).normalized * Time.deltaTime * SPEED_FACTOR;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.velocity -= new Vector3(transform.position.x - mainCamera.transform.position.x, 0, transform.position.z - mainCamera.transform.position.z).normalized * Time.deltaTime * SPEED_FACTOR;
        }

        if (rigidBody.velocity.x > MAX_VELOCITY)
        {
            rigidBody.velocity = new Vector3(MAX_VELOCITY, rigidBody.velocity.y, rigidBody.velocity.z);
        }

        if (rigidBody.velocity.z < -MAX_VELOCITY)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -MAX_VELOCITY);
        }

        if (rigidBody.velocity.z > MAX_VELOCITY)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, MAX_VELOCITY);
        }

        if (rigidBody.velocity.x < -MAX_VELOCITY)
        {
            rigidBody.velocity = new Vector3(-MAX_VELOCITY, rigidBody.velocity.y, rigidBody.velocity.z);
        }
    }

}
