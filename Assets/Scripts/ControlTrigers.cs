using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTrigers : MonoBehaviour
{
    GameObject player;
    MovementControllerWithCamera movementControllerWithCamera;
    Fly fly;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Config.Tags.Player);
        fly = player.GetComponent<Fly>();
        movementControllerWithCamera = player.GetComponent<MovementControllerWithCamera>();
    }

    void Update()
    {
        ToggleFly();
    }

    void ToggleFly()
    {
        if (Input.GetKeyDown(Config.Input.flyToggle))
        {
            fly.enabled = !fly.enabled;
            movementControllerWithCamera.enabled = !movementControllerWithCamera.enabled;
            player.GetComponent<Rigidbody>().useGravity = movementControllerWithCamera.enabled;
        }
    }
}
