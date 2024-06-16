using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerInteractable : XRGrabInteractable
{
    private Transform parentTransform;

    [SerializeField] XRSocketInteractor keySocket;
  
    [SerializeField] private Transform drawerTransform;

    [SerializeField] bool isLocked;
    [SerializeField] bool isGrabbed;

    Vector3 limitPositions;
    [SerializeField] Vector3 limitDistances = new Vector3(.02f, .02f, 0);
    [SerializeField] float drawerLimitZ = .8f;

    private const string defaultLayer = "Default";
    private const string grabLayer = "Grab";

    void Start()
    {
        if (keySocket != null)
        {
            keySocket.selectEntered.AddListener(OnDrawerUnlocked);
            keySocket.selectExited.AddListener(OnDrawerLocked);
        }
        parentTransform = transform.parent.transform;
        limitPositions = drawerTransform.localPosition;
        Debug.Log(limitPositions);
    }

    private void OnDrawerLocked(SelectExitEventArgs arg0)
    {
        isLocked = true;
    }

    private void OnDrawerUnlocked(SelectEnterEventArgs arg0)
    {
        isLocked = false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (!isLocked)
        {
            transform.SetParent(parentTransform);
            isGrabbed = true;
          
        }
        else
        {
            ChangeLayerMask(defaultLayer);
        }
    }

   
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
    
        ChangeLayerMask(grabLayer);
        isGrabbed = false;
        transform.localPosition = drawerTransform.localPosition;
    }

    private void ChangeLayerMask(string layer)
    {
        interactionLayers = InteractionLayerMask.GetMask(layer);
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrabbed && drawerTransform!=null)
        {
            drawerTransform.localPosition = new Vector3(drawerTransform.localPosition.x, 
                drawerTransform.localPosition.y, 
                transform.localPosition.z); // movement logic, drawer follows the z of this gameobject here

            CheckLimits();
        }
    }

    private void CheckLimits()
    {
        if (transform.localPosition.x >= limitPositions.x + limitDistances.x 
            || transform.localPosition.x <= limitPositions.x - limitDistances.x)
        {
            ChangeLayerMask(defaultLayer);
            Debuger(0);
        }

        else if (transform.localPosition.y >= limitPositions.y + limitDistances.y 
            || transform.localPosition.y <= limitPositions.y - limitDistances.y)
        {
            ChangeLayerMask(defaultLayer);
            Debuger(1);
        }

        else if (drawerTransform.localPosition.z <= limitPositions.z - limitDistances.z)
        {
            isGrabbed = false;
            drawerTransform.localPosition = limitPositions;
            ChangeLayerMask(defaultLayer);
            Debuger(2);
        }

        else if (drawerTransform.localPosition.z > drawerLimitZ + limitDistances.z)
        {
            isGrabbed = false;
            drawerTransform.localPosition = new Vector3(drawerTransform.localPosition.x, 
                drawerTransform.localPosition.y, 
                drawerLimitZ-.02f);  // this .02f was added so that the drawer remains grabbable after auto release, it takes the drawer a lil back from its limit position which is .8
            ChangeLayerMask(defaultLayer);
            Debuger(3);
        }
    }


    public static void Debuger(int v)
    {
        Debug.Log(v);
    }

}
