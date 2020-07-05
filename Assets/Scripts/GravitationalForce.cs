using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForce : MonoBehaviour
{
    public float forceFactor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(Config.PLAYER_TAG))
            return;
        Vector3 vectorTowardCurrentObject = (transform.position - other.transform.position);
        other.gameObject.GetComponent<Rigidbody>().velocity += (vectorTowardCurrentObject.normalized / vectorTowardCurrentObject.magnitude) * forceFactor;
    }
}
