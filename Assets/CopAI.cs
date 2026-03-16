using UnityEngine;
using UnityEngine.AI;

public class CopAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = FindFirstObjectByType<CartScript>().transform.position;
    }
}
