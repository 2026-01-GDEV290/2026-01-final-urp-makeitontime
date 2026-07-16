using UnityEngine;

public class camHolderScript : MonoBehaviour
{
    [SerializeField]
    Transform carPos;
    void Start()
    {
        transform.localPosition = new Vector3(0,-1,0);
    }
    void Update()
    {
        // follow the car's pos but only y rotation
        // rigidbodies get kinda weird when a cam is attached so this keeps the camera nice and smooth
        transform.position = carPos.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, carPos.eulerAngles.y, transform.eulerAngles.z);
    }
}
