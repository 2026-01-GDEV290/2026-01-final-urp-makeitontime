using UnityEngine;

public class StartCutSceneScript : MonoBehaviour
{
    GameObject DialougeSystem;
    GameObject PauseMenu;
    [SerializeField]
    Animator animator; 
    GameObject UI;
    GameFreezeScript gameFreeze;

    bool CutsceneRan;
    
    void Start()
    {
        DialougeSystem = GameObject.Find("DialougeSystem");
        PauseMenu = GameObject.Find("PauseMenu");
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
            gameObject.SetActive(false);
            UI.SetActive(true);
            CutsceneRan = true;
        }
    }
}
