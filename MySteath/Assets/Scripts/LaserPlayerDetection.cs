using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {

    private Renderer m_Render;
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;
    // Use this for initialization
    void Start ()
    {
        m_Render = GetComponent<Renderer>();
        player = GameObject.FindWithTag(Tags.Player);
        lastPlayerSighting = GameObject.FindWithTag(Tags.GameController).GetComponent<LastPlayerSighting>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (m_Render.enabled)
        {
            if(other.gameObject == player)
            {
                lastPlayerSighting.position = other.transform.position;
            }
        }
    }
}
