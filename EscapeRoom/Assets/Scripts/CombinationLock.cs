using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CombinationLock : MonoBehaviour
{
    public UnityAction UnlockAction;
    private void Unlocked() => UnlockAction?.Invoke(); // event created for using this function form anywhere.

    public UnityAction LockAction;

    private void OnLocked() => LockAction?.Invoke();

    [SerializeField] XRButtonInteractable[] comboButtons;
    [SerializeField] TMP_Text infoText;
    private const string startString = "Enter 3 digit Combo";
    private const string resetString = "Enter 3 digit To Reset Combo";
    [SerializeField] TMP_Text userInputText;
    [SerializeField] Image lockedPanel;
    [SerializeField] Color unlockedColor;
    [SerializeField] Color lockedColor;
    [SerializeField] TMP_Text lockedText;
    private const string unlockedString = "Unlocked";
    private const string lockedString = "Locked";
    [SerializeField] bool isLocked;
    [SerializeField] bool isResettable;
    private bool resetCombo;
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
        if (resetCombo)
        {
            resetCombo = false;
            LockCombo();
        }
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
            UnLockCombo();
           
        }
        else
        {
            ResetUserValues();
        }
    }

    private void UnLockCombo()
    {
        AudioManagerr.instance.PlaySFX(0);
        isLocked = false;
        Unlocked();
        lockedPanel.color = unlockedColor;
        lockedText.text = unlockedString;
        if (isResettable)
        {
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        infoText.text = resetString;
        ResetUserValues();
        buttonPresses = 0;
        resetCombo = true;
    }

    private void LockCombo()
    {
        isLocked = true;
        OnLocked();
        lockedPanel.color = lockedColor;
        lockedText.text = lockedString;
        infoText.text = startString;
        for(int i = 0; i < maxButtonPresses; i++)
        {
            comboValues[i] = inputValues[i];
        }
        ResetUserValues();
    }

    private void ResetUserValues()
    {
        inputValues = new int[maxButtonPresses];
        userInputText.text = "";
        buttonPresses = 0;
    }
}
