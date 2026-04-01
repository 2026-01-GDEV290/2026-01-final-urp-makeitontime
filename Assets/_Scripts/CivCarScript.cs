using UnityEngine;

public class CivCarScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    bool destroy;
    [SerializeField]
    AudioSource source;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
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
            gameObject.SetActive(false);
        }
    }
}
