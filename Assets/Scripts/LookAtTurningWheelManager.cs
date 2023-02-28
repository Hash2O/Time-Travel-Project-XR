using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class LookAtTurningWheelManager : MonoBehaviour
{

    [SerializeField] GameObject handController;
    [SerializeField] GameObject wheel;
    [SerializeField] GameObject axis;
    [SerializeField] TextMeshProUGUI affichage;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        wheel = GameObject.Find("Wheel");

        InputDevice targetDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
        }

        affichage.SetText("Target Device : " + targetDevice.isValid);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Solution qui nécessite de pouvoir enable les touch du casque Oculus Quest 2
        /*
        if (targetDevice.isValid)
        {
           
            bool primaryTouch;
            bool secondaryTouch;
            if (targetDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out primaryTouch) &&
                targetDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out secondaryTouch))
            {
                // Print the touch values to the console
                Debug.Log("Primary Touch: " + primaryTouch);
                Debug.Log("Secondary Touch: " + secondaryTouch);

                affichage.SetText("Device detected");
            }
            else
            {
                // Print an error message to the console
                Debug.Log("Failed to get touch values!");
            }
        }
                   
            if (Vector3.Distance(transform.position, handController.transform.position) < 1)
            {
                //Solution avec LookAt
                //transform.LookAt(handController.transform);

                affichage.SetText("Contact established");

                        // Get the position of the controller
                        Vector3 controllerPosition;
                    if (targetDevice.TryGetFeatureValue(CommonUsages.devicePosition, out controllerPosition))
                    {
                        // Rotate the wheel based on the position of the controller
                        float rotation = controllerPosition.x * rotationSpeed;
                        transform.Rotate(0, rotation, 0);
                    }
                }
            }
            */

        //On récupère l'input du joystick du touch pour faire tourner la roue (Bof)
        /*
        // Get the controller input
        float rotation = Input.GetAxis("Horizontal");
        affichage.SetText("Input : " + rotation); 

        // Rotate the wheel
        transform.Rotate(0, - rotation * rotationSpeed, 0);
        */

        rotationSpeed = Vector3.Distance(transform.position, handController.transform.position);


    }//fin Update

    //Ici, on opte pour la proximité et le lookAt de l'axis pour tenter de faire tourner la roue
    
    private void OnTriggerEnter(Collider other)
    {
        axis.transform.LookAt(handController.transform);
        print("Contact made");
        affichage.SetText("OnTriggerEnter : Contact made");

    }

    private void OnTriggerStay(Collider other)
    {
        affichage.SetText("OnTriggerStay : Contact established");
        /*
        float rotation = handController.transform.position.y * rotationSpeed;
        transform.Rotate(0, rotation, 0);
        */
        axis.transform.LookAt(handController.transform);
        //transform.Rotate(0, axis.transform.rotation.y, 0);
        //transform.LookAt(handController.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        print("Contact lost");
        affichage.SetText("OnTriggerExit : Contact lost");
    }
    
}//fin classe
