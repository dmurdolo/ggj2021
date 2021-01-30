using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public float Amount = 0.1f;
    public float Speed = 1.0f;
    public float Offset = 0.0f;

    void Start()
    { 
    }

    void Update()
    {
        transform.localScale = new Vector3(1, 1 + Mathf.Sin(Time.time * Speed + Offset) * Amount, 1);
    }
}
