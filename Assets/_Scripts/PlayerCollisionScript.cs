using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    [SerializeField]
    float hp = 3;
    [SerializeField]
    CartScript script;

    [SerializeField]
    public Vector3 prevVel;
    [SerializeField]
    float crashPunishment;

    private void Start()
    {
        script = GetComponent<CartScript>();
    } 

    private void Update()
    {
        prevVel = script.rb.linearVelocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "cop" && !script.CDFour)
        {
            FindFirstObjectByType<GameOverScript>().GameOver();
        }
        else if (collision.collider.tag == "cop" && script.CDFour)
        {
            collision.collider.gameObject.GetComponent<CopAI>().DestroyThisCar();
            script.rb.linearVelocity = prevVel;
        }

        if (collision.collider.tag.Equals("civ") && !script.CDFour)
        {
            script.rb.linearVelocity = prevVel - (prevVel * crashPunishment);
            collision.collider.gameObject.GetComponent<CivCarScript>().destroyThisCar();

        }
        else if (collision.collider.tag.Equals("civ") && script.CDFour)
        {
            collision.collider.gameObject.GetComponent<CivCarScript>().destroyThisCar();
            script.rb.linearVelocity = prevVel;
        }
    }
}
