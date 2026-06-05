using System.Collections;
using UnityEngine;

public class ResultsCutscene : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartCutscene());
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Start");
    }
}
