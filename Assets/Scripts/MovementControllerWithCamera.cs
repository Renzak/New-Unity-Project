using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerWithCamera : MonoBehaviour
{
    public AudioClip jumpSound;

    public float verticalVelocity =  30f;
    public float timeDelayBetweenJumps =  0.5f;
    public float maxVelocityReducer = 30f;

    public int speedFactor = 50;
    public int maxVelocity = 20;

    public int speedFactorSprint = 90;
    public int maxVelocitySprint = 40;

    float deltaTimeJump = 0;
    float currentMaxVelocity;

    int collidingJumpPlatformsCount = 0;
    int currentSpeedFactor;
    
    bool isShiftUp = true;
   
    GameObject mainCamera;
    Rigidbody rigidBody;

    void Start()
    {
        mainCamera = GameObject.Find(Config.ObjectNames.mainCamera);
        currentMaxVelocity = maxVelocity;
        currentSpeedFactor = speedFactor;
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProccessUserInput();
        deltaTimeJump += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Config.Tags.Jumpable))
        {
            collidingJumpPlatformsCount++;

            if(gameObject.transform.parent != null)
            {
                GameObject temp = gameObject.transform.parent.gameObject;
                gameObject.transform.parent = null;
                Destroy(temp);
            }

            var emptyObject = new GameObject();
            emptyObject.name = Config.ObjectNames.playerParent;
            emptyObject.transform.rotation = collision.transform.rotation;
            emptyObject.transform.parent = collision.gameObject.transform;
            gameObject.transform.parent = emptyObject.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(Config.Tags.Jumpable))
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
        Vector3 vector = new Vector3(posX, 0, posZ).normalized * Time.deltaTime * currentSpeedFactor;

        if (Input.GetKey(Config.Input.jump) && collidingJumpPlatformsCount > 0 && deltaTimeJump > timeDelayBetweenJumps)
        {
            deltaTimeJump = 0f;
            rigidBody.velocity += new Vector3(0, verticalVelocity, 0);
            gameObject.GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }

        if (Input.GetKey(Config.Input.left))
        {
            rigidBody.velocity += Quaternion.Euler(0, -90, 0) * vector;
        }

        if (Input.GetKey(Config.Input.forward))
        {
            rigidBody.velocity += vector;
        }

        if (Input.GetKey(Config.Input.right))
        {
            rigidBody.velocity -= Quaternion.Euler(0, -90, 0) * vector;
        }

        if (Input.GetKey(Config.Input.backward))
        {
            rigidBody.velocity -= vector;
        }

        if (Input.GetKeyDown(Config.Input.sprint))
        {
            currentMaxVelocity = maxVelocitySprint;
            currentSpeedFactor = speedFactorSprint;
            isShiftUp = false;
        }
        else if (Input.GetKeyUp(Config.Input.sprint))
        {
            currentSpeedFactor = speedFactor;
            isShiftUp = true;           
        }

        if (isShiftUp)
        {
            if (currentMaxVelocity > maxVelocity)
            {
                currentMaxVelocity -= maxVelocityReducer * Time.deltaTime;
            }
            else
            {
                currentMaxVelocity = maxVelocity;
            }
        }

        Debug.Log(currentMaxVelocity);
        LimitVelocity();
    }

    void LimitVelocity()
    {
        if (rigidBody.velocity.x > currentMaxVelocity)
        {
            rigidBody.velocity = new Vector3(currentMaxVelocity, rigidBody.velocity.y, rigidBody.velocity.z);
        }

        if (rigidBody.velocity.z < -currentMaxVelocity)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -currentMaxVelocity);
        }

        if (rigidBody.velocity.z > currentMaxVelocity)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, currentMaxVelocity);
        }

        if (rigidBody.velocity.x < -currentMaxVelocity)
        {
            rigidBody.velocity = new Vector3(-currentMaxVelocity, rigidBody.velocity.y, rigidBody.velocity.z);
        }
    }

    private void OnEnable()
    {
        if (Input.GetKey(Config.Input.sprint))
        {
            currentMaxVelocity = maxVelocitySprint;
            currentSpeedFactor = speedFactorSprint;
        }

        else 
        {
            currentMaxVelocity = maxVelocity;
            currentSpeedFactor = speedFactor;
        }
    }
}
