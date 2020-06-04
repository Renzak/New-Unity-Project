using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerWithCamera : MonoBehaviour
{
    public GameObject mainCamera;
    public AudioClip jumpSound;

    public Vector3 VERTICAL_VELOCITY = new Vector3(0, 30, 0);

    public int SPEED_FACTOR = 50;
    public int MAX_VELOCITY = 20;
    public float DELTA_SINCE_JUMP =  0.5f;

    int collidingJumpPlatformsCount = 0;
    Rigidbody rigidBody;

    readonly string JUMPABLE_TAG = "JumpPlatform";
    float deltaTimeJump = 0;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProccessUserInput();
        deltaTimeJump += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(JUMPABLE_TAG))
        {
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

            if(gameObject.transform.parent != null)
            {
                GameObject temp = gameObject.transform.parent.gameObject;
                gameObject.transform.parent = null;
                Destroy(temp);
            }
        }
    }

    void ProccessUserInput()
    {
        if (Input.GetKey(KeyCode.Space) && collidingJumpPlatformsCount > 0 && deltaTimeJump > DELTA_SINCE_JUMP)
        {
            deltaTimeJump = 0f;
            rigidBody.velocity += VERTICAL_VELOCITY;
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
