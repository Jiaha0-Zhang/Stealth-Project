using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleDoorController : MonoBehaviour
{
    [Header("Door Open/Close Audio Clip")]
    public AudioClip doorAud;

    private int counter;//the number of units are inside of trigger
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger)
        {
            return;
        }
        if(other.CompareTag(GameConsts.PLAYER) || other.CompareTag(GameConsts.ENEMY))
        {
            if(++counter == 1)//unit+1,and judge if it is the first one come into trigger
            {    
                ani.SetBool(GameConsts.DOOROPEN_PARAM, true); // open door

                AudioSource.PlayClipAtPoint(doorAud,transform.position); //play open door audio clip
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if (other.CompareTag(GameConsts.PLAYER) || other.CompareTag(GameConsts.ENEMY))
        {
            if (--counter == 0)//unit -1, and judge if it is the last one leave out of trigger
            {    
                ani.SetBool(GameConsts.DOOROPEN_PARAM, false);//close door

                AudioSource.PlayClipAtPoint(doorAud, transform.position); //play close door audio clip
            }
        }
    }
}
