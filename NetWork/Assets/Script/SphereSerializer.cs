using UnityEngine;
using System.Collections;

public class SphereSerializer : MonoBehaviour {

    private Rigidbody rigidbody;
    private NetworkView networkView;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        networkView = GetComponent<NetworkView>();
        networkView.observed = rigidbody;
    }
}
