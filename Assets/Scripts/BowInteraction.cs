using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


//URL : https://blog.immersive-insiders.com/bow-and-arrow-in-vr-part2/ (update)

public class BowInteraction : XRGrabInteractable
{
    private LineRenderer bowString;
    private BowStringInteraction stringInteraction;

    [SerializeField] private Transform socketTransform;
    public bool BowHeld { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        stringInteraction = GetComponentInChildren<BowStringInteraction>();
        bowString = GetComponentInChildren<LineRenderer>();
        movementType = MovementType.Instantaneous;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        BowHeld = true;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        BowHeld = false;
        base.OnSelectExited(args);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            UpdateBow(stringInteraction.PullAmount);
        }
    }

    private void UpdateBow(float pullAmount)
    {
        Debug.Log("UpdateBow");
        float xPositionStart = stringInteraction.stringStartPoint.localPosition.x;
        float xPositionEnd = stringInteraction.stringEndPoint.localPosition.x;

        Vector3 linePosition = Vector3.right * Mathf.Lerp(xPositionStart, xPositionEnd, pullAmount);

        bowString.SetPosition(1, linePosition);
        socketTransform.localPosition = linePosition;

    }

}