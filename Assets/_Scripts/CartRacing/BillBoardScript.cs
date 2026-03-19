using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    //the thing that the billboard wants to look at
    [SerializeField]
    private Transform target;

    //some things we don't want facing the camera vertically like the cars
    //but somethings we may want to have facing the cam like collectables
    [SerializeField]
    public bool canLookVertically;

    private void Start()
    {
        target = Camera.main.transform;
    }

    private void LateUpdate()
    {
        //if we can look vertically look directly at the target otherwise
        //look at the target's x and z position but this object's y position
        if (canLookVertically)
        {
            transform.LookAt(target);
        }
        else
        {
            Vector3 modTarget = target.position;
            modTarget.y = transform.position.y;

            transform.LookAt(modTarget);
        }
    }
}
