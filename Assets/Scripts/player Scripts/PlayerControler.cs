using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    [Header("Camera Properties")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXlook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    [Header("Player Properties")]
    public float maxHp;
    public float curHP;
    public float moveSpeed;
    private Vector2 curMoveInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Player components")]
    public Rigidbody rig;


    //unity methods called during game loop

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        cameraLook();
    }

    private void FixedUpdate()
    {
        move();
    }
   
    // player methods called to make player do stuff

    //move method calculates the facing direction of the player and adds force in that facing direction when called should be called in fixed update because if is a physics calculation 
    private void move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;
        rig.velocity = dir;
    }

    // changes the camera container as the mouse is moved on the screen
    void cameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXlook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    //adds force to the players rigidbody in the up direction using the impulse force mode  causing the player to jump up 
    void jump()
    {
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    // this method shoots 4 raycasts down from the players base and checks to se if we have colided with the ground and returns true or false
    bool isGrounded()
    {

        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position +(transform.forward * 0.2f)+(Vector3.up*0.02f),Vector3.down),
            new Ray(transform.position +(-transform.forward * 0.2f)+(Vector3.up*0.02f),Vector3.down),
            new Ray(transform.position +(transform.right * 0.2f)+(Vector3.up*0.02f),Vector3.down),
            new Ray(transform.position +(-transform.right * 0.2f)+(Vector3.up*0.02f),Vector3.down),
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }


    // input action methods

    // method is called when the look action is preformed
    public void onLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // method is called when the move action is preformed
    public void onMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }

    public void onJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(isGrounded())
            {
                jump();
            }
        }
    }

}//end of class
