using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoorController : MonoBehaviour
{
    [Header("door open clip")]
    public AudioClip doorOpenClip;
    [Header("refuse door open clip")]
    public AudioClip refuseDoorOpenClip;

    private Animator ani;
    private Animator innerDoorAni;
    private LiftController liftController; //elevator controller
    private Vector3 dir;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        innerDoorAni = GameObject.FindWithTag(GameConsts.INNERDOOR).GetComponent<Animator>();
        liftController = GameObject.FindWithTag(GameConsts.LIFT).GetComponent<LiftController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConsts.PLAYER))
        {
            //if(other.transform.eulerAngles.y < -70 || other.transform.eulerAngles.y >70) //player must face to door
            //{
            //    return;
            //}
            if(PlayerBag.instance.hasKey) //if player has keycard
            {  //open door and play door-open audio clip
                ani.SetBool(GameConsts.DOOROPEN_PARAM, true); //outterDoor open

                innerDoorAni.SetBool(GameConsts.DOOROPEN_PARAM, true);//innerDoor open

                AudioSource.PlayClipAtPoint(doorOpenClip,transform.position);
            }
            else //if player hasn't keycard
            {  //play refuse door-open audio clip
                AudioSource.PlayClipAtPoint(refuseDoorOpenClip,transform.position);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConsts.PLAYER))
        {
            if (ani.GetBool(GameConsts.DOOROPEN_PARAM)) //if door has opened
            {  //close door and play door-close audio clip
                ani.SetBool(GameConsts.DOOROPEN_PARAM, false);

                innerDoorAni.SetBool(GameConsts.DOOROPEN_PARAM, false);

                AudioSource.PlayClipAtPoint(doorOpenClip, transform.position);

                dir = other.transform.position - transform.position; //the vector direction of doorToPlayer
                if(dir.z > 0) //player left trigger from inside of door
                {
                    sphereCollider.enabled = false; //enable trigger

                    liftController.BeginMove(); //elevator rises up
                }
            }
        }
    }
}
