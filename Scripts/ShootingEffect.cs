using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEffect : MonoBehaviour
{
    [Header("Shooting Audio")]
    public AudioClip shootingAud;

    private LineRenderer lineRenderer;
    private Light lt;
    private Transform player;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lt = GetComponent<Light>();
    }
    private void Start()
    {
        player = PlayerBag.instance.transform;
    }
    public void PlayShootingEffect()
    {
        lineRenderer.positionCount = 2; //设置激光端点个数
        lineRenderer.SetPosition(0,transform.position); //设置端点坐标
        lineRenderer.SetPosition(1,player.position +Vector3.up * GameConsts.PLAYER_HEART_OFFSET);
        lt.enabled = true; //enable light
        AudioSource.PlayClipAtPoint(shootingAud,transform.position);//play clip

        Invoke("DelayClose", 0.1f);//delay close effects
    }
    private void DelayClose()
    {
        lineRenderer.positionCount = 0; //设置顶点个数为0
        lt.enabled = false; //turn off light
    }
}
