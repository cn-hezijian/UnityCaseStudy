using UnityEngine;
using System.Collections;

public class MyBear : MonoBehaviour
{
    public float AvatarRange = 25;
    private Animator animator;
    private float speedDampTime = 0.25f;
    private float DirectionDampTime = 0.25f;
    private Vector3 TargetPosition = new Vector3(0, 0, 0);

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator == null)
            return;
        int r = Random.Range(0, 50);
        animator.SetBool("Jump", r == 20);
        animator.SetBool("Dive", r == 30);

        if (Vector3.Distance(animator.rootPosition, TargetPosition) > 5)
        {
            animator.SetFloat("Speed", 1.0f, speedDampTime, Time.deltaTime);
            Vector3 currentDir = animator.rootRotation * Vector3.forward;
            Vector3 wantDir = (TargetPosition - animator.rootPosition).normalized;
            if (Vector3.Dot(currentDir, wantDir) > 0)
            {
                animator.SetFloat("Direction", Vector3.Cross(currentDir, wantDir).y,
                    DirectionDampTime, Time.deltaTime);
            }
            else
            {
                animator.SetFloat("Direction", Vector3.Cross(currentDir, wantDir).y > 0 ? 1 : -1,
                    DirectionDampTime, Time.deltaTime);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0, speedDampTime, Time.deltaTime);
            if (animator.GetFloat("Speed") < 0.01f)
            {
                TargetPosition = new Vector3(
                    Random.Range(-AvatarRange, AvatarRange),
                    0,
                    Random.Range(-AvatarRange, AvatarRange)
                    );
            }
        }

        if(animator)
        {
            var nextState = animator.GetNextAnimatorStateInfo(0);
            if(nextState.IsName("Base Layer.Dying"))
            {
                animator.SetBool("Dying", false);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(animator != null)
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextState = animator.GetNextAnimatorStateInfo(0);
            if(!currentState.IsName("Base Layer.Dying") && !currentState.IsName("Base Layer.Dying"))
            {
                animator.SetBool("Dying", true);
            }
        }
    }
}
