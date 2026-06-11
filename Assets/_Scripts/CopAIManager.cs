using System.Collections.Generic;
using UnityEngine;

public class CopAIManager : MonoBehaviour
{
    float heat;
    [SerializeField]
    List<GameObject> cops;
    [SerializeField]
    GameObject copPrefab;

    private void Awake()
    {
        GAMEMANAGER.Instance.CopsCalled = 0;
    }
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
        GAMEMANAGER.Instance.CopsCalled++;
        cops.Add(cop);
    }
}
