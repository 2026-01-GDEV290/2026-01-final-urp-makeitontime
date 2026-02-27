using UnityEngine;

public class CarSpriteAnimScript : MonoBehaviour
{
    //get car animator
    [SerializeField]
    Animator anim;
    // let our horizontal axis be accessible in the whole script
    float horizontal;
    void Start()
    {
        //set our animator to our car animator
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //logic!
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
        if (Input.GetKey(KeyCode.Period) && !Input.GetKey(KeyCode.Comma))
        {
            anim.SetBool("Reversing", true);
        }
        else
        {
            anim.SetBool("Reversing", false);
        }
    }
}
