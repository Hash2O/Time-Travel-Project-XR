using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MultipleAttachPoints : MonoBehaviour
{
    [SerializeField] private Transform[] attachPoints;
    [Space(10)]
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [Space(10)]
    [Tooltip("The own XR Grab Interactable component of the object.")]
    [SerializeField] private XRGrabInteractable xrGrab;
    private Transform actualAttachPoint;
    private Transform handGrabbing;
    [Space(10)]
    [Tooltip("Maintains the rotation of the object when grabbed.")]
    [SerializeField] private bool maintainRotation = true;


    public void grabbed(SelectEnterEventArgs interactor)     // Selects the nearest attach point of the object when grabbed.
    {
        Quaternion objectRotation = transform.rotation;

        float mindistanceFromHand = 100000;
        handGrabbing = interactor.interactorObject.transform;

        for (int i = 0; i < attachPoints.Length; i++)
        {
            float distanceFromHand = Vector3.Distance(attachPoints[i].position, handGrabbing.position);
            if (distanceFromHand < mindistanceFromHand)
            {
                mindistanceFromHand = distanceFromHand;
                actualAttachPoint = attachPoints[i];

                if (maintainRotation)
                {
                    if (handGrabbing.name == rightHand.name)
                    {
                        attachPoints[i].rotation = rightHand.rotation;
                    }
                    else if (handGrabbing.name == leftHand.name)
                    {
                        attachPoints[i].rotation = leftHand.rotation;
                    }
                }
            }
        }
        xrGrab.attachTransform = actualAttachPoint;
    }
}
