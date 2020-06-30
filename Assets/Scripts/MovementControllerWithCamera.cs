using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerWithCamera : MonoBehaviour
{
    public GameObject mainCamera;
    public AudioClip jumpSound;

    public Vector3 verticalVelocity = new Vector3(0, 30, 0);

    public int speedFactor = 50;
    public int maxVelocity = 20;
    public int speedFactorSprint = 40;
    public int maxVelocitySprint = 20;

    public float delaSinceJump =  0.5f;

    Rigidbody rigidBody;
    
    int collidingJumpPlatformsCount = 0;
    
    float deltaTimeJump = 0;
    
    readonly string JUMPABLE_TAG = "JumpPlatform";
    readonly string PLAYER_PARENT_OBJECT_NAME = "PlayerParentObject";


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

            if(gameObject.transform.parent != null)
            {
                GameObject temp = gameObject.transform.parent.gameObject;
                gameObject.transform.parent = null;
                Destroy(temp);
            }

            var emptyObject = new GameObject();
            emptyObject.name = PLAYER_PARENT_OBJECT_NAME;
            emptyObject.transform.parent = collision.gameObject.transform;
            gameObject.transform.parent = emptyObject.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(JUMPABLE_TAG))
        {
            collidingJumpPlatformsCount--;

            if (gameObject.transform.parent != null)
            {
                GameObject temp = gameObject.transform.parent.gameObject;
                gameObject.transform.parent = null;
                Destroy(temp);
            }
        }
    }

    void ProccessUserInput()
    {

        float posX = transform.position.x - mainCamera.transform.position.x;
        float posZ = transform.position.z - mainCamera.transform.position.z;

        if (Input.GetKey(KeyCode.Space) && collidingJumpPlatformsCount > 0 && deltaTimeJump > delaSinceJump)
        {
            deltaTimeJump = 0f;
            rigidBody.velocity += verticalVelocity;
            gameObject.GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.velocity += Quaternion.Euler(0, -90, 0) * new Vector3(posX, 0, posZ).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.velocity += new Vector3(posX, 0, posZ).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.velocity += Quaternion.Euler(0, -90, 0) * -new Vector3(posX, 0, posZ).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.velocity -= new Vector3(posX, 0, posZ).normalized * Time.deltaTime * speedFactor;
        }

        if (rigidBody.velocity.x > maxVelocity)
        {
            rigidBody.velocity = new Vector3(maxVelocity, rigidBody.velocity.y, rigidBody.velocity.z);
        }

        if (rigidBody.velocity.z < -maxVelocity)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -maxVelocity);
        }

        if (rigidBody.velocity.z > maxVelocity)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, maxVelocity);
        }

        if (rigidBody.velocity.x < -maxVelocity)
        {
            rigidBody.velocity = new Vector3(-maxVelocity, rigidBody.velocity.y, rigidBody.velocity.z);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxVelocity += maxVelocitySprint;
            speedFactor += speedFactorSprint;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxVelocity -= maxVelocitySprint;
            speedFactor -= speedFactorSprint;
        }
    }
}
