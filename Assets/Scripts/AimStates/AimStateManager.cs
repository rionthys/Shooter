using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class AimStateManager : MonoBehaviour
{
    [HideInInspector] public AimBaseState currentState;
    [HideInInspector] public HipFireState Hip = new HipFireState();
    [HideInInspector] public AimState Aim = new AimState();

    [HideInInspector] public Animator anim;

    [SerializeField, Range(0, 10)] 
    private float mouseSense = 1;
    private float xAxis, yAxis;

    [SerializeField] Transform camFollowPos;

    private float xFollowPos;
    private float yFollowPos, ogYpos;

    [SerializeField, Range(0, 1)]
    float crouchCamHeight = 0.6f;

    [SerializeField, Range(0, 20)]
    float shoulderSwapSpeed = 10;
    MovementManagerState moving;
    
    [HideInInspector] public CinemachineVirtualCamera vCam;
    [SerializeField, Range(0, 60)]
    public float aimFov = 40f;

    [HideInInspector] public float hipFov;

    [HideInInspector] public float currentFov;
    [SerializeField, Range(0, 20)]
    public float fovSmoothSpeed = 10f;

    public Transform aimPos;

    [SerializeField, Range(0, 100)]
    float aimSmoothSpeed = 20f;
    
    [SerializeField] LayerMask aimMask;

    public TwoBoneIKConstraint lHandIK;

    void Start()
    {
        moving = GetComponent<MovementManagerState>();
        xFollowPos = camFollowPos.localPosition.x;
        ogYpos = camFollowPos.localPosition.y;
        yFollowPos = ogYpos;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        currentFov = hipFov;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);
        
        MoveCamera();

        Vector2 centerScreen = new Vector2(Screen.width/2, Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(centerScreen);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }

        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void MoveCamera()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt)) 
        {
            xFollowPos *= -1;
        }
        if(moving.currentState == moving.Crouch) 
        {
            yFollowPos = crouchCamHeight;
        }
        else 
        {
            yFollowPos = ogYpos;
        }

        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, camFollowPos.localPosition.z);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);
    }
}
