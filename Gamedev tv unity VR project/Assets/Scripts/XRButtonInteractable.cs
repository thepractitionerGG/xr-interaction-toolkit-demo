using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
public class XRButtonInteractable : XRSimpleInteractable
{
    [SerializeField] Image buttonImage;
    [SerializeField] Color[] buttonColors = new Color[4];
    private Color normalColor;
    private Color highlightColor;
    private Color pressedColor;
    private Color selectedColor;
    private bool isPressed;



    void Start()
    {
        normalColor = buttonColors[0];
        highlightColor = buttonColors[1];

        buttonImage.color = normalColor;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        isPressed = false;
        buttonImage.color = highlightColor;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        if (!isPressed)
        {
            buttonImage.color = normalColor;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        isPressed = true;
        buttonImage.color = buttonColors[2];
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        isPressed = false;
        buttonImage.color = buttonColors[3];

    }


    void Update()
    {
        
    }
}
