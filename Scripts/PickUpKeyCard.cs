using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKeyCard : MonoBehaviour
{
    [Header("Audip Clip of Pick Up Keycard")]
    public AudioClip pickUpClip;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(GameConsts.PLAYER))
        {
            PlayerBag.instance.hasKey = true; //if player picked keycard

            AudioSource.PlayClipAtPoint(pickUpClip,transform.position); //play audio clip

            Destroy(gameObject); //destroy keycard model on the ground
        }
    }
}
