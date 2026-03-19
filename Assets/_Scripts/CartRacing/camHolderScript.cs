using UnityEngine;

public class camHolderScript : MonoBehaviour
{
    [SerializeField]
    Transform carPos;
    void Update()
    {
        // follow the car's pos but only y rotation
        // rigidbodies get kinda weird when a cam is attached so this keeps the camera nice and smooth
        transform.position = carPos.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, carPos.eulerAngles.y, transform.eulerAngles.z);
    }
}
