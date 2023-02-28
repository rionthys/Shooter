using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManagerState : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask GroundLayer;

    [Header("Movement speeds")]
    [HideInInspector] public float walkSpeed = 3f;
    [HideInInspector] public float walkBackSpeed = 2f;
    [HideInInspector] public float runSpeed = 7f;
    [HideInInspector] public float runBackSpeed = 5f;
    [HideInInspector] public float crouchSpeed = 2f;
    [HideInInspector] public float crouchBackSpeed = 1f;
    [HideInInspector] public float airSpeed = 1.5f;
    public float currentMoveSpeed;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 10;
    [HideInInspector] public bool jumped;

    private Vector3 velocity;
    [HideInInspector] public float vInput;
    [HideInInspector] public float hInput;

    private Vector3 spherePos;
    public Vector3 direction;

    [Header("States")]
    public MovementBaseState currentState;
    public MovementBaseState previousState;
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    public CrouchState Crouch = new CrouchState();
    public JumpState Jump = new JumpState();

    [Header("Animation")]
    [HideInInspector] public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    void Update()
    {
        Gravity();
        MoveLogic();
        Falling();

        anim.SetFloat("hInput", hInput);
        anim.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void MoveLogic()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        Vector3 airDir = Vector3.zero;
        if(!isGrounded) airDir = transform.forward *  vInput + hInput * transform.right;
        else direction =  transform.forward *  vInput + hInput * transform.right;

        controller.Move((direction.normalized * currentMoveSpeed + airDir.normalized * airSpeed) * Time.deltaTime);
    }

    public bool isGrounded
    {
        get
        {
            spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

            return Physics.CheckSphere(spherePos, controller.radius - 0.05f, GroundLayer);
        }
    }

    private void Gravity()
    {
        velocity.y += Time.deltaTime * gravity;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
    
    private void Falling() => anim.SetBool("Falling", !isGrounded);

    public void JumpForce() => velocity.y += jumpForce;

    public void Jumped() => jumped = true;
}