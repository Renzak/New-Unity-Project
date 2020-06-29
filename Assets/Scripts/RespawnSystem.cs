using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    GameObject RespawnPoint;

    public string nextSceneName;

    readonly string DEAD_TAG = "DeadPit";
    readonly string RESPAWN_OBJECT_NAME = "RespawnPoint";
    readonly string CHECKPOINT_TAG = "Checkpoint";

    // Start is called before the first frame update
    void Start()
    {
        RespawnPoint = GameObject.Find(RESPAWN_OBJECT_NAME);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(DEAD_TAG))
        {
            transform.position = RespawnPoint.transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (other.gameObject.CompareTag(CHECKPOINT_TAG))
        {
            RespawnPoint.transform.position = other.gameObject.transform.position;
        }

    }
}
