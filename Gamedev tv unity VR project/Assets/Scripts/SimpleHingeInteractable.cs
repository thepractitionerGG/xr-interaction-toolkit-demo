using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class SimpleHingeInteractable : XRSimpleInteractable
{
    [SerializeField] Vector3 positionLimits;
    private Transform grabHand;
    private Collider hingeCollider;
    private Vector3 hingePosition;
    [SerializeField] private bool isLocked;

    private const string Default_Layer = "Default";
    private const string Grab_Layer = "Grab";
    // Start is called before the first frame update
    protected virtual void Start()
    {
        hingeCollider = GetComponent<Collider>();
        hingePosition = hingeCollider.bounds.center;
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
            TrackHand();
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
        ChangeLayerMask(Grab_Layer);
        ResetHinge(); // this is an abstract function ok, we are doing this in this script as this script is inherited by other script which has a hinge functionaliy and they all need reseting at some point 
                      // so this is a genral call is made on release of a spefic hinge which is grabbed 
                      // so we dont need to call this function again and again in each hinge script
    }

    private void TrackHand()
    {
        transform.LookAt(grabHand, transform.forward);
        hingePosition = hingeCollider.bounds.center;
        if (grabHand.position.x >= hingePosition.x + positionLimits.x|| 
            grabHand.position.x <=hingePosition.x-positionLimits.x)
        {
            ReleaseHinge();
        }
       else if (grabHand.position.y >= hingePosition.y + positionLimits.y ||
           grabHand.position.y <= hingePosition.y - positionLimits.y)
       {
            ReleaseHinge();
       }
       else if (grabHand.position.z >= hingePosition.z + positionLimits.z ||
           grabHand.position.z <= hingePosition.z - positionLimits.z)
       {
            ReleaseHinge();
       }
    }


    public abstract void ResetHinge();
    public void ReleaseHinge()
    {
        ChangeLayerMask(Default_Layer);
    }

    private void ChangeLayerMask(string layer)
    {
        interactionLayers = InteractionLayerMask.GetMask(layer);
    }
}
