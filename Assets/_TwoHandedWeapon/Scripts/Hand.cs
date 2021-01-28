using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Hand is a Interactor to interact with Interactable objects eg. HandHold

public class Hand : XRDirectInteractor
{
    private SkinnedMeshRenderer meshRenderer = null;
    protected override void Awake()
    {
        base.Awake(); //call the base to initialize
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void SetVisibility(bool value)
    {
       // meshRenderer.enabled = value;
    }
}
