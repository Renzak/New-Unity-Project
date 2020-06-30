using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    public string nextSceneName;
    public GameObject mainCamera;

    GameObject respawnPoint;
    CameraOnObjectMovement cameraOnObjectMovement;

    readonly string DEAD_TAG = "DeadPit";
    readonly string RESPAWN_OBJECT_NAME = "RespawnPoint";
    readonly string CHECKPOINT_TAG = "Checkpoint";

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = GameObject.Find(RESPAWN_OBJECT_NAME);
        cameraOnObjectMovement = GetComponent<CameraOnObjectMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(DEAD_TAG))
        {
            transform.position = respawnPoint.transform.position;
            cameraOnObjectMovement.SetCameraLookDirection(respawnPoint.transform.rotation.eulerAngles.y);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (other.gameObject.CompareTag(CHECKPOINT_TAG))
        {
            respawnPoint.transform.parent = null;
            respawnPoint.transform.rotation = other.transform.rotation;
            respawnPoint.transform.position = other.transform.position;
            respawnPoint.transform.parent = other.transform;
        }

    }
}
