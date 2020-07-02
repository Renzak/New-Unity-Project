using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTrigers : MonoBehaviour
{
    public GameObject player;
    MovementControllerWithCamera movementControllerWithCamera;
    Fly fly;
    
    void Start()
    {
        fly = player.GetComponent<Fly>();
        movementControllerWithCamera = player.GetComponent<MovementControllerWithCamera>();
    }

    void Update()
    {
        ToggleFly();
    }

    void ToggleFly()
    {
        if (Input.GetKeyDown(Config.flyToggleKeyCode))
        {
            if(movementControllerWithCamera.enabled)
            {
                movementControllerWithCamera.PrepareForDisabling();
                movementControllerWithCamera.enabled = false;
            }
            else
            {
                movementControllerWithCamera.PrepareForEnabling();
                movementControllerWithCamera.enabled = true;
            }
            fly.enabled = !fly.enabled;
            player.GetComponent<Rigidbody>().useGravity = movementControllerWithCamera.enabled;
        }
    }
}
