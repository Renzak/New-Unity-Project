using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRespawn : MonoBehaviour
{
    readonly string DEAD_TAG = "DeadPit";
    Vector3 respawnPosition;

    void Start()
    {
        respawnPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(DEAD_TAG))
        {
            transform.position = respawnPosition;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}
