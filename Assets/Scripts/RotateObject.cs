using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public bool IsRotateYAxis;
    public bool IsRotateZAxis;

    private void Update()
    {
        if (IsRotateYAxis)
            transform.RotateAround(transform.position, transform.forward, Time.deltaTime * 90f);
        else if (IsRotateZAxis)
            transform.RotateAround(transform.position, transform.right, Time.deltaTime * 90f);
        else
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
}
