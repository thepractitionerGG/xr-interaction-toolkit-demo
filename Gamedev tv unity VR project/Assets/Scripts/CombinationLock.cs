using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class CombinationLock : MonoBehaviour
{
    [SerializeField] XRButtonInteractable[] comboButtons;
    [SerializeField] TMP_Text userInputText;
    [SerializeField] Image lockedPanel;
    [SerializeField] Color unlockedColor;
    [SerializeField] TMP_Text lockedText;

    [SerializeField] bool isLocked;
    [SerializeField] int[] comboValues = new int[3];
    [SerializeField] int[] inputValues;
    private int maxButtonPresses;
    private int buttonPresses;
    // Start is called before the first frame update
    void Start()
    {
        maxButtonPresses = comboValues.Length;
        ResetUserValues();
        
        for(int i=0; i < comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }
    }

    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {

        if (buttonPresses >= maxButtonPresses)
        {
            //
        }
        else
        {
            for (int i = 0; i < comboButtons.Length; i++)
            {
                if (arg0.interactableObject.transform.name == comboButtons[i].transform.name)
                {
                    userInputText.text += i.ToString();
                    inputValues[buttonPresses] = i;
                }
                else
                {
                    comboButtons[i].ResetColor();
                }
            }
            buttonPresses++;
            if (buttonPresses == maxButtonPresses)
            {
                CheckCombo();
            }
        }
     
    }

    private void CheckCombo()
    {
        int matches = 0;
        for(int i = 0; i < maxButtonPresses; i++)
        {
            if (inputValues[i] == comboValues[i])
            {
                matches++;
            }
        }

        if (matches == maxButtonPresses)
        {
            isLocked = false;
            lockedPanel.color = unlockedColor;
            lockedText.text = "Unlocked";
        }
        else
        {
            ResetUserValues();
        }
    }

    private void ResetUserValues()
    {
        inputValues = new int[maxButtonPresses];
        userInputText.text = "";
        buttonPresses = 0;
    }
}
