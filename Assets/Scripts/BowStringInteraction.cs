using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


//URL : https://blog.immersive-insiders.com/bow-and-arrow-in-vr-part1/

public class BowStringInteraction : XRBaseInteractable
{
    [SerializeField] public Transform stringStartPoint;
    [SerializeField] public Transform stringEndPoint;

    private XRBaseInteractor stringInteractor = null;
    private Vector3 pullPosition;
    private Vector3 pullDirection;
    private Vector3 targetDirection;

    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> clips;

    public float PullAmount { get; private set; } = 0.0f;
    public Vector3 StringStartPoint { get => stringStartPoint.localPosition; }
    public Vector3 StringEndPoint { get => stringEndPoint.localPosition; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        stringInteractor = args.interactor;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        stringInteractor = null;
        PullAmount = 0f;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && isSelected)
        {
            pullPosition = stringInteractor.transform.position;
            PullAmount = CalculatePull(pullPosition);
            //Debug.Log("<<<<< Pull amount is "+ PullAmount+" >>>>>");
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Debug.Log("CalculatePull");
        //audioSource.PlayOneShot(clips[0]);
        pullDirection = pullPosition - stringStartPoint.position;
        targetDirection = stringEndPoint.position - stringStartPoint.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();

        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }
}
