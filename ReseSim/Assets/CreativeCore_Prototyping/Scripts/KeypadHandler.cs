using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadHandler : MonoBehaviour
{
    private List<int> digits;
    public List<GameObject> digitsDisplay;
    public List<int> password;
    public List<AudioClip> resultSounds;

    public bool idCardSwiped = false;
    private bool isSequenceCorrect;
    private int currentIndex = 0;
    private AudioSource audioSource;

    public static Action<bool> keypadSuccess;

    private void OnEnable() {
        KeypadKey.onKeypadButtonPressed += OnKeypadButtonPressed;
    }

    private void OnDisable() {
        KeypadKey.onKeypadButtonPressed -= OnKeypadButtonPressed;
    }

    private void Awake() {
        digits = new List<int>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void OnKeypadButtonPressed(int num) {
        
        if(num >= 0 && num <= 9) {
            if(digits.Count < password.Count) {
                digits.Add(num);
                currentIndex = digits.Count - 1;
            }   
        }
            
        if(num == 10) {
        //if backspace button
            if (digits.Count > 0 && digits.Count < password.Count + 1) {
                currentIndex = digits.Count - 1;
                digits.RemoveAt(currentIndex);
                }    
            }      
        

        if(num == 11) {
            //if ok button
            if(digits.Count == password.Count) {
                CheckCode();
            }
            //else clear screen and play beep
        }

        if(currentIndex > password.Count - 1) {
            currentIndex = password.Count - 1;
        }

        UpdateKeypadDisplay(num);
    }

    public void UpdateKeypadDisplay(int keypadKeyIndex) {

        //if number button
        if(keypadKeyIndex >= 0 && keypadKeyIndex <= 9 && currentIndex < 4 && currentIndex >= 0) {
            digitsDisplay[currentIndex].GetComponent<TextMeshProUGUI>().text = digits[currentIndex].ToString();        
            }

        //if backspace
        if(keypadKeyIndex == 10) {
            digitsDisplay[currentIndex].GetComponent<TextMeshProUGUI>().text = ""; 
        }

    }

    public void OnIDCardSwipe() {
        idCardSwiped = true;
    }

    public void ResetKeypad() {
        idCardSwiped = false;
        digits.Clear();

        for(int i = 0; i < digitsDisplay.Count; i++) {
            digitsDisplay[i].GetComponent<TextMeshProUGUI>().text = ""; 
        }
    }

    public void CheckCode() {

        for(int i = 0; i < digits.Count; i++) {
            if(digits[i] != password[i]){
                Debug.Log("Try again!");
                isSequenceCorrect = false;
                keypadSuccess?.Invoke(false);
                PlayResultsSound(1);
                break;
                }
            else isSequenceCorrect = true;
            PlayResultsSound(0);
             
            }

        if(isSequenceCorrect) {
            Debug.Log("Congrats! You got it!");
            keypadSuccess?.Invoke(true);
        }    
        
        ResetKeypad();

    }

    public void PlayResultsSound(int index) {
        audioSource.clip = resultSounds[index];
        audioSource.Play();   
    } 

}
