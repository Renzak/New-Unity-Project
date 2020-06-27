using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public GameObject mainCamera;
    
    public int speedFactor = 50;
    public int maxVelocity = 20;
    public float velocityReduce = 0.99f;

    Rigidbody rigidBody;


    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProccessUserInput();
        rigidBody.velocity = new Vector3(rigidBody.velocity.x*velocityReduce,
                                         rigidBody.velocity.y*velocityReduce,
                                         rigidBody.velocity.z*velocityReduce);
    }
    
    void ProccessUserInput()
    {
        float posX = transform.position.x - mainCamera.transform.position.x;
        float posY = transform.position.y - mainCamera.transform.position.y;
        float posZ = transform.position.z - mainCamera.transform.position.z;


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

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity -= new Vector3(0, posY, 0).normalized * Time.deltaTime * speedFactor;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rigidBody.velocity += new Vector3(0, posY, 0).normalized * Time.deltaTime * speedFactor;
        }

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

}
