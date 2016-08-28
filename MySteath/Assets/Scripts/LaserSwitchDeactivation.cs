using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour {

    public GameObject laser;
    public Material unlockedMat;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag(Tags.Player);
    }

    void LaserDeactivation()
    {
        laser.SetActive(false);
        Renderer screen = transform.Find("prop_switchUnit_screen").GetComponent<Renderer>();
        screen.material = unlockedMat;
        GetComponent<AudioSource>().Play();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            if(Input.GetButton("Switch"))
            {
                LaserDeactivation();
            }
        }
    }
}
