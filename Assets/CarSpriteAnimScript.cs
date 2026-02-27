using UnityEngine;

public class CarSpriteAnimScript : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    float horizontal;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal >= 0.01f)
        {
            anim.SetBool("RightTurn", false);
            anim.SetBool("LeftTurn", true);
        }
        else if (horizontal <= -0.01f)
        {
            anim.SetBool("RightTurn", true);
            anim.SetBool("LeftTurn", false);
        }
        else
        {
            anim.SetBool("LeftTurn", false);
            anim.SetBool("RightTurn", false);
        }
    }
}
