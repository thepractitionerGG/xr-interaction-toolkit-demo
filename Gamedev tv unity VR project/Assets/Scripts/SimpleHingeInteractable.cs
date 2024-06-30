using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SimpleHingeInteractable : XRSimpleInteractable
{
    private Transform grabHand;
    [SerializeField] private bool isLocked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void UnlockHinge()
    {
        isLocked = false;
    }
    public void LockHinge()
    {
        isLocked = true;
    }
    protected virtual void Update()
    {
        if (grabHand != null)
        {
            transform.LookAt(grabHand, transform.forward);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!isLocked)
        {
            base.OnSelectEntered(args);
            grabHand = args.interactorObject.transform;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        grabHand = null;
    }
}