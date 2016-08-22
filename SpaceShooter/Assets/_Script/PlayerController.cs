using UnityEngine;
using System.Collections;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour {
    public float Speed = 5.0f;
    public Boundary boundary;

    public float tilt = 4.0f;

    private Rigidbody rb = null;
    private AudioSource fireAudio = null;
    #region 子弹发射相关
    public float fireRata = 0.5f;
    public GameObject shot = null;
    public Transform shotSpawn = null;
    private float nextFireTime = 0.0f;
    #endregion

    // Use this for initializations
    void Start () {
        rb = GetComponent<Rigidbody>();
        fireAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRata;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            if (fireAudio != null)
            {
                fireAudio.Play();
            }
        }    
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0 , moveVertical);
        if (rb != null)
        {
            rb.velocity = movement * Speed;
            rb.position = new Vector3(
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
                );

            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * (-tilt));
        }
    }
}
