using UnityEngine;

public class CartHealthScript : MonoBehaviour
{
    [SerializeField]
    int hp = 3;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("civ"))
        {
            hp--;
        }
    }
}
