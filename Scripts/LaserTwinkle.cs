using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTwinkle : MonoBehaviour
{
    [Header("Twinkle Interval Time")]
    public float interval = 2f;

    private float timer = 0;//timer
    Vector3 originsPos; //origin position
    private bool isShow; //if shows current laser

    private void Start()
    {
        originsPos = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            isShow = !isShow;
            timer = 0;
        }
        if(isShow)
        {
            transform.position = originsPos;
        }
        else
        {
            transform.position = Vector3.up * 1000;
        }
    }
}
