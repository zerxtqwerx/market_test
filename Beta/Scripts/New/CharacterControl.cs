using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class CharacterControl : MonoBehaviour
{
    public static CharacterControl Instance;
    [SerializeField] private Joystick joystick;
    //[SerializeField] public Button jumpButton;

    CharacterController characterController;
    public Transform orientation;

    private Vector3 playerVelocity;

    private bool groundedPlayer;
    public float playerSpeed;
    public float jumpHeight;

    private float currentPlayerSpeed;
    private float playerRunSpeed;

    //private float gravityValue = -9.81f;

    public float jumpForce;
    public float jumpCooldown;

    /*public UnityEvent joystickTouchFieldPick;
    public UnityEvent joystickTouchFieldUp;
    public UnityEvent jumpButtonPressed;*/

    bool readyToJump;
    //bool isJumpButtonPressed;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    float directionY;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        readyToJump = true;
        playerRunSpeed = playerSpeed * 2;
    }

    void Update()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (groundedPlayer)
        {
            readyToJump = true;
        }

        MyInput();

        if (horizontalInput != 0 && verticalInput != 0)
        {
            currentPlayerSpeed = playerSpeed;
        }

        else if (Math.Sqrt((Math.Pow(horizontalInput, 2) + Math.Pow(verticalInput, 2))) > 0.9)
        {
            currentPlayerSpeed = playerRunSpeed;
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(moveDirection != Vector3.zero)
        {
            characterController.Move(moveDirection.normalized * currentPlayerSpeed);
        }
    }

    public void Jump()
    {
        if(readyToJump )
        {
            readyToJump = false;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -10f * Physics.gravity.y);

            //Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    /*private void ResetJump()
    {
        readyToJump = true;
    }*/

    /*
    private Vector2 LookAxis;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool trig = true;*/
    /*private void Awake()
    {
        Instance = this;
    }*/
    /*public void SetLookAxis(Vector2 n)
    {
        LookAxis = n;
    }*/

    /*void Update()
    {
        *//*if (trig)
        {
            rotationX = _playerCamera.transform.localEulerAngles.x;
            trig = false;
        }*//*
        _horizontalInput = _joystick.Horizontal;
        _verticalInput = _joystick.Vertical;

        moveDirection = transform.TransformDirection(new Vector3(_horizontalInput * _speed, 0, _verticalInput * _speed));
        characterController.Move(moveDirection * Time.deltaTime);
        //Гравитация
        characterController.Move(Physics.gravity * Time.deltaTime);

        rotationX += -LookAxis.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        _playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, LookAxis.x * lookSpeed, 0);
    }

    public void DisableControll()
    {
        enabled = false;
        touchField.SetActive(false);
        _joystick.enabled = false;
    }

    public void EnableControll()
    {
        enabled = true;
        touchField.SetActive(true);
        _joystick.enabled = true;
    }*/
}

