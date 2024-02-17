using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    public static AlarmSystem instance; //singleton script
    [Header("transitional Velocity")]
    public float turnSpeed = 3f;
    [HideInInspector]
    public Vector3 alarmPosition = new Vector3(0, 1000, 0);
    [HideInInspector]
    public Vector3 safePosition = new Vector3(0, 1000, 0);

    private AlarmLight alarmLight; //alarm light script
    private Light mainLight; //main light
    private AudioSource normalAud;  //normal background music
    private AudioSource panicAud; //panic background music
    private GameObject[] megaPhoneObj; //megaPhone object
    private AudioSource[] megaPhoneAud; //megaPhone audio

    private void Awake()
    {
        instance = this;
        alarmLight = GameObject.FindWithTag(GameConsts.ALARMLIGHT).GetComponent<AlarmLight>();
        mainLight = GameObject.FindWithTag(GameConsts.MAINLIGHT).GetComponent<Light>();
        normalAud = GetComponent<AudioSource>();
        panicAud = transform.GetChild(0).GetComponent<AudioSource>();
        megaPhoneObj = GameObject.FindGameObjectsWithTag(GameConsts.MEGAPHONE);
    }

    private void Start()
    {
        megaPhoneAud = new AudioSource[megaPhoneObj.Length]; //instantiate audiosource array
        for (int i = 0; i < megaPhoneObj.Length; i++)
        {
            megaPhoneAud[i] = megaPhoneObj[i].GetComponent<AudioSource>(); //traversal all megaphones' audiosource component
        }
    }
    private void Update()
    {
        if (alarmPosition == safePosition) //alarm off
        {
            AlarmSystemOperation(false);
        }
        else //alarm on
        {
            AlarmSystemOperation(true);
        }
        //AlarmSystemOperation(alarmPosition != safePosition); //¼ò»¯°æ
    }
    private void  AlarmSystemOperation(bool alarmOn)
    {
        float value = 0;
        if (alarmOn)
        {
            value = 1;
        }

        alarmLight.alarmOn = alarmOn; //alarmlight on
        mainLight.intensity = Mathf.Lerp(mainLight.intensity, 1 - value, Time.deltaTime * turnSpeed); //operate mainLight
        normalAud.volume = Mathf.Lerp(normalAud.volume, 1 - value, Time.deltaTime * turnSpeed);//operate normal background music
        panicAud.volume = Mathf.Lerp(panicAud.volume, value, Time.deltaTime * turnSpeed);//operate panic background music
        for (int i = 0; i < megaPhoneAud.Length; i++)
        {
            megaPhoneAud[i].volume = Mathf.Lerp(megaPhoneAud[i].volume, value, Time.deltaTime * turnSpeed);
        }   //operate megaphone sounds
    }
}
