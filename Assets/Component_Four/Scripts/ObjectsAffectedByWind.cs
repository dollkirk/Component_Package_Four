using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsAffectedByWind : MonoBehaviour
{
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
