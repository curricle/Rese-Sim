using System;
using System.Collections.Generic;
using UnityEngine;

public class KeypadKey : MonoBehaviour
{
    public int keyID;
    public Material keyPressedMaterial;
    public Material keyIdleMaterial;
    public List<AudioClip> keyClicks;
    private AudioSource audioSource;

    public static Action<int> onKeypadButtonPressed;
    
    public void KeypadKeyPressed() {
        onKeypadButtonPressed?.Invoke(keyID);
    }

    private void OnMouseOver() {
        gameObject.GetComponent<Renderer>().material = keyPressedMaterial;
    }

    private void OnMouseExit() {
        gameObject.GetComponent<Renderer>().material = keyIdleMaterial;
    }

    private void OnMouseDown() {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = keyClicks[UnityEngine.Random.Range(0, keyClicks.Count)];
        audioSource.Play();
        KeypadKeyPressed();
    }

}
