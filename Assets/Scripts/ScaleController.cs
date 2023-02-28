using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleController : MonoBehaviour
{
    public float scaleSpeed = 1f;
    private XRGrabInteractable grabInteractable;
    private Vector3 initialScale;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            //N�cessite InputDevice, que je n'ai toujours pas pu faire marcher
            //float distance = Vector2.Distance(grabInteractable.selectingInteractor.GetComponent<XRController>().inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position) ? position : Vector2.zero, Vector2.zero);

            //Solution de repli : utiliser Input.GetAxis("Horizontal"), r�cup�rer la valeur et en faire la base pour faire grossir ou r�tr�cir l'objet tenu en main
            //Pour ne pas trop modifier le code, on appellera distance la valeur ainsi calcul�e

            float distance = Input.GetAxis("Horizontal");
            float scaleFactor = 10.0f + distance * scaleSpeed;
            transform.localScale = initialScale * scaleFactor;
        }
        else
        {
            transform.localScale = initialScale;
        }
    }
}

