using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    public bool canLookVertically;

    private void Awake()
    {
        target = Camera.main.transform;
    }
    private void Update()
    {
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
