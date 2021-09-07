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
    private float chaseSpeed = 4.0f;
    private float playerChaseDistance = 10.0f;
    private float playerAttackDistance = 1.0f;
    private float destinationRadiusMin = 20.0f, destinationRadiusMax = 60.0f;
    private float timeBeforeAttack = 2.0f;
    private float maxWalkTime = 4.0f;

    private MovingState movingState = MovingState.WALK;
    private float totalWalkTime = 4.0f;
    private float totalBeforeAttackTime = 0.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        Debug.Log(player);
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
        navMeshAgent.isStopped = true;

        totalBeforeAttackTime += Time.deltaTime;

        if (totalBeforeAttackTime > timeBeforeAttack)
        {
            totalBeforeAttackTime = 0.0f;
            Debug.Log("Attacking");
            animator.Play("Attack");
        }
        else if (!CheckAttackDistance())
        {
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
