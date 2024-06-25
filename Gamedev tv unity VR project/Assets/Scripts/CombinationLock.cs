using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class CombinationLock : MonoBehaviour
{
    [SerializeField] XRButtonInteractable[] comboButtons;
    [SerializeField] TMP_Text userInputText;
    // Start is called before the first frame update
    void Start()
    {
        userInputText.text = "";
        for(int i=0; i < comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }
    }

    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {
      
      for(int i = 0; i < comboButtons.Length; i++)
      {
            if (arg0.interactableObject.transform.name == comboButtons[i].transform.name)
            {
                userInputText.text = i.ToString();
            }
            else
            {
                comboButtons[i].ResetColor();
            }
      }
    }

}
