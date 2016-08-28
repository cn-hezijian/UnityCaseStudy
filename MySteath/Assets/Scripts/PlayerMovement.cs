using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;
    private Animator animator;
    private HashIDs hash;
    private Rigidbody rb;
    private AudioSource audioSource;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hash = GameObject.FindWithTag(Tags.GameController).GetComponent<HashIDs>();
        animator.SetLayerWeight(1, 1);
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Rotating(float h, float v)
    {
        Vector3 targetDir = new Vector3(h, 0, v);
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        rb.MoveRotation(newRotation);
    }

    void MovementManagement(float h, float v, bool sneaking)
    {
        animator.SetBool(hash.sneakingBool, sneaking);
        if (h != 0 || v != 0)
        {
            Rotating(h, v);
            animator.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            animator.SetFloat(hash.speedFloat, 0.0f);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        MovementManagement(h, v, sneak);
    }

    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        animator.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);

    }

    void AudioManagement(bool shout)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }    

    }
}
