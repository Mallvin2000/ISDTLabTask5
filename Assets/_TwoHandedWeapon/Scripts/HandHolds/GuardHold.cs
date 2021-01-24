using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

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

    protected void onTriggerEnter(Collider collider) {
        if (collider.tag == "pumpBack") 
        {
            Debug.Log("Pump is cocked back");
            GameObject.Find("Weapon2").GetComponent<Weapon>().hasCockedback = true;
        } else if (collider.tag == "pumpForward") {
            Debug.Log("Pump is cocked forward and ready to fire");
            GameObject.Find("Weapon2").GetComponent<Weapon>().hasCockedForward = true;
        }
    }

}
