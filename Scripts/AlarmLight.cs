using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    [Header("Alarm Switch ")]
    public bool alarmOn;
    [Header("Alarm Flash Speed")]
    public float turnSpeed = 3f;

    private float highIntensity = 4f;
    private float lowIntensity = 0f;
    private float targetIntensity;
    private Light lt; //Light component

    private void Awake()
    {
        lt = GetComponent<Light>();
    }

    private void Start()
    {
        targetIntensity = highIntensity; //initial value
    }
    private void Update()
    {
        if (alarmOn) //alarm switch on
        {
            if(Mathf.Abs(lt.intensity - targetIntensity) < 0.05f) //lightIntensity reach to targetIntensity
            {
                if(targetIntensity == highIntensity)
                {
                    targetIntensity = lowIntensity;
                }
                else
                {
                    targetIntensity = highIntensity;
                }
            }
            lt.intensity = Mathf.Lerp(lt.intensity, targetIntensity, Time.deltaTime * turnSpeed);
        }
        else //alarm switch off
        {
            lt.intensity = Mathf.Lerp(lt.intensity, lowIntensity, Time.deltaTime * turnSpeed);
            if (Mathf.Abs(lt.intensity - lowIntensity) < 0.05f)
            {
                lt.intensity = 0;
            }
        }
    }
}
