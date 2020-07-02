using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRespawn : MonoBehaviour
{
    Vector3 respawnPosition;

    void Start()
    {
        respawnPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Config.DEAD_TAG))
        {
            transform.position = respawnPosition;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}
