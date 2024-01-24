using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testingscrpt : MonoBehaviour
{
    public Vector3 pos;
    void Awake()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            transform.position += Vector3.left;
            Debug.Log(transform.position);
            Debug.Log(pos);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position = pos;
        }
    }
}
