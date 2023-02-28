using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(BowStringInteraction))]
public class SocketInteraction : XRSocketInteractor
{

    private XRBaseInteractor handHoldingArrow = null;
    public XRBaseInteractable currentArrow = null;
    private BowStringInteraction stringInteraction = null;
    private BowInteraction bowInteraction = null;
    private ArrowInteraction currentArrowInteraction = null;

    protected override void Awake()
    {
        base.Awake();
        stringInteraction = GetComponent<BowStringInteraction>();
        bowInteraction = GetComponentInParent<BowInteraction>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stringInteraction.selectExited.AddListener(ReleaseArrow);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        stringInteraction.selectExited.RemoveListener(ReleaseArrow);
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        handHoldingArrow = args.interactable.selectingInteractor;
        if (args.interactable.tag == "Arrow" && bowInteraction.BowHeld)
        {
            interactionManager.SelectExit(handHoldingArrow, args.interactable);
            interactionManager.SelectEnter(handHoldingArrow, stringInteraction);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        StoreArrow(args.interactable);
    }

    private void StoreArrow(XRBaseInteractable interactable)
    {
        if (interactable.tag == "Arrow")
        {
            Debug.Log("Socket : StoreArrow");
            currentArrow = interactable;
            currentArrowInteraction = currentArrow.gameObject.GetComponent<ArrowInteraction>();
        }
    }

    private void ReleaseArrow(SelectExitEventArgs args)
    {
        if (currentArrow && bowInteraction.BowHeld)
        {
            Debug.Log("Socket : ReleaseArrow");
            ForceDetach();
            ReleaseArrowFromSocket();
            ClearVariables();
        }
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }

    private void ForceDetach()
    {
        Debug.Log("Socket : Force Detach");
        interactionManager.SelectExit(this, currentArrow);
    }

    private void ReleaseArrowFromSocket()
    {
        Debug.Log("Socket : Release Arrow From Socket");
        currentArrowInteraction.ReleaseArrow(stringInteraction.PullAmount);
    }

    private void ClearVariables()
    {
        Debug.Log("Socket : Clear Variables");
        currentArrow = null;
        currentArrowInteraction = null;
        handHoldingArrow = null;
    }
}
