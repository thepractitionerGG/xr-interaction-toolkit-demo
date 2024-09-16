using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchMovementType : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    private bool teleport = false;
    private InputAction switchAction;

    [SerializeField] Move move;
    [SerializeField] XRRayInteractor left, right;
    private void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("XRControls");
        switchAction = actionMap.FindAction("Switch");
        switchAction.Enable();
        switchAction.performed += OnSwitch;


    }

    private void OnDisable()
    {
        switchAction.performed -= OnSwitch;

        switchAction.Disable();
    }

    private void OnSwitch(InputAction.CallbackContext obj)
    {
        if (teleport)
        {
            teleport = false;
            left.enabled = false;
            right.enabled = false;
            move.enabled = true;
        }
        else
        {
            teleport = true;
            left.enabled = true;
            right.enabled = true;
            move.enabled = false; 
        }
    }
}
