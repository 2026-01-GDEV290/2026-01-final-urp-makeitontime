using UnityEngine;

public class camHolderScript : MonoBehaviour
{
    [SerializeField]
    Transform carPos;
    void Update()
    {
        transform.position = carPos.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, carPos.eulerAngles.y, transform.eulerAngles.z);
    }
}
