using System.Collections;
using UnityEngine;

public class GameLoadStart : MonoBehaviour
{
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
        yield return new WaitForSeconds(1);
            loadingScreenScript.LoadLevel(1); 
    }
}
