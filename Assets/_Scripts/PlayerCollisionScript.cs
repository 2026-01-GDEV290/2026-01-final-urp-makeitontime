using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "cop")
        {
            FindFirstObjectByType<GameOverScript>().GameOver();
        }
    }
}
