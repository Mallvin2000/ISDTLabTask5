using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

// Only need to care put hand to the guard instead of which hand goes to Grip or Guard
public class GuardHold : HandHold
{
    public AudioSource reloadSound;

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

    public void movePump()
    {
        StartCoroutine(ExampleCoroutine());
        /*bug.Log("Moving pump");
        transform.Translate(0f, 0f, -0.056f);
        transform.Translate(0f, 0f, 0.056f);*/
        /*loat speed = 0.3f;
         Vector3 newPosition = new Vector3(0f, 0f, -5f);
         transform.Translate(newPosition * Time.deltaTime * speed);*/

    }

    IEnumerator ExampleCoroutine()
    {
        float waitTime = 0.4f;
        reloadSound.Play(0);
        transform.Translate(0f, 0f, -0.076f);
        yield return new WaitForSeconds(waitTime);
        transform.Translate(0f, 0f, 0.076f);
    }

}
