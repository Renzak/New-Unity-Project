using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public float speedFactor;
    public float maxVelocity;

    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 vectorTowardsTarget = (target.transform.position - this.transform.position).normalized;

        if (Mathf.Abs(rigidbody.velocity.x) < maxVelocity &&
            Mathf.Abs(rigidbody.velocity.y) < maxVelocity &&
            Mathf.Abs(rigidbody.velocity.z) < maxVelocity)
            rigidbody.velocity += vectorTowardsTarget * Time.deltaTime * speedFactor;
    }
}
