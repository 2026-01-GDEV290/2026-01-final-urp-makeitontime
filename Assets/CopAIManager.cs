using System.Collections.Generic;
using UnityEngine;

public class CopAIManager : MonoBehaviour
{
    float heat;
    [SerializeField]
    List<GameObject> cops;
    [SerializeField]
    GameObject copPrefab;
    public void addHeat()
    {
        heat++;
        spawnCop();
    }
    public void spawnCop()
    {
        
        GameObject cop = Instantiate(copPrefab, transform.position, Quaternion.identity);
        if((cops.Count + 1) % 2 == 0)
        {
            cop.GetComponent<CopAI>().alwaysChase = true;
            cop.GetComponent<CopAI>().currentState = CopAI.State.chasing;
        }
        cops.Add(cop);
    }
}
