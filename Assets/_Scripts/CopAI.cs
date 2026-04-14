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
    public bool destroyed;
    [SerializeField]
    GameObject[] copGank;
    private void Awake()
    {
        destroyed = false;
        remapPath = true;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        playerTransform = FindFirstObjectByType<CartScript>().gameObject.transform;
       
        childSource = child.GetComponent<AudioSource>();
        copGank = GameObject.FindGameObjectsWithTag("CopGank");
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position,playerTransform.position);
        if (distance < 100)
        {
            agent.SetDestination(playerTransform.position);
        }
        else if (distance > 500 && remapPath)
        {
            cutOff();
        }
        else if (remapPath)
        {
            remapPath = false;
            agent.SetDestination(playerTransform.position);
            StartCoroutine(repath());
        }
        if (destroyed && !childSource.isPlaying)
        {
            destroyed = false;
            gameObject.SetActive(false);
        }
    }
    public void cutOff()
    {
        Transform chosenPoint = null;
        float chosenPointDistance = float.NaN;
        foreach (GameObject t in copGank)
        {
            if (chosenPoint == null)
            {
                chosenPoint = t.transform;
                chosenPointDistance = Vector3.Distance(t.transform.position, playerTransform.position);
            }
            else if (Vector3.Distance(t.transform.position, playerTransform.position) < chosenPointDistance)
            {
                chosenPoint = t.transform;
                chosenPointDistance = Vector3.Distance(t.transform.position, playerTransform.position);
            }
        }
        if (chosenPoint != null)
        {
            agent.transform.position = chosenPoint.position;
            remapPath = false;
            agent.SetDestination(playerTransform.position);
            StartCoroutine(repath());
        }
        else
        {
            remapPath = false;
            agent.SetDestination(playerTransform.position);
            StartCoroutine(repath());
        }
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
