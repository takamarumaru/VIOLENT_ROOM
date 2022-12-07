using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForwardMove : MonoBehaviour
{

    private Rigidbody rigidbody;
    [SerializeField] float power;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * power, ForceMode.Impulse);
    }
}
