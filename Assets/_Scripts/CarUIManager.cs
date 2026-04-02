using UnityEngine;

public class CarUIManager : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    RectTransform dialTransform;

    // Update is called once per frame
    void Update()
    {
        moveDial();
    }

    void moveDial()
    {
        float vel = Mathf.Round(rb.linearVelocity.magnitude);
        float percent = vel / 80;
        float rotation = (percent * 85) - 45;

        Vector3 newRotation = new Vector3(dialTransform.rotation.x,dialTransform.transform.rotation.y,rotation);

        dialTransform.rotation = Quaternion.Euler(newRotation);
    }
}
