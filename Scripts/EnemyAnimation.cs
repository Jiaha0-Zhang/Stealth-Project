using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    [Header("Blind Spot Degree")]
    public float blindZone = 4f;//blindspot zone

    private NavMeshAgent nav;
    private Animator ani;
    private EnemySightingAndHearing enemySightingAndHearing;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        enemySightingAndHearing = GetComponent<EnemySightingAndHearing>();
    }
    private void Start()
    {
        playerHealth = PlayerBag.instance.GetComponent<PlayerHealth>();
    }
    private void OnAnimatorMove()//角色通过动画发生了移动或旋转时执行，每帧一次
    {
        nav.velocity = ani.deltaPosition / Time.deltaTime;//通过动画位移除以每帧的时间，得到导航的瞬时速度

        transform.rotation = ani.rootRotation; //旋转使用动画的根旋转
    }
    private void Update()
    {   //计算期望速度向量在机器人自身前方的投影向量
        Vector3 projection = Vector3.Project(nav.desiredVelocity, transform.forward);

        ani.SetFloat(GameConsts.SPEED_PARAM, projection.magnitude,0.2f,Time.deltaTime);//setup speed parameter

        float angle = Vector3.Angle(transform.forward, nav.desiredVelocity);//求期望速度向量与自身前方向量的夹角

        if (nav.desiredVelocity == Vector3.zero)
        {
            angle = 0; //角速度强行设置为0
        }
        if(angle < blindZone && enemySightingAndHearing.playerInsight /*== true*/)
        {
            angle = 0;//角速度强行设置为0

            transform.LookAt(PlayerBag.instance.transform); //turn to player by LookAt
        }

        Vector3 normal = Vector3.Cross(transform.forward, nav.desiredVelocity);//求自身前方向量与期望速度向量的法向量Normal vector

        if(normal.y < 0)//左手定律 left hand law
        {
            angle *= -1; //夹角值变负号
        }
        angle *= Mathf.Deg2Rad; //角度转弧度
        ani.SetFloat(GameConsts.ANGULARSPEED_PARAM, angle, 0.2f, Time.deltaTime); //设置角速度 set angular speed parameter

        if(playerHealth.playerHP > 0)
        {
            ani.SetBool(GameConsts.PLAYERINSIGHT_PARAM, enemySightingAndHearing.playerInsight);//set if enemy can see player
        }
        else
        {
            ani.SetBool(GameConsts.PLAYERINSIGHT_PARAM, false);//set if enemy can see player
        }
    }
}
