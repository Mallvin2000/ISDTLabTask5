using UnityEngine.XR.Interaction.Toolkit;

// Only need to care put hand to the guard instead of which hand goes to Grip or Guard
public class GuardHold : HandHold
{
    protected override void Grab(XRBaseInteractor interactor)
    {
        base.Grab(interactor);
        weapon.SetGuardHand(interactor);
    }

    protected override void Drop(XRBaseInteractor interactor)
    {
        base.Drop(interactor);
        weapon.ClearGuardHand(interactor);
    }
}
