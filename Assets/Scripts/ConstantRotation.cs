using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public enum Axis
    {
        x,
        y,
        z,
    }

    public Axis axis;
    public float rotationSpeed;

    void Update()
    {
        gameObject.transform.Rotate(
            new Vector3( axis == Axis.x? 1 : 0, axis == Axis.y? 1 : 0, axis == Axis.z? 1 : 0), rotationSpeed * Time.deltaTime);
    }
}
