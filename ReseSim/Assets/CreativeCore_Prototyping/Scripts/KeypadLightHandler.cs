using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadLightHandler : MonoBehaviour
{
    public int lightID;
    public Material defaultMaterial;
    public Material successMaterial;
    public Material failMaterial;
    private GameObject light;

    private void OnEnable() {
        KeypadHandler.keypadSuccess += FlashLight;
    }

    private void OnDisable() {
        KeypadHandler.keypadSuccess -= FlashLight;
    }

    private void Awake() {
        light = this.gameObject;
    }

    private void FlashLight(bool result) {

        light.GetComponent<Renderer>().material = defaultMaterial;
        
        if(result && lightID == 0) {
            light.GetComponent<Renderer>().material = successMaterial;
        }
        if(!result && lightID == 1) {
            light.GetComponent<Renderer>().material = failMaterial;
        }
    }
}
