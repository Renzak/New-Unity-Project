using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    x,
    y,
    z,
}

public class ConstantRotation : MonoBehaviour
{
    public Axis axis;
    public float rotationSpeed;

    Vector3 rotationVector;
    private void Start()
    {
        rotationVector = new Vector3(axis == Axis.x ? 1 : 0, axis == Axis.y ? 1 : 0, axis == Axis.z ? 1 : 0);
    }

    void Update()
    {
        gameObject.transform.Rotate(
            rotationVector, rotationSpeed * Time.deltaTime);
    }
}
