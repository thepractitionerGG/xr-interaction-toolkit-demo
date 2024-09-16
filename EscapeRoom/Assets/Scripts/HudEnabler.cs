using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HudEnabler : MonoBehaviour
{
    [SerializeField] Transform HudLocation;
    [SerializeField] GameObject HudMennu;
    public InputActionAsset inputActionAsset;
    private bool menuEnabled = false;
    private InputAction switchAction;
    private void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("XRControls");
        switchAction = actionMap.FindAction("Switch");
        switchAction.actionMap.Enable();
        switchAction.performed += OnSwitch;
    }

    private void OnDisable()
    {
       
        switchAction.actionMap.Disable();
        switchAction.performed -= OnSwitch;
    }


    private void OnSwitch(InputAction.CallbackContext obj)
    {
        Debug.Log("button");

        if (HudMennu.activeInHierarchy)
        {
            HudMennu.SetActive(false);
        }
        else
        {
            HudMennu.SetActive(true);
            HudMennu.transform.position = HudLocation.position;
        }
    }
}
