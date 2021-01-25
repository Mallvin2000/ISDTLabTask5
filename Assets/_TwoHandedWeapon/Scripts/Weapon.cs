using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// If grab with GripHold, call OnSelectEnter which start the pickup interaction.
// Rotate and Position the weapon accordingly on the Hand

public class Weapon : XRGrabInteractable
{
    public float breakDistance = 0.25f;
    public int recoilAmount = 25;

    private GripHold gripHold = null;
    private GuardHold guardHold = null;

    private XRBaseInteractor gripHand = null;
    private XRBaseInteractor guardHand = null;

    // put new to remove warning from Visual Studio (optional)
    private new Rigidbody rigidbody = null;
    private Barrel barrel = null;

    private readonly  Vector3 gripRotation = new Vector3(45,0,0);

    public bool hasCockedback = false;
    public bool hasCockedForward = false;
    
    protected override void Awake()
    {
        base.Awake();
        SetupHolds();
        SetupExtras();
        
        onSelectEntered.AddListener(SetInitialRotation);
    }

    private void SetupHolds()
    {
        gripHold = GetComponentInChildren<GripHold>();
        gripHold.Setup(this);

        guardHold = GetComponentInChildren<GuardHold>();
        guardHold.Setup(this);

    }

    private void SetupExtras()
    {
        rigidbody = GetComponent<Rigidbody>();
        barrel = GetComponentInChildren<Barrel>();
        barrel.Setup(this);

    }

    private void OnDestroy()
    {

    }

    private void SetInitialRotation(XRBaseInteractor interactor)
    {
        Quaternion newRotation = Quaternion.Euler(gripRotation);
        // apply this to attach point transform and not the weapon
        interactor.attachTransform.localRotation = newRotation;
    }

  public void SetGripHand(XRBaseInteractor interactor)
    {
        gripHand = interactor;
        ManualSelect(interactor);
    }

    private void ManualSelect(XRBaseInteractor interactor)
    {
        OnSelectEntering(interactor);
        OnSelectEntered(interactor);
    }

    public void ClearGripHand(XRBaseInteractor interactor)
    {
        gripHand = null;
        ManualDeselect(interactor);
    }

    private void ManualDeselect(XRBaseInteractor interactor)
    {
        OnSelectExiting(interactor);
        OnSelectExited(interactor);
    }

    public void SetGuardHand(XRBaseInteractor interactor)
    {
        guardHand = interactor;
    }

    public void ClearGuardHand(XRBaseInteractor interactor)
    {
        guardHand = null;
    }

    public override void ProcessInteractable(
    XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (gripHand && guardHand)
            SetGripRotation();
        
        CheckDistance(gripHand, gripHold);
        CheckDistance(guardHand, guardHold);

    }

    private void SetGripRotation()
    {
        // Find the lookRotation from GripHand to GuardHand
        Vector3 target = guardHand.transform.position - gripHold.transform.position; //what if using gripHand?
        Quaternion lookRotation = Quaternion.LookRotation(target);

        // Find the gripRotation from GripHand to GuardHan
        Vector3 gripRotation = Vector3.zero; //initialize
        gripRotation.z = gripHand.transform.eulerAngles.z; //eulerAngles is the human readable local angle
        
        // Whichever Hand on the grip, when roll, we roll the weapon as well
        lookRotation *= Quaternion.Euler(gripRotation);
        gripHand.attachTransform.rotation = lookRotation;

    }

    private void CheckDistance(XRBaseInteractor interactor, HandHold handHold)
    {
        if (interactor)
        {
            float distanceSqr = GetDistanceSqrToInteractor(interactor);
            
            if (distanceSqr > breakDistance)
                handHold.BreakHold(interactor);
        }

    }

    public void PullTrigger()
    {
        Debug.Log("trigger pulled");
        if (hasCockedback && hasCockedForward)
        {
            Debug.Log("Clear to fire");
          barrel.firecartridge();
          hasCockedback = false;
          hasCockedForward = false;
        } else
        {
            Debug.Log("cant fire yet");
        }
        //barrel.StartFiring();
    }

    public void ReleaseTrigger()
    {
        barrel.StopFiring();
    }

    public void ApplyRecoil()
    {
        rigidbody.AddRelativeForce(Vector3.back * recoilAmount, ForceMode.Impulse);
    }
}
