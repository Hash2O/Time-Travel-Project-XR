using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionCharacterControllerManager : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float ySpeed;
    float originalStepOffset;

    bool isJumping;
    bool isGrounded;

    CharacterController characterController;
    Animator characterAnimator;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float jumpSpeed;

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float jumpHorizontalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();

        //To fix the glitch with jump when not directly on the ground
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Emulate gravity for jumping
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            characterAnimator.SetBool("isGrounded", true);
            isGrounded = true;
            characterAnimator.SetBool("isJumping", false);
            isJumping = false;
            characterAnimator.SetBool("isFalling", false);

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                characterAnimator.SetBool("isJumping", true);
                isJumping = true;
                print("Jump ! " + jumpSpeed);
            }
        }
        else
        {
            characterController.stepOffset = 0;
            characterAnimator.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                characterAnimator.SetBool("isFalling", true);
            }
        }

        MovingCharacter();

    }

    void MovingCharacter()
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 5;
            print("start walking : " + inputMagnitude);
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMagnitude *= 1.5f;
            print("start sprinting : " + inputMagnitude);
        }

        characterAnimator.SetFloat("Input Magnitude", inputMagnitude);

        //Rotation around the y axis of the camera to set direction of movement
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;

        characterAnimator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection.Normalize();  //Get only 0 to 1 values

        if (movementDirection != Vector3.zero)
        {
            characterAnimator.SetBool("isRunning", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            characterAnimator.SetBool("isRunning", false);
        }

        if (isGrounded == false)
        {
            Vector3 velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = characterAnimator.deltaPosition;

            velocity.y = ySpeed * Time.deltaTime;

            characterController.Move(velocity);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    

}
