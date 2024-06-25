using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class SimpleUIControl : MonoBehaviour
{
    [SerializeField] XRButtonInteractable startButton;
    [SerializeField] string[] msgString;
    [SerializeField] TMP_Text[] msgText;
    [SerializeField] GameObject keyIndicatorLight;
    // Start is called before the first frame update
    void Start()
    {
        if (startButton != null)
        {
            startButton.selectEntered.AddListener(EnableKeyLight);
        }
    }

    private void EnableKeyLight(SelectEnterEventArgs arg0)
    {
        SetText(msgString[1]);
        if (keyIndicatorLight != null)
            keyIndicatorLight.SetActive(true);
    }

    public void SetText(string msg)
    {
        for (int i = 0; i < msgText.Length; i++)
        {
            msgText[i].text = msg;
        }
    }
}
