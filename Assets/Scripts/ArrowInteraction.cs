using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//URL : https://blog.immersive-insiders.com/bow-and-arrow-in-vr-part3/

[RequireComponent(typeof(XRGrabInteractable))]
public class ArrowInteraction : MonoBehaviour
{
    private XRGrabInteractable xRGrabInteractable = null;
    private bool inAir = false;
    private Vector3 lastPosition = Vector3.one;
    private Rigidbody arrowRigidBody = null;

    [SerializeField] private float speed;
    [SerializeField] private Transform tipPosition;

    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        arrowRigidBody = GetComponent<Rigidbody>();
        inAir = false;
        lastPosition = Vector3.zero;
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        arrowRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            CheckCollision();
            lastPosition = tipPosition.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(lastPosition, tipPosition.position, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.TryGetComponent(out Rigidbody body))
            {
                arrowRigidBody.interpolation = RigidbodyInterpolation.None;
                transform.parent = hitInfo.transform;
                body.AddForce(arrowRigidBody.velocity, ForceMode.Impulse);
            }
            StopArrow();
        }
    }

    private void StopArrow()
    {
        Debug.Log("Arrow : StopArrow");
        inAir = false;
        SetPhysics(false);
        audioSource.Play();
    }

    private void SetPhysics(bool usePhysics)
    {
        Debug.Log("Arrow : SetPhysics");
        arrowRigidBody.useGravity = usePhysics;
        arrowRigidBody.isKinematic = !usePhysics;
    }

    public void ReleaseArrow(float value)
    {
        Debug.Log("Arrow : ReleaseArrow");
        inAir = true;
        SetPhysics(true);
        MaskAndFire(value);
        StartCoroutine(RotateWithVelocity());
        lastPosition = tipPosition.position;

    }

    private void MaskAndFire(float power)
    {
        Debug.Log("Arrow : MaskAndFire");
        xRGrabInteractable.colliders[0].enabled = false;
        xRGrabInteractable.interactionLayers = 1 << LayerMask.NameToLayer("Ignore");
        Vector3 force = transform.forward * power * speed;
        arrowRigidBody.AddForce(force, ForceMode.Impulse);
    }

    private IEnumerator RotateWithVelocity()
    {
        Debug.Log("Arrow : RotateWithVelocity");
        yield return new WaitForFixedUpdate();
        while (inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(arrowRigidBody.velocity);
            transform.rotation = newRotation;
            yield return null;
        }
    }
}
