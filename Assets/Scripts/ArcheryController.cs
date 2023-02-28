using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Version chatGPT 

//Ce script fonctionne en attachant les XRGrabInteractable au modèle de l'arc et à la flèche.
//Lorsque l'utilisateur saisit l'arc, il peut ensuite saisir la flèche avec l'autre main et la
//fixer à l'arc. L'utilisateur peut ensuite tirer la flèche en tirant sur la corde de l'arc.

//Le script utilise le point d'attache de la corde de l'arc et le point d'attache de la flèche
//pour calculer la distance de traction de l'arc. La longueur de l'arc est ajustée en conséquence.
//La direction de la flèche est également ajustée.

public class ArcheryController : MonoBehaviour
{
    public Transform bowTransform;
    public Transform arrowTransform;
    public Transform bowStringAttachPoint;
    public Transform arrowAttachPoint;

    public float maxBowDrawDistance = 0.5f;
    public float arrowVelocity = 10f;

    private XRGrabInteractable bowInteractable;
    private XRGrabInteractable arrowInteractable;

    private bool isBowGrabbed;
    private bool isArrowGrabbed;
    private float bowDrawDistance;

    private Vector3 arrowStartPosition;

    void Start()
    {
        bowInteractable = bowTransform.GetComponent<XRGrabInteractable>();
        arrowInteractable = arrowTransform.GetComponent<XRGrabInteractable>();

        isBowGrabbed = false;
        isArrowGrabbed = false;
    }

    void Update()
    {
        if (isBowGrabbed && isArrowGrabbed)
        {
            UpdateBowDraw();
        }
        else
        {
            ResetBowDraw();
        }
    }

    void UpdateBowDraw()
    {
        Vector3 bowStringAttachPosition = bowStringAttachPoint.position;
        Vector3 arrowAttachPosition = arrowAttachPoint.position;

        float distance = Vector3.Distance(bowStringAttachPosition, arrowAttachPosition);
        bowDrawDistance = Mathf.Clamp(distance, 0, maxBowDrawDistance);

        Vector3 bowScale = bowTransform.localScale;
        bowScale.z = bowDrawDistance * 2f;
        bowTransform.localScale = bowScale;

        Vector3 arrowDirection = (arrowAttachPosition - arrowStartPosition).normalized;
        arrowTransform.rotation = Quaternion.LookRotation(arrowDirection, bowStringAttachPoint.up);
    }

    void ResetBowDraw()
    {
        bowDrawDistance = 0;

        Vector3 bowScale = bowTransform.localScale;
        bowScale.z = 0;
        bowTransform.localScale = bowScale;

        arrowTransform.position = arrowStartPosition;
        arrowTransform.rotation = Quaternion.identity;
    }

    public void FireArrow()
    {
        if (isBowGrabbed && isArrowGrabbed)
        {
            Vector3 arrowDirection = (arrowAttachPoint.position - arrowStartPosition).normalized;
            Vector3 arrowVelocityVector = arrowDirection * arrowVelocity;
            Rigidbody arrowRigidbody = arrowTransform.GetComponent<Rigidbody>();
            arrowRigidbody.velocity = arrowVelocityVector;
        }
    }

    public void GrabBow()
    {
        isBowGrabbed = true;
    }

    public void ReleaseBow()
    {
        isBowGrabbed = false;
    }

    public void GrabArrow()
    {
        isArrowGrabbed = true;
        arrowStartPosition = arrowTransform.position;
    }

    public void ReleaseArrow()
    {
        isArrowGrabbed = false;
    }
}

