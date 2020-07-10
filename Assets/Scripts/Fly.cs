using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    
    public int speedFactor = 50;
    public int maxVelocity = 20;
    public float velocityReduce = 0.99f;

    Rigidbody rigidBody;
    GameObject mainCamera;


    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        mainCamera = GameObject.Find(Config.ObjectNames.mainCamera);
    }

    void Update()
    {
        ProccessUserInput();
        LimitVelocity();
        DecayVelocity();
    }
    
    void ProccessUserInput()
    {
        float posX = transform.position.x - mainCamera.transform.position.x;
        float posY = transform.position.y - mainCamera.transform.position.y;
        float posZ = transform.position.z - mainCamera.transform.position.z;

        if (Input.GetKey(Config.Input.left))
        {
            rigidBody.velocity += Quaternion.Euler(0, -90, 0) * new Vector3(posX, 0, posZ).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(Config.Input.forward))
        {
            rigidBody.velocity += new Vector3(posX, 0, posZ).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(Config.Input.right))
        {
            rigidBody.velocity += Quaternion.Euler(0, -90, 0) * -new Vector3(posX, 0, posZ).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(Config.Input.backward))
        {
            rigidBody.velocity -= new Vector3(posX, 0, posZ).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(Config.Input.jump))
        {
            rigidBody.velocity -= new Vector3(0, posY, 0).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(Config.Input.sprint))
        {
            rigidBody.velocity += new Vector3(0, posY, 0).normalized * Time.deltaTime * speedFactor;
        }
    }

    void LimitVelocity()
    {
        if (rigidBody.velocity.x > maxVelocity)
        {
            rigidBody.velocity = new Vector3(maxVelocity, rigidBody.velocity.y, rigidBody.velocity.z);
        }

        if (rigidBody.velocity.x < -maxVelocity)
        {
            rigidBody.velocity = new Vector3(-maxVelocity, rigidBody.velocity.y, rigidBody.velocity.z);
        }

        if (rigidBody.velocity.y > maxVelocity)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, maxVelocity, rigidBody.velocity.z);
        }

        if (rigidBody.velocity.y < -maxVelocity)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, -maxVelocity, rigidBody.velocity.z);
        }

        if (rigidBody.velocity.z < -maxVelocity)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -maxVelocity);
        }

        if (rigidBody.velocity.z > maxVelocity)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, maxVelocity);
        }
    }

    void DecayVelocity()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x * velocityReduce,
                                         rigidBody.velocity.y * velocityReduce,
                                         rigidBody.velocity.z * velocityReduce);
    }

}
