using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class WheelManager : MonoBehaviour
{
    /*
    [SerializeField] GameObject target;
    [SerializeField] float revolutionSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Axis");
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.forward, revolutionSpeed * Time.deltaTime);
    }
    */

    //Version chatGPT

    private XRGrabInteractable grabInteractable;
    private Quaternion originalRotation;

    [SerializeField] TextMeshProUGUI affichage;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originalRotation = transform.rotation;

        InputDevice targetDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
        }

        affichage.SetText("Target Device : " + targetDevice.isValid);
    }

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            float handRotation = grabInteractable.selectingInteractor.transform.localEulerAngles.y;
            transform.rotation = originalRotation * Quaternion.Euler(0f, handRotation, 0f);
        }
        else
        {
            transform.rotation = originalRotation;
        }
    }
}
