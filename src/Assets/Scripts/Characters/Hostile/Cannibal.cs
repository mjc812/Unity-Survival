using UnityEngine;
using UnityEngine.AI;

public enum MovingState
{
    IDLE,
    WALK,
    CHASE,
    ATTACK
}

public class Cannibal : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private float walkSpeed = 1.0f;
    private float maxWalkTime = 10.0f;
    private float totalWalkTime = 10.0f;
    private float destinationRadiusMin = 100.0f, destinationRadiusMax = 200.0f;

    private float chaseSpeed = 5.0f;
    private float playerChaseDistance = 15.0f;

    private float playerAttackDistance = 1.2f;
    private float timeBeforeAttack = 3.0f;
    private float totalBeforeAttackTime = 3.0f;

    public float rotationSpeed = 10f;

    private MovingState movingState = MovingState.WALK;
   
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        switch (movingState)
        {
            case MovingState.WALK:
                {
                    Walk();
                    break;
                }
            case MovingState.CHASE:
                {
                    Chase();
                    break;
                }
            case MovingState.ATTACK:
                {
                    Attack();
                    break;
                }
        }
    }

    private void Idle()
    {

    }

    private void Walk()
    {
        navMeshAgent.isStopped = false;
        movingState = MovingState.WALK;
        totalWalkTime += Time.deltaTime;
        navMeshAgent.speed = walkSpeed;
        animator.Play("Walk");

        if (CheckChaseDistance())
        {
            movingState = MovingState.CHASE;
        }
        else
        {
            if (totalWalkTime > maxWalkTime)
            {
                totalWalkTime = 0.0f;
                NavMeshHit nextDestination = GetNavMeshDestination();
                navMeshAgent.SetDestination(nextDestination.position);
            }
        }
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
        navMeshAgent.speed = chaseSpeed;
        animator.Play("Chase");
        RotateTowards();

        if (!CheckChaseDistance())
        {
            movingState = MovingState.WALK;
            totalWalkTime = maxWalkTime;
        }
        else if (CheckAttackDistance())
        {
            movingState = MovingState.ATTACK;
        }
    }

    private void Attack()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        totalBeforeAttackTime += Time.deltaTime;
        RotateTowards();

        Debug.Log(totalBeforeAttackTime);
        if (totalBeforeAttackTime > timeBeforeAttack)
        {
            totalBeforeAttackTime = 0.0f;
            animator.Play("Attack");
        }
        else if (!CheckAttackDistance())
        {
            totalBeforeAttackTime = timeBeforeAttack;
            movingState = MovingState.CHASE;
        }
    }

    private bool CheckAttackDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= playerAttackDistance;
    }

    private bool CheckChaseDistance()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        return dist <= playerChaseDistance;
    }

    private void RotateTowards()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

        private NavMeshHit GetNavMeshDestination()
    {
        float destinationRadius = Random.Range(
            destinationRadiusMin,
            destinationRadiusMax
        );

        Vector3 direction = Random.insideUnitSphere * destinationRadius;
        direction += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(direction, out navHit, destinationRadius, NavMesh.AllAreas);

        return navHit;
    }
}
