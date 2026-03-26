using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CartScript : MonoBehaviour
{
    /*
       Welcome to the kart script my name is emery and this your tour of
       How the hell this all works 
     */

    //This is the UI text for the MPH idk if it actually is going so and so MPH
    //but idrc;

    [SerializeField]
    TMP_Text debugText;

    //Our car's rigid body;
    [SerializeField]
    Rigidbody rb;

    /*These are our multipliers for out drifting
    * I have it set so you can change both the "driftAlignMult" and turn speed
    * in the game i have the turnMult equal to 1 so nothing happens
    */
    [SerializeField]
    float driftAlignMult;
    [SerializeField]
    float driftTurnMult;
    // The movement speed of the car as a float
    [SerializeField]
    float moveSpeed;
    float acceleration;
    // The max movement speed of the car I have to have this otherwise the car could amass
    // far too much speed for the player to handle also smthing about max speed n intertia or physics whatever
    [SerializeField]
    float maxMoveSpeed;

    //How quickly the camera turns
    [SerializeField]
    float turnSpeed;
    //How quickly the car aligns the force it's going towards where it's facing
    [SerializeField]
    float alignmentSpeed;

    //The linear inertia of the car's rigidbody
    [SerializeField]
    float groundDrag;

    //How tall the car is
    [SerializeField]
    float carHeight;

    //What is ground?
    [SerializeField]
    LayerMask GroundLayers;
    [SerializeField]
    LayerMask Road;
    [SerializeField]
    LayerMask Ground;

    //the three "wheels" of the car
    [SerializeField]
    Transform backLeftCar;
    [SerializeField]
    Transform frontLeftCar;
    [SerializeField]
    Transform backRightCar;
    [SerializeField]
    Transform frontRightCar;

    //check if the car is grounded
    bool grounded;

    //the direction we want to move the car
    Vector3 moveDir;

    //the horizontal axis
    float horizontal;

    //control bools set in MyInput()
    bool isAccelerating;
    bool isBreaking;
    bool isDrifting;
    bool jump;

    //Physics Shenanigans
    bool canMoveAlongY;

    [SerializeField]
    float jumpForce;

    bool canJump;

    [SerializeField]
    bool CDOne;
    [SerializeField]
    bool CDTwo;
    [SerializeField]
    bool CDThree;
    [SerializeField]
    bool CDFour;

    private void Awake()
    {
        acceleration = moveSpeed;
        if (CDTwo)
        {
            alignmentSpeed = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity.y > 0.1f && grounded)
        {
            Debug.Log("Jump at : " + rb.position.x + "x " + rb.position.y + "y " + rb.position.z + "z");
        }
        //Update the MPH text
        debugText.text = "MPH: " + Mathf.Round(rb.linearVelocity.magnitude);
        //Shoots a ray downwards half of the car's size with an added .2 for error
        //set grounded to true if the ray collides with an object with they layer tag
        grounded = Physics.Raycast(transform.position, Vector3.down, carHeight * .5f + .2f, GroundLayers);

        //Controls all the input in one spot
        MyInput();

        //Caps the speed of the car at maxMoveSpeed
        SpeedControl();


        //if the car is touching the ground set linearDamping to our groundDrag value
        //otherwise set the linearDamping to 0
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }

        checkIfNeedToMoveY();

        setSpeed();
    }
    private void FixedUpdate()
    {
        //Method to align our car to ramp (literally the worst thing ever)
        AlignToRamp();

        //only do movement shenanigans when the car is grounded because a car cant move itself in air
        if (grounded)
        {
            //this stops the car from rotating after it his the ground
            rb.angularVelocity = Vector3.zero;

            //Method to do more physics bullshit that i hate
            MovePlayer();
        }
    }
    void AlignToRamp()
    {
        //create a new vector called upDir = to transform.up this value will be changed later so it doesnt really matter what its initial
        //value is this just makes sure that god forbid the logic gods decide that bool has a 3rd state (null) the entire script doesnt break
        Vector3 upDir = transform.up;

        //shoot rays down infinitely from all of our "wheels" and outputs the hit data to each of the wheels Raycasthit Vars

        // L=Left,R=Right,B=back,F=Front
        RaycastHit LB;
        RaycastHit RB;
        RaycastHit LF;
        RaycastHit RF;
        Physics.Raycast(backLeftCar.position + Vector3.up, Vector3.down, out LB);
        Physics.Raycast(backRightCar.position + Vector3.up, Vector3.down, out RB);
        Physics.Raycast(frontLeftCar.position + Vector3.up, Vector3.down, out LF);
        Physics.Raycast(frontRightCar.position + Vector3.up, Vector3.down, out RF);

        //if the car is in the air set the car to be flat with the ground normally I'd set this to transform.up but since the car isn't supposed to really have a z axis
        //it makes more sense to set it's default state to flat. also makes the car transition to the floor nicely
        if (!grounded)
        {
            upDir = Vector3.up;
            canJump = false;
        }
        else
        {
            canJump = true;
            /* ok so Vector3.Cross produces the cross product of two vectors
             * basically what this means is that it takes the two input vectors and 
             * outputs a 3rd which is perpendicular to the other 2 vectors
             * 
             * so when i cross 2 Vectors R(ight)B(ack) and L(eft)B(ack) I'm essentially finding the direction
             * perpendicular to the 2 Vectors and by substracting them by Vector3.up I gurantee that the Cross
             * Vector is always pointing up relative to the "line" between the two points by doing this in a 
             * square RB -> LB -> LF -> RF -> RB I'm finding the overall upward direction of the car were
             * it a box
             * 
             * then I normalize it so the magnitude is 1
             */

            upDir = (Vector3.Cross(RB.point - Vector3.up, LB.point - Vector3.up) +
                            Vector3.Cross(LB.point - Vector3.up, LF.point - Vector3.up) +
                            Vector3.Cross(LF.point - Vector3.up, RF.point - Vector3.up) +
                            Vector3.Cross(RF.point - Vector3.up, RB.point - Vector3.up)).normalized;
        }

            /*set the current forward to the car's rigidbody.rotation multiplied by global forward
             *this flattens the velocity for further calculations
            */
            Vector3 currentForward = rb.rotation * Vector3.forward;

            /*Vector3.ProjectOnPlane does as it sounds, projects a vector onto a plane
             *In this case the play is our upDir which allows the car's flat velocity to be titled into the 3rd
             *dimension. This is so when our car goes onto an angled surface it acts as though it was riding
             *up the surface instead of into it
            */
            Vector3 projForward = Vector3.ProjectOnPlane(currentForward, upDir).normalized;

            /* in the case that our rigidbody becomes stationary on a platform create a vector
             * perpindicular ro right and upDir to serve as forward direction
             */
            if (projForward.sqrMagnitude < 0.001f)
            {
                projForward = Vector3.Cross(transform.right, upDir).normalized;
            }
            
            /* LookRotation creates a rotation with a forward and upwards direction
             * I use this instead directly setting the cars transform.up because otherwise the 
             * y axis that i use for controlling the car's turning gets tossed out
             */
            Quaternion targetRotation = Quaternion.LookRotation(projForward, upDir);

            //let's the rigidbody control it's own rotation towards the targetRotation with the max degrees being
            //360 so a full rotation can be made. Time.deltaTime smooths this out
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation,targetRotation,360 * Time.deltaTime));
        
    }
    void MovePlayer()
    {
        //if the car is accelerating set the moveDir to the forward direction and add force
        //multiply by 10 to simplify editor values
        if (isAccelerating)
        {
            moveDir = transform.forward;

            rb.AddForce(moveDir * moveSpeed * 10f, ForceMode.Force);   
        }
        //same thing as accelerating but the other way
        if (isBreaking)
        {
            moveDir = -transform.forward;

            rb.AddForce(moveDir * moveSpeed * 10f, ForceMode.Force);
        }

        //This bundle of fun handles the rotation of the car and it's ability to coast
        if (rb.linearVelocity.magnitude >= 0.1f)
        {
            //this section down to the if statement sets the values so they change when the car is drifting
            float driftingAlignmentSpeed = alignmentSpeed;
            float driftingTurnSpeed = turnSpeed;
            /*  if (isDrifting)
              {
                  driftingTurnSpeed *= driftTurnMult;
                  driftingAlignmentSpeed *= driftAlignMult;
              }*/

            //first thing this code does is get the local velocity of the car's rigid body
            Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
            float sidewaysSpeed = localVelocity.x;

            //anti slide force adds a force to the opposite side of the cars movement direction based on its
            //current sideways speed ,"alignment speed" which could be thought of as it's grip, and it's mass
            //this allow the car to "drift" and "coast" depending on player input
            Vector3 antiSlideForce = -transform.right * sidewaysSpeed * driftingAlignmentSpeed * rb.mass;

            //apply antiSlideForce as a constant force like friction
            rb.AddForce(antiSlideForce, ForceMode.Force);

            //this turns the car itself to face the direction the player wants it to with the
            //horizontal inputs
            float turnAngle = horizontal * driftingTurnSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0f, turnAngle, 0f);
            rb.MoveRotation(rb.rotation * rotation);
        }
        if (jump && canJump && CDThree)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
    /*I use MyInput for a few reasons
    * 1. It allows me to quickly edit and keys were they needed to be changed
    * 2. It allows me to slap all key pressed into Update without taking up and ungodly amout of room (assume we end up with a lot of control inputs)
    * 3. I can update all values easily in update which is called every from so no key presses are missed.
    */
    void MyInput()
    {
        isAccelerating = Input.GetKey(KeyCode.Comma);
        isBreaking = Input.GetKey(KeyCode.Period);
        horizontal = Input.GetAxisRaw("Horizontal");
        isDrifting = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKeyDown(KeyCode.Space);
    }

    //Caps speed of rigidbody
    private void SpeedControl()
    {
        //get our velocity only on the X and Z axis hence flatVel (get it cause its flat haha im so smart)
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //if the magnitude of the car's velocity ignoring y is greater than our maxMoveSpeed
        if (flatVel.magnitude > maxMoveSpeed)
        {
            //ok so normalizing vectors is kinda dificult to explain in short comments but imma do my best

            /*
             * When we create a vector it stores three values (x,y,z) and those values can be anything withing the floating point limit (i think)
             * and as such the magnitude of the vector can be anything aswell however we like to multiply by 1s when we're creating a speed value
             * so the magnitude of the vector is equal to our speed
             * 
             * for example a vector(1,1,0) has a magnitude of sqrt(2) which is basically 1.5 (1.4 but i like 5s so bleh!) so if we multiply the vector
             * by our speed (for this example speed will be 10) the magnitude/speed is = 15 and thats no good!
             * 
             * so if we normalize the vector we set the magnitude to 1 allowing our speed value to seemlessly be applied as we expect it to be applied
             */
            Vector3 limVel = flatVel.normalized * maxMoveSpeed;

            //set linearVel = to our new flatvel x and z while retaining it's y value
            rb.linearVelocity = new Vector3(limVel.x, rb.linearVelocity.y, limVel.z);
        }
    }
    void checkIfNeedToMoveY()
    {
        if (Vector3.Distance(Vector3.up, transform.up) > 0.15f || !grounded)
        {
            Debug.Log("The car needs to be able to move along Y");
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            Debug.Log("The car needs to be locked on Y");
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; ;
        }
    }
    void setSpeed()
    {
        bool onRoad = Physics.Raycast(transform.position, Vector3.down, carHeight * .5f + .2f, Road);
        bool onGround = Physics.Raycast(transform.position, Vector3.down, carHeight * .5f + .2f, Ground);

        if (!CDOne)
        {
            if (onRoad)
            {
                Debug.Log("onRoad");
                acceleration = moveSpeed;
                maxMoveSpeed = 80;
            } else if (onGround)
            {
                Debug.Log("onGround");
                acceleration = moveSpeed / 2;
                maxMoveSpeed = 30;
            }
        }
    }
}
