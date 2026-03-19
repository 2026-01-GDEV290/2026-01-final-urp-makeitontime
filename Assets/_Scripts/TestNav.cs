using UnityEngine;
using UnityEngine.AI;

public class TestNav : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    public float walkRadius;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Reposition();
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 3)
        {
            Reposition();
        }
    }
    void Reposition()
    {
        Vector3 randomDir = UnityEngine.Random.insideUnitSphere * walkRadius;

        randomDir += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, walkRadius, 3);
        Vector3 finalPos = hit.position;

        agent.destination = finalPos;
    }
}
