using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Retention Time")]
    public float waitTime = 2f;
    [Header("Chasing Speed")]
    public float chasingSpeed = 5f;
    [Header("Patrolling Speed")]
    public float patrollingSpeed = 2f;
    [Header("Patrol Points")]
    public Transform[] wayPoints;

    private EnemySightingAndHearing enemySightingAndHearing;
    private PlayerHealth playerHealth;
    private NavMeshAgent nav;
    private float timer;
    private int wayPointIndex = 0; //patrol points index

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();       
        enemySightingAndHearing = GetComponent<EnemySightingAndHearing>();
    }
    private void Start()
    {
        playerHealth = PlayerBag.instance.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if(enemySightingAndHearing.playerInsight && playerHealth.playerHP > 0) //robots find player, also player is alive
        {
            Shooting();
        }
                //personal alarm position is not equal to safe position
        else if (enemySightingAndHearing.personalAlarmPosition != AlarmSystem.instance.safePosition)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }
    private void Shooting()
    {
        nav.isStopped = true; //stop navigation
    }
    private void Chasing()
    {
        nav.isStopped = false;//recover navigation
        nav.speed = chasingSpeed; //set speed as chasingSpeed
        nav.stoppingDistance = 1;        
        nav.SetDestination(enemySightingAndHearing.personalAlarmPosition);//navigate to personal alarm position
        //if (nav.pathStatus == NavMeshPathStatus.PathPartial)//Emergency case, like player hide in rooms, enemy can't go to destination
        //{
        //    AlarmSystem.instance.alarmPosition = AlarmSystem.instance.safePosition; //clear alarm
        //                                                                            // (0,1000,0) = (0,1000,0)
        //    enemySightingAndHearing.personalAlarmPosition = AlarmSystem.instance.safePosition; //clear personal alarm
        //}
        if (nav.remainingDistance - nav.stoppingDistance < 0.05f)//judge if arrived destination
        {
            timer += Time.deltaTime; //timer start to record
            if (timer > waitTime) 
            {  
                AlarmSystem.instance.alarmPosition = AlarmSystem.instance.safePosition; //clear alarm
                // (0,1000,0) = (0,1000,0)
                enemySightingAndHearing.personalAlarmPosition = AlarmSystem.instance.safePosition; //clear personal alarm
                timer = 0; //reset timer
            }
        }
        else
        {
            timer = 0;//reset timer
        }
    }
    private void Patrolling()
    {
        nav.isStopped = false;//recover navigation
        nav.speed = patrollingSpeed; //set speed as chasingSpeed
        nav.stoppingDistance = 0;
        nav.SetDestination(wayPoints[wayPointIndex].position);
        if (nav.remainingDistance - nav.stoppingDistance < 0.05f)//judge if arrived destination
        {
            timer += Time.deltaTime;//timer start to record
            if(timer > waitTime)
            {
                //wayPointIndex++; //switch to next patrol point
                //if (wayPointIndex ==4 ) //防止数组越界 rookie way
                //{
                //    wayPointIndex = 0;
                //}

                //wayPointIndex++;
                //wayPointIndex = wayPointIndex % wayPoints.Length; //better way
                
                wayPointIndex = ++wayPointIndex % wayPoints.Length; //best way

                timer = 0; //reset timer
            }
        }
    }
}
