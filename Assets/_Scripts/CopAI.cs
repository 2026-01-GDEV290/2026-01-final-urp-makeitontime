using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CopAI : MonoBehaviour
{
    Transform playerTransform;
    NavMeshAgent agent;
    bool remapPath;

    [SerializeField]
    AudioClip deathSound;
    [SerializeField]
    GameObject child;
    [SerializeField]
    AudioSource childSource;

    [SerializeField]
    float chaseSpeed;
    [SerializeField]
    float interceptSpeed;

    [SerializeField]
    Transform goal;
    public bool destroyed;

    public bool alwaysChase;

    public enum State {chasing, intercept, waiting};

    private float waitTime;
    [SerializeField]
    public State currentState;

    public CopAIManager pigManager;

    private void Awake()
    {
        destroyed = false;
        remapPath = true;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        playerTransform = FindFirstObjectByType<CartScript>().gameObject.transform;

        if(FindFirstObjectByType<GoalScript>() != null)
        {
            goal = FindFirstObjectByType<GoalScript>().gameObject.transform;
        }else
        {
            Debug.LogError("No goal found in scene");
            goal = transform;
        }

       // child = GetComponentInChildren<GameObject>();
        //childSource = child.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (gameObject.activeInHierarchy == false)
        {
            return;
        }

        if(!alwaysChase)
        {
            setState();
        }
        stateLogic();

       /* float distance = Vector3.Distance(transform.position,playerTransform.position);
        if (distance < 75 && agent.enabled == true)
        {
            agent.SetDestination(playerTransform.position);
        }else if(remapPath)
        {
            remapPath = false;
            agent.SetDestination(playerTransform.position);
            StartCoroutine(repath());
        }*/

        if (destroyed && !childSource.isPlaying)
        {
            destroyed = false;
            agent.enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void setState()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, playerTransform.position);
        float distanceFromTarget = Vector3.Distance(transform.position, agent.destination);

        if(distanceFromTarget < 5 && agent.enabled == true && currentState != State.waiting)
        {
            currentState = State.waiting;
            waitTime = Random.Range(5, 10);
        }
        if(distanceFromTarget < 5 && agent.enabled == true)
        {
            currentState = State.waiting;
        }
        if(distanceFromPlayer > 200 && agent.enabled == true && currentState != State.intercept)
        {
            currentState = State.waiting;
            setInterceptPoint();
        }
        if(distanceFromPlayer > 200 && agent.enabled == true && currentState != State.waiting)
        {
            currentState = State.intercept;
        }
        if(distanceFromPlayer < 200 && agent.enabled == true)
        {
            currentState = State.chasing;
        }
    }

    private void stateLogic()
    {
        switch(currentState)
        {
            case State.chasing:
                agent.SetDestination(playerTransform.position);
                agent.speed = chaseSpeed;
                break;
            case State.intercept:
                agent.speed = interceptSpeed;
                break;
            case State.waiting:
                waitTime -= Time.deltaTime;
                if(waitTime < 0)
                {
                    currentState = State.intercept;
                }
                break;
        }
    }

    private void setInterceptPoint()
    {
        Vector3 middlePoint = (playerTransform.position + goal.position) / 2;
        Vector3 direction = (middlePoint - transform.position).normalized;
        Vector3 interceptPoint = middlePoint + direction * 10;
        agent.SetDestination(interceptPoint);
    }
    public void DestroyThisCar()
    {
        child.GetComponent<Animator>().SetBool("destroyed", true);
        GetComponent<Collider>().enabled = false;
        childSource.loop = false;
        childSource.Stop();
        childSource.PlayOneShot(deathSound);
        agent.enabled = false;
        StartCoroutine(waitToDestroy());
    }
    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(.05f);
        destroyed = true;
    }
    IEnumerator repath()
    {
        yield return new WaitForSeconds(5);
        remapPath = true;
    }
}
