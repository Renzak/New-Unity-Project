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

    void Start()
    {
        respawnPoint = GameObject.Find(Config.RESPAWN_OBJECT_NAME);
        cameraOnObjectMovement = GetComponent<CameraOnObjectMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Config.ENEMY_TAG))
        {
            transform.position = respawnPoint.transform.position;
            cameraOnObjectMovement.SetCameraLookDirection(respawnPoint.transform.rotation.eulerAngles.y);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Config.DEAD_TAG))
        {
            transform.position = respawnPoint.transform.position;
            cameraOnObjectMovement.SetCameraLookDirection(respawnPoint.transform.rotation.eulerAngles.y);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (other.gameObject.CompareTag(Config.CHECKPOINT_TAG))
        {
            respawnPoint.transform.parent = null;
            respawnPoint.transform.rotation = other.transform.rotation;
            respawnPoint.transform.position = other.transform.position;
            respawnPoint.transform.parent = other.transform;
        }
    }
}
