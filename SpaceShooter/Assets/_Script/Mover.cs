using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float speed = 10.0f;
    private Rigidbody rb;
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.velocity = speed * transform.forward;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
