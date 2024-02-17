using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Time From Walk To Run")]
    [Range(0.1f,2f)]
    public float dampTime = 1.5f;
    [Header("Player Turn Speed")]
    public float turnSpeed = 10f;
    [Header("Shout Clip")]
    public AudioClip shoutClip;

    private float hor, ver;
    private bool sneak, shout;
    private Animator ani;
    private AudioSource aud;
    private Vector3 dir; // vector direction 
    private Quaternion targetQua; //target Quaternion
    private PlayerHealth playerHealth;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        dir = Vector3.zero;
        playerHealth = GetComponent<PlayerHealth>(); 
    }
    private void Update()
    {
        if(playerHealth.playerHP <=0)
        {
            return;
        }
        hor = Input.GetAxis(GameConsts.HORIZONTAL);
        ver = Input.GetAxis(GameConsts.VERTICAL);
        sneak = Input.GetButton(GameConsts.SNEAK);
        shout = Input.GetButtonDown(GameConsts.SHOUT);

        dir.x = hor;
        dir.z = ver;

        if(hor != 0 || ver != 0)
        {
            ani.SetFloat(GameConsts.SPEED_PARAM, 5.66f,dampTime,Time.deltaTime);

            targetQua = Quaternion.LookRotation(dir); //change vector direction to quaternion
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQua, Time.deltaTime * turnSpeed);
        }
        else
        {
            ani.SetFloat(GameConsts.SPEED_PARAM, 1.4f); 
        }

        ani.SetBool(GameConsts.SNEAK_PARAM, sneak); //set sneak parameter

        if(shout)
        {
            ani.SetTrigger(GameConsts.SHOUT_PARAM);
        }
        FootStepAudioSetup();
    }
    private void FootStepAudioSetup()
    {    //measure current state if it's locomotion
        bool isLocomotion = ani.GetCurrentAnimatorStateInfo(0).shortNameHash == GameConsts.LOCOMOTION_STATE;

        if(isLocomotion) //if current state is locomotion
        {
            if(!aud.isPlaying) //if current state has no audio
            {
                aud.Play(); //now we play, here we go!
            }
        }
        else //if current state is not locomotion , it is idle or sneak
        {
            aud.Stop(); //stop playing
        }
    }

    private void PlayShoutAudio()
    {
        AudioSource.PlayClipAtPoint(shoutClip, transform.position);
    }
}
