using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Move : MonoBehaviour
{
    public InputActionAsset inputActionAsset; // Drag your InputAction asset here
    public float moveSpeed = 3.0f;
    public float rotationSpeed = 100.0f;

    private InputAction moveAction;
    private InputAction lookAction;

    private Vector2 moveInput;
    private Vector2 lookInput;



    void OnEnable()
    {
        // Get the action map and actions
        var actionMap = inputActionAsset.FindActionMap("XRControls");
        moveAction = actionMap.FindAction("Move");
        lookAction = actionMap.FindAction("Look");
       

        // Enable the actions
        moveAction.Enable();
        lookAction.Enable();
       

        // Subscribe to the input events
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        lookAction.performed += OnLook;
        lookAction.canceled += OnLook;

       
    }

    void OnDisable()
    {
        // Unsubscribe from the input events
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        lookAction.performed -= OnLook;
        lookAction.canceled -= OnLook;

      

        // Disable the actions
        moveAction.Disable();
        lookAction.Disable();
     
    }


    void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        // Handle movement
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 move = transform.TransformDirection(direction) * moveSpeed * Time.deltaTime;
        transform.position += move;

    }
}
