using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTrigers : MonoBehaviour
{
    public GameObject ball;
    MovementControllerWithCamera movementControllerWithCamera;
    Fly fly;
    


    void Start()
    {
        fly = ball.GetComponent<Fly>();
        movementControllerWithCamera = ball.GetComponent<MovementControllerWithCamera>();
    }

    void Update()
    {
        ToggleFly();
    }

    void ToggleFly()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            movementControllerWithCamera.enabled = !movementControllerWithCamera.enabled;
            fly.enabled = !fly.enabled;
            ball.GetComponent<Rigidbody>().useGravity = movementControllerWithCamera.enabled;
        }
    }
}
