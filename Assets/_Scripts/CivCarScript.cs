using UnityEngine;

public class CivCarScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    bool destroy;
    [SerializeField]
    AudioSource source;

    [SerializeField]
    GAMEMANAGER gameManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameManager = GAMEMANAGER.Instance;
    }
    public void destroyThisCar()
    {
        animator.SetBool("destroyed",true);
        gameObject.GetComponent<Collider>().enabled = false;
    }


    private void Update()
    {
        if (destroy && !source.isPlaying)
        {
            gameManager.CarsHit++;
            gameObject.SetActive(false);
        }
    }
}
