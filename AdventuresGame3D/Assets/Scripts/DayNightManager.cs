using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{

    public GameObject directional;
    public float totalSecondsTo16hours = 91;
    public float startDayRotationX = 0;
    public float endDayRotationX = 270;
    public float secondsLeft = 90;
    private float elapsedTime = 0;

    // Use this for initialization
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        secondsLeft = totalSecondsTo16hours - elapsedTime;
        //Debug.Log(secondsLeft);
        float rot = (endDayRotationX - startDayRotationX) / totalSecondsTo16hours;
        //directional.transform.Rotate(rot * Time.deltaTime, 0, 0);

        if(Input.GetKeyDown(KeyCode.R) || secondsLeft <= 0)
        {
            Reset();
        }
    }

    public void Reset()
    {
        secondsLeft = 90;
        totalSecondsTo16hours--;
        elapsedTime = 0;
        //directional.transform.rotation = Quaternion.Euler(startDayRotationX, directional.transform.rotation.eulerAngles.y, directional.transform.rotation.eulerAngles.z);
    }
}
