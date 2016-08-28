using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

    public float maximumDamage = 120f;
    public float minimumDamage = 45f;
    public AudioClip shotClip;
    public float flahIntensity = 3.0f;
    public float fadeSpeed = 10f;
    private Animator anim;
    private HashIDs hash;
    private LineRenderer laserShotLine;
    private Light laserShotLight;
    private SphereCollider col;
    private Transform player;
    private PlayerHealth playerHealth;
    private bool shooting;
    private float scaledDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindWithTag(Tags.Player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        hash = GameObject.FindWithTag(Tags.GameController).GetComponent<HashIDs>();
        laserShotLine.enabled = false;
        laserShotLight.intensity = 0f;
        scaledDamage = maximumDamage - minimumDamage;
    }

    void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(hash.aimWeightFloat);
        
    }
}
