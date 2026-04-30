using System.Collections;
using UnityEngine;

public class GameLoadStart : MonoBehaviour
{
    LoadingScreenScript loadingScreenScript;
    [SerializeField]
    Animator blowyAnim;
    private void Awake()
    {
        loadingScreenScript = GetComponent<LoadingScreenScript>();
    }
    private void Start()
    {
        blowyAnim.SetTrigger("start");
        StartCoroutine(gameStart());
    }
    IEnumerator gameStart()
    {
        yield return new WaitUntil(() => blowyAnim.GetNextAnimatorStateInfo(0).IsName("Fin"));
        loadingScreenScript.LoadLevel(1); 
    }
}
