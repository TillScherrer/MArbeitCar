using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testforce : MonoBehaviour
{

    public Transform R;
    public Transform L;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(R.forward*Time.fixedDeltaTime, R.position);
        rb.AddForceAtPosition(L.forward * Time.fixedDeltaTime, L.position);
    }
}
