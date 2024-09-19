using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : SimpleHingeInteractable
{
    [SerializeField] CombinationLock comboLock;
    [SerializeField] Transform doorObject;
    [SerializeField] Vector3 rotationLimits;
    [SerializeField] Collider closedCollider;
    private bool isClosed;
    private Vector3 startRotation;


    [SerializeField] Collider openCollider;
    private bool isOpen;
    [SerializeField] private Vector3 endRotation;


    private float startAngleX;
    protected override void Start()
    {
        base.Start();
        startRotation = transform.localEulerAngles;
        startAngleX = startRotation.x;
        if (startAngleX >= 180)
        {
            startAngleX -= 360;
        }
        if (comboLock != null)
        {
            comboLock.UnlockAction += OnUnlocked;
            comboLock.LockAction += OnLocked;
        }
    }

    private void OnLocked()
    {
        LockHinge();
    }

    private void OnUnlocked()
    {
        UnlockHinge();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (doorObject != null)
        {
            doorObject.localEulerAngles = new Vector3(
                doorObject.transform.localEulerAngles.x, 
                transform.localEulerAngles.y, 
                doorObject.transform.localEulerAngles.z);

          
        }

        if (isSelected)
        {
            CheckLimits();
        }
    }


    private void CheckLimits()
    {
        isClosed = false;
        isOpen = false;
        float localAngelX = transform.localEulerAngles.x;

        if (localAngelX >= 180)
        {
            localAngelX = -360;// this has been done in order to get the localEulerAngles in the range of -15 and 15 degrees,
                               // if we dont do this sub, the angle is not going negative and is staying betwen 0 to 360, no negative numbers.
        }

        if (localAngelX >= startAngleX + rotationLimits.x||localAngelX<=startAngleX-rotationLimits.x) // here we are checking the limits for angle X. we are checing if it goes above 15 or below -15
        {
            ReleaseHinge();
           
        }
    }

    public override void ResetHinge() // in the video its protected instead of public dont know whats happning
    {
        if (isClosed)
        {
            transform.localEulerAngles = startRotation;
        }
        else if (isOpen)
        {
            transform.localEulerAngles = endRotation;
        }
        else
        {
            transform.localEulerAngles = new Vector3(startAngleX, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == closedCollider)
        {
            isClosed = true;
            ReleaseHinge();
        }

        if (other == openCollider)
        {
            isOpen = true;
            ReleaseHinge();
        }
    }
}
