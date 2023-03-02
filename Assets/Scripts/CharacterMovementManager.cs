using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    CharacterController characterController;

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        MovingCharacter();
;    }

    void MovingCharacter()
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();  //Get only 0 to 1 values

        //Without CharacterController
        //transform.Translate(movementDirection * magnitude * Time.deltaTime, Space.World);

        //With CharacterController
        //NB : no need to use Time.deltaTime for it is already included in SimpleMove()
        characterController.SimpleMove(movementDirection * magnitude);

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
