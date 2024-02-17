using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("Controlled Laser")]
    public GameObject controlledLaser;
    [Header("Audio Clip of Turn Off Laser")]
    public AudioClip switchAud;
    [Header("Material of Unlock Screen")]
    public Material unlockMat;

    private MeshRenderer screenMeshRenderer;

    private void Awake()
    {
        screenMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(GameConsts.PLAYER) && Input.GetButtonDown(GameConsts.SWITCH) && controlledLaser.activeSelf)
        {
            controlledLaser.SetActive(false); //turn off controlled laser

            AudioSource.PlayClipAtPoint(switchAud, transform.position); //play audio clip

            screenMeshRenderer.material = unlockMat; //change material
        }
    }
}
