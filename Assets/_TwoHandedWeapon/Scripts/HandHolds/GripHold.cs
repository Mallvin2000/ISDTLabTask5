using UnityEngine.XR.Interaction.Toolkit;

// If grab by Hand Interactor, try to access weapon

public class GripHold : HandHold
{
    protected override void BeginAction(XRBaseInteractor interactor)
    {
        base.BeginAction(interactor);
        weapon.PullTrigger();
    }

    protected override void EndAction(XRBaseInteractor interactor)
    {
        base.BeginAction(interactor);
        weapon.ReleaseTrigger();
    }

    protected override void Grab(XRBaseInteractor interactor)
    {
        //Whichever hands, ask HandHold to hide hand
        base.Grab(interactor);
        weapon.SetGripHand(interactor);
    }

    protected override void Drop(XRBaseInteractor interactor)
    {
        base.Drop(interactor);
        weapon.ClearGripHand(interactor);
    }
}
