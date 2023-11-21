using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestImpulse : MonoBehaviour
{
    public Rigidbody rb;
    public bool splitImpulse;
    // Start is called before the first frame update
    void Start()
    {
        if (splitImpulse)
        {
            rb.AddForceAtPosition(Vector3.forward * 0.5f, rb.position, ForceMode.Impulse);
            rb.AddForceAtPosition(Vector3.forward * 0.5f, rb.position, ForceMode.Impulse);
        }
        else
        {
            rb.AddForceAtPosition(Vector3.forward , rb.position, ForceMode.Impulse);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(-rb.velocity, rb.position, ForceMode.Impulse);
    }
}
