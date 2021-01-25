using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

// Only need to care put hand to the guard instead of which hand goes to Grip or Guard
public class GuardHold : HandHold
{
    protected override void Grab(XRBaseInteractor interactor)
    {
        //Debug.Log("Grabbing pump");
        base.Grab(interactor);
        weapon.SetGuardHand(interactor);
    }

    protected override void Drop(XRBaseInteractor interactor)
    {
        base.Drop(interactor);
        weapon.ClearGuardHand(interactor);
    }

    private void OnTriggerEnter(Collider collider) {
        //Debug.Log("Hit detected" + collider.gameObject.name);
        if (collider.gameObject.name == "pumpBack") 
        {
            Debug.Log("Pump is cocked back");
            GameObject.Find("Weapon2").GetComponent<Weapon>().hasCockedback = true;
        } else if (collider.gameObject.name == "pumpFront")
        {
            Debug.Log("Pump is cocked forward and ready to fire");
            GameObject.Find("Weapon2").GetComponent<Weapon>().hasCockedForward = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //Debug.Log("Exit");
    }

}
