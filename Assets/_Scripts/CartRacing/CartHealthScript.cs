using UnityEngine;

public class CartHealthScript : MonoBehaviour
{
    [SerializeField]
    int hp = 3;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("civ"))
        {
            hp--;
            Debug.Log("hp: " + hp);
        }
    }
}
