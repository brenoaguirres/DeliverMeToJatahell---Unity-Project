using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable, IHealable
{
    [SerializeField] public Joystick joystick;
    [SerializeField] private JoystickCamera joystickCamera;
    [SerializeField] private Touchscreen touchscreen;
    [SerializeField] private float cameraSpeed;
    private PlayerStatus status;
    Vector3 direction;
    Vector3 newDirection;
    public LayerMask floorMask;
    public GUIController GUI;
    public AudioClip DmgSound;
    public CameraSwitcher cameraswitcher;
    public Transform cam;
    private string camKeycode = "CameraChange";
    private string intKeycode = "Interaction";
    private string inventoryKeycode = "InventoryToggle";
    public bool canInteract = false;
    public bool hasInteracted = false;

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

    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        mainCam = Camera.main;
        stateManager = GetComponentInChildren<StateManager>();
    }

    void Update()
    {
        CharacterAnimator characterAnimator = GetComponent<CharacterAnimator>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        status = GetComponent<PlayerStatus>();

        Move();

        Vector3 movement = CalculateMovement();

        playerMovement.Movement(movement, status.speed);
        playerMovement.FaceMovementDirection();

        if (movement.magnitude > 0.1)
        {
            characterAnimator.OutIdle();
            characterAnimator.Run(true);
        }
        else
        {
            characterAnimator.Run(false);
        }

        //SearchForInputs();
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

    private Vector3 CalculateMovement()
    {
        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * verticalSpeed + right * horizontalSpeed;
    }


    private void Move()
    {
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
    }

    public void ReceiveDamage(int damage)
    {
        status = GetComponent<PlayerStatus>();

        status.life -= damage;

        AudioController.instance.PlayOneShot(DmgSound);
        GUI.PlayerLifebarUpdate();

        if (status.life <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        status = GetComponent<PlayerStatus>();

        status.isAlive = false;
    }

    public void Heal(int healQuantity)
    {
        status = GetComponent<PlayerStatus>();

        status.life += healQuantity;
        status.life = Mathf.Clamp(status.life, 0, status.maxLife);
        GUI.PlayerLifebarUpdate();
    }

    public void SearchForInputs()
    {
        Interact();
        ToggleInventory();
        hasInteracted = false;
    }

    public void AddAmmo(int ammoValue)
    {
        status = GetComponent<PlayerStatus>();

        status.reloadAmmo += ammoValue;
        GUI.UpdateAmmoCount();
    }

    public void Interact()
    {
        hasInteracted = true;
    }

    public void ToggleInventory()
    {
        if (Input.GetButtonDown(inventoryKeycode))
        {
            GUI.ToggleInventory();
        }
    }

    public void CallBloodVFX(Vector3 position, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

    public void DeactivateTargetLock()
    {
        while (targetEnemy != null || targetLocked == true)
        {
            targetEnemy = null;
            targetLocked = false;
        }
    }
}
