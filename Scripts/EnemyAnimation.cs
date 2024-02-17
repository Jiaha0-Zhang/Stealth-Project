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
    private void OnAnimatorMove()//��ɫͨ�������������ƶ�����תʱִ�У�ÿ֡һ��
    {
        nav.velocity = ani.deltaPosition / Time.deltaTime;//ͨ������λ�Ƴ���ÿ֡��ʱ�䣬�õ�������˲ʱ�ٶ�

        transform.rotation = ani.rootRotation; //��תʹ�ö����ĸ���ת
    }
    private void Update()
    {   //���������ٶ������ڻ���������ǰ����ͶӰ����
        Vector3 projection = Vector3.Project(nav.desiredVelocity, transform.forward);

        ani.SetFloat(GameConsts.SPEED_PARAM, projection.magnitude,0.2f,Time.deltaTime);//setup speed parameter

        float angle = Vector3.Angle(transform.forward, nav.desiredVelocity);//�������ٶ�����������ǰ�������ļн�

        if (nav.desiredVelocity == Vector3.zero)
        {
            angle = 0; //���ٶ�ǿ������Ϊ0
        }
        if(angle < blindZone && enemySightingAndHearing.playerInsight /*== true*/)
        {
            angle = 0;//���ٶ�ǿ������Ϊ0

            transform.LookAt(PlayerBag.instance.transform); //turn to player by LookAt
        }

        Vector3 normal = Vector3.Cross(transform.forward, nav.desiredVelocity);//������ǰ�������������ٶ������ķ�����Normal vector

        if(normal.y < 0)//���ֶ��� left hand law
        {
            angle *= -1; //�н�ֵ�为��
        }
        angle *= Mathf.Deg2Rad; //�Ƕ�ת����
        ani.SetFloat(GameConsts.ANGULARSPEED_PARAM, angle, 0.2f, Time.deltaTime); //���ý��ٶ� set angular speed parameter

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
