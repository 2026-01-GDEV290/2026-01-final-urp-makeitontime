using UnityEngine;

public class StartCutSceneScript : MonoBehaviour
{
    GameObject DialougeSystem;
    [SerializeField]
    GameObject PauseMenu;
    [SerializeField]
    Animator animator; 
    GameObject UI;
    GameFreezeScript gameFreeze;

    [SerializeField]
    Vector3 startPos;

    bool CutsceneRan;
    
    void Start()
    {
        DialougeSystem = GameObject.Find("DialougeSystem");
        gameFreeze = FindFirstObjectByType<GameFreezeScript>();
        DialougeSystem.SetActive(false);
        PauseMenu.SetActive(false);    
        UI = GameObject.Find("UI");
        UI.SetActive(false);
        gameFreeze.cutsceneScreen();
        CutsceneRan = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Fin") && CutsceneRan == false)
        {
            gameFreeze.uncutsceneScreen();
            DialougeSystem.SetActive(true);
            PauseMenu.SetActive(true);
            //gameObject.SetActive(false);
            UI.SetActive(true);
            CutsceneRan = true;
        }

        if(CutsceneRan == true)
        {
            transform.localPosition = startPos;
        }
    }
}
