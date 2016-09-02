using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
    public float lifetime = 2.0f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
