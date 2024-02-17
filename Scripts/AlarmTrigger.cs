using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConsts.PLAYER))
        {
            AlarmSystem.instance.alarmPosition = other.transform.position; //set player position to alarm postion
        }
    }
}
