using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float playerJumpForce;
    public ForceMode appliedForceMode;
    public float currentSpeed;
    private float xAxis;
    private float zAxis;
    public float maxspeed;
    private Rigidbody rb;
    float playeroriginalScaleY;
    public float walkspeedadder;
    public float runspeedadder;
    public GameObject playerform;
    float playercrouchScaleY;
    public bool grounded;
    void Start()
    {
        playeroriginalScaleY = playerform.transform.localScale.y;
        playercrouchScaleY = playeroriginalScaleY * (25f / 100f);
        rb = GetComponent<Rigidbody>();
        Screen.lockCursor = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Screen.lockCursor = false;
        }
        if (grounded)
        {
            if (!Input.GetKey(KeyCode.UpArrow))
            {
                if (currentSpeed >= (runspeedadder + walkspeedadder) * 2.0f)
                {
                    currentSpeed -= (runspeedadder + walkspeedadder)*2.0f;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (grounded)
                    {
                        if (currentSpeed <= maxspeed)
                        {
                            currentSpeed += runspeedadder;
                        }
                    }
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                if (currentSpeed <= maxspeed)
                {
                    currentSpeed += walkspeedadder;
                }
            }
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
        }
        if (Input.GetKey(KeyCode.LeftControl) == false)
        {
            StopCrouch();
        }
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + Time.deltaTime * currentSpeed * transform.TransformDirection(xAxis, 0f, zAxis));

        Vector3 down = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, down, 1))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }


        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ApplyJump();
            }
        }
    }
    void PlayerJump(float jumpForce, ForceMode forceMode)
    {
        rb.AddForce(jumpForce * rb.mass * Time.deltaTime * Vector3.up, forceMode);
    }

    void ApplyJump()
    {

        PlayerJump(playerJumpForce, appliedForceMode);

    }
    void Crouch()
    {
        if (playerform.transform.localScale.y >= playercrouchScaleY)
        {
            playerform.transform.localScale -= new Vector3(playerform.transform.localScale.x, 0.14f, playerform.transform.localScale.z);
        }
    }
    void StopCrouch()
    {
        if (playerform.transform.localScale.y <= playeroriginalScaleY)
        {
            playerform.transform.localScale += new Vector3(playerform.transform.localScale.x, 0.14f, playerform.transform.localScale.z);
        }
    }
}