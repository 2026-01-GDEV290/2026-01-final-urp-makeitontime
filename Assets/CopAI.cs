using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CopAI : MonoBehaviour
{
    Transform playerTransform;
    NavMeshAgent agent;
    bool remapPath;

    private void Awake()
    {
        remapPath = true;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        playerTransform = FindFirstObjectByType<CartScript>().gameObject.transform;
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position,playerTransform.position);
        if (distance < 75)
        {
            agent.SetDestination(playerTransform.position);
        }else if(remapPath)
        {
            remapPath = false;
            agent.SetDestination(playerTransform.position);
            StartCoroutine(repath());
        }
    }
    IEnumerator repath()
    {
        yield return new WaitForSeconds(5);
        remapPath = true;
    }
}
