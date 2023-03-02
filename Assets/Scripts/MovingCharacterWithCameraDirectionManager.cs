using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacterWithCameraDirectionManager : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float ySpeed;
    float originalStepOffset;

    CharacterController characterController;
    Animator characterAnimator;

    [SerializeField] 
    float maximumSpeed;

    [SerializeField] 
    float rotationSpeed;

    [SerializeField] 
    float jumpSpeed;

    [SerializeField] 
    Transform cameraTransform;

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

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        MovingCharacter();
    }

    void MovingCharacter()
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            print("start walking");
            inputMagnitude /= 2;
        }

        characterAnimator.SetFloat("Input Magnitude", inputMagnitude);

        float speed = inputMagnitude * maximumSpeed;

        //Rotation around the y axis of the camera to set direction of movement
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;

        

        characterAnimator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection.Normalize();  //Get only 0 to 1 values

        Vector3 velocity = movementDirection * speed;

        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

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
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
