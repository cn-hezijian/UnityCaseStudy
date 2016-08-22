using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

    public float tumble = 10.0f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
