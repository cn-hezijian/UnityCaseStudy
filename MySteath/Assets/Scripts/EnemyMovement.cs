using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float deadZone = 5.0f;
    private Transform player;
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Animator anims;
    private HashIDs hash;
    private SimpleLocomotion locomotion;

    void Awake()
    {
        player = GameObject.FindWithTag(Tags.Player).transform;
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        anims = GetComponent<Animator>();
        hash = GameObject.FindWithTag(Tags.GameController).GetComponent<HashIDs>();
        nav.updateRotation = false;
        locomotion = new SimpleLocomotion(anims, hash);
        anims.SetLayerWeight(1, 1.0f);
        anims.SetLayerWeight(2, 1.0f);
        deadZone *= Mathf.Deg2Rad;
    }

    void OnAnimatorMove()
    {
        nav.velocity = anims.deltaPosition / Time.deltaTime;
        transform.rotation = anims.rootRotation;
    }
    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if(toVector == Vector3.zero)
        {
            return 0;
        }

        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;
        return angle;
    }

    void NavAnimSetup()
    {
        float speed;
        float angle;
        if(enemySight.playerInSight)
        {
            speed = 0.0f;
            angle = FindAngle(transform.forward, player.transform.position - transform.position, transform.up);
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
            if(Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0.0f;
            }
        }
        locomotion.Do(speed, angle);
    }

    void Update()
    {
        NavAnimSetup();
    }
}
