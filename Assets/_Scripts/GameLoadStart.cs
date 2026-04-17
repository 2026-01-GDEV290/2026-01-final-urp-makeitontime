using System.Collections;
using UnityEngine;

public class GameLoadStart : MonoBehaviour
{
    [SerializeField]
    LoadingScreenScript loadingScreenScript;
    private void Awake()
    {
        loadingScreenScript = GetComponent<LoadingScreenScript>();
    }
    private void Start()
    {
        StartCoroutine(gameStart());
    }
    IEnumerator gameStart()
    {
        yield return new WaitForSeconds(2);
            loadingScreenScript.LoadLevel(1); 
    }
}
