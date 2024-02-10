using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] public Joystick joystick;
    [SerializeField] private JoystickCamera joystickCamera;
    [SerializeField] private float cameraSpeed;

    private float horizontalSpeed;
    private float verticalSpeed;
    private float speed = 1f;
    [SerializeField] private Transform mainCameraTransform;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    public GameObject CinemachineCameraTarget;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    // Aim Assist On/Off
    [SerializeField]
    private bool AimAssistance = true;
    // Casts a ray to check if enemy is in sight
    private Camera mainCam;
    // Used to ignore player from raycast to find enemies
    [SerializeField]
    private LayerMask playerLayerMask;
    // Stores enemy targeted
    private Transform targetEnemy;
    // Check if aim assist target is locked/unlocked
    private bool targetLocked = false;
    // Check if currentState is Aiming
    private StateManager stateManager;

    private Rigidbody characterRB;

    [SerializeField]
    public float rotationDamping = 5f;

    [HideInInspector]
    public Vector3 direction;

    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        mainCam = Camera.main;
        stateManager = GetComponentInChildren<StateManager>();
        characterRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        FaceMovementDirection();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        // if there is no target locked then CameraRotation is based on joystick movement, otherwise it will follow the target
        if (!targetLocked)
        {
            // if there is an input and camera position is not fixed
            if (joystickCamera.cameraMovementValue.sqrMagnitude >= _threshold)
            {
                _cinemachineTargetYaw += joystickCamera.cameraMovementValue.x * Time.deltaTime * cameraSpeed;
                _cinemachineTargetPitch += joystickCamera.cameraMovementValue.y * Time.deltaTime * cameraSpeed;
            }
        }
        else
        {
            _cinemachineTargetYaw = targetEnemy.position.x;
            _cinemachineTargetPitch = targetEnemy.position.y;
        }

        // clamp our rotations so our values are limited 360 degrees in case of free joystick
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        if (!targetLocked)
        {
            // Cinemachine will follow this target if no target is in sight
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }
        else
        {
            // Cinemachine will follow this target if there is a target in sight
            Vector3 _camToTarget = targetEnemy.position - mainCam.transform.position;
            //0.5f is an offset to position the cursor in a better place
            _camToTarget.y += targetEnemy.transform.position.y + 0.5f;
            Quaternion _lookRotation = Quaternion.LookRotation(_camToTarget);
            CinemachineCameraTarget.transform.rotation = _lookRotation;
        }

        // Locks target when AimAssist is on and if enemy is spotted and if player is currently aiming
        if (AimAssistance && (stateManager.GetState() is PlayerAimingState))
        {
            Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 1000, Color.blue);
            RaycastHit hit;
            Physics.Raycast(mainCam.transform.position, mainCam.transform.forward * 1000, out hit, playerLayerMask);
            if (hit.transform.tag == "Enemy")
            {
                targetEnemy = hit.transform;
                targetLocked = true;
            }
        }

    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void Move()
    {
        // Check if there is joystick input

        horizontalSpeed = joystick.Horizontal;
        verticalSpeed = joystick.Vertical;

        if (joystick.Horizontal >= .2f)
        {
            horizontalSpeed = speed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            horizontalSpeed = -speed;
        }
        else
        {
            horizontalSpeed = 0f;
        }

        if (joystick.Vertical >= .2f)
        {
            verticalSpeed = speed;
        }
        else if (joystick.Vertical <= -.2f)
        {
            verticalSpeed = -speed;
        }
        else
        {
            verticalSpeed = 0f;
        }

        // Calculates direction to move vector

        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        direction = forward * verticalSpeed + right * horizontalSpeed;

        //Uses rigidbody to move character position

        characterRB.MovePosition(characterRB.position + (direction.normalized * playerSpeed * Time.deltaTime));
    }

    public void FaceMovementDirection()
    {
        var rot = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y, 
                Camera.main.transform.rotation.eulerAngles.z);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            rot,
            Time.deltaTime * rotationDamping
        );
    }
}
