using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivator : MonoBehaviour
{

    public Material normalMaterial;
    public Material lightMaterial;
    public Light myLight;
    public float minutesLeftToActivate = 70;

    private MeshRenderer myRenderer;
    DayNightManager dayNight;

    bool lightsOn;
    
    // Use this for initialization
    void Start()
    {
        dayNight = DayNightManager.instance;
        myRenderer = GetComponent<MeshRenderer>();
        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        if(dayNight.secondsLeft <= minutesLeftToActivate && !lightsOn)
        {
            lightsOn = true;
            TurnOn();
        }
        else if(dayNight.secondsLeft > minutesLeftToActivate && lightsOn)
        {
            lightsOn = false;
            TurnOff();
        }
    }

    void TurnOn()
    {
        myLight.enabled = true;
        if(lightMaterial != null)
            myRenderer.materials[5] = lightMaterial;
    }

    void TurnOff()
    {
        myLight.enabled = false;
        if (normalMaterial != null)
            myRenderer.materials[5] = normalMaterial;
    }
}
