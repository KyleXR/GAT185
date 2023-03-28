using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Sensor sensor;
    [SerializeField] Transform attackTransform;
    [SerializeField] float damage;
    
    private Camera mainCamera;
    private NavMeshAgent agent;
    private Transform target;

    private State state = State.IDLE;
    private float timer = 0;

    enum State
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        DEATH
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        GetComponent<Health>().onDeath += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                state = State.PATROL;
                break;
            case State.PATROL:
                agent.isStopped = false;
                target = GetComponent<WaypointNavigator>().waypoint.transform;
                if(sensor.sensed != null)
                {
                    state = State.CHASE;
                }
                break;
            case State.CHASE:
                agent.isStopped = false;
                if (sensor.sensed != null)
                {
                    target = sensor.sensed.transform;
                    float distance = Vector3.Distance(target.position, transform.position);
                    if (distance <= 2)
                    {
                        StartCoroutine(Attack());
                    }
                    timer = 2;
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    state = State.PATROL;
                }
                break;
            case State.ATTACK:
                agent.isStopped = false;
                break;
            case State.DEATH:
                agent.isStopped = false;
                break;
            default:
                break;
        }
        agent.SetDestination(target.position);
        animator.SetFloat("Speed", agent.velocity.magnitude);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        agent.SetDestination(hit.point);
        //    }
        //}
    }

    void OnDeath()
    {
        StartCoroutine(Death());
    }

    IEnumerator Attack()
    {
        state = State.ATTACK;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(4.0f);
        state = State.CHASE;
    }
    IEnumerator Death()
    {
        state = State.DEATH;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }

    void OnAnimAttack()
    {
        Debug.Log("Attack");
        var colliders = Physics.OverlapSphere(attackTransform.position, 2);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.TryGetComponent<Health>(out Health health))
                {
                    health.OnApplyDamage(damage);
                    break;
                }
            }
        }
    }
}
