using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CartScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text debugText;
    //[CAUTION]
    //THIS CODE WAS WRITTEN BY A FAGGOT!!!!!!!!!
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Transform orientation;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float maxMoveSpeed;

    [SerializeField]
    float maxTurnSpeed;

    [SerializeField]
    float turnSpeed;

    [SerializeField]
    float alignmentSpeed;

    [SerializeField]
    float groundDrag;

    [SerializeField]
    float carHeight;

    [SerializeField]
    LayerMask GroundLayer;

    [SerializeField]
    Transform backLeftCar;
    [SerializeField]
    Transform frontLeftCar;
    [SerializeField]
    Transform backRightCar;
    [SerializeField]
    Transform frontRightCar;
    bool grounded;

    [SerializeField]
    bool isDrifting;

    Vector3 moveDir;

    float horizontal;

    bool isAccelerating;
    bool isBreaking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnSpeed = maxTurnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "MPH: " + Mathf.Round(rb.linearVelocity.magnitude);

        grounded = Physics.Raycast(transform.position, Vector3.down, carHeight * .5f + .2f, GroundLayer);

        MyInput();

        SpeedControl();

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }
    private void FixedUpdate()
    {
        AlignToRamp();
        if (grounded)
        {
            rb.angularVelocity = Vector3.zero;
            MovePlayer();
        }
    }
    void AlignToRamp()
    {
        Vector3 upDir = transform.up;

        // L=Left,R=Right,B=back,F=Front
        RaycastHit LB;
        RaycastHit RB;
        RaycastHit LF;
        RaycastHit RF;
        Physics.Raycast(backLeftCar.position + Vector3.up, Vector3.down, out LB);
        Physics.Raycast(backRightCar.position + Vector3.up, Vector3.down, out RB);
        Physics.Raycast(frontLeftCar.position + Vector3.up, Vector3.down, out LF);
        Physics.Raycast(frontRightCar.position + Vector3.up, Vector3.down, out RF);
        if (!grounded)
        {
            upDir = Vector3.up;
        }
        else
        {
            upDir = (Vector3.Cross(RB.point - Vector3.up, LB.point - Vector3.up) +
                            Vector3.Cross(LB.point - Vector3.up, LF.point - Vector3.up) +
                            Vector3.Cross(LF.point - Vector3.up, RF.point - Vector3.up) +
                            Vector3.Cross(RF.point - Vector3.up, RB.point - Vector3.up)).normalized;
        }
            Vector3 currentForward = rb.rotation * Vector3.forward;

            Vector3 projForward = Vector3.ProjectOnPlane(currentForward, upDir).normalized;

            if (projForward.sqrMagnitude < 0.001f)
            {
                projForward = Vector3.Cross(transform.right, upDir).normalized;
            }
            Quaternion targetRotation = Quaternion.LookRotation(projForward, upDir);

            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation,targetRotation,360 * Time.deltaTime));
        
    }
    void MovePlayer()
    {
        if (isAccelerating)
        {
            moveDir = transform.forward;

            rb.AddForce(moveDir * moveSpeed * 10f, ForceMode.Force);   
        }
        if (isDrifting)
        {
            //Drift;
        }
        if (isBreaking)
        {
            moveDir = -transform.forward;

            rb.AddForce(moveDir * moveSpeed * 10f, ForceMode.Force);
        }

        if (rb.linearVelocity.magnitude >= 0.1f)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
            float sidewaysSpeed = localVelocity.x;
            Vector3 antiSlideForce = -transform.right * sidewaysSpeed * alignmentSpeed * rb.mass;
            rb.AddForce(antiSlideForce, ForceMode.Force);

            float turnAngle = horizontal * turnSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0f, turnAngle, 0f);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }
    void MyInput()
    {
        isAccelerating = Input.GetKey(KeyCode.Comma);
        isBreaking = Input.GetKey(KeyCode.Period);
        horizontal = Input.GetAxisRaw("Horizontal");
        isDrifting = Input.GetKey(KeyCode.LeftShift);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > maxMoveSpeed)
        {
            Vector3 limVel = flatVel.normalized * maxMoveSpeed;
            rb.linearVelocity = new Vector3(limVel.x, rb.linearVelocity.y, limVel.z);
        }
    }
}
