using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySightingAndHearing : MonoBehaviour
{
    [Header("Angle Of Robot View")]
    public float fieldOfView = 110;
    [HideInInspector]
    public bool playerInsight = false;
    [HideInInspector]
    public Vector3 personalAlarmPosition;

    private Vector3 previousAlarmPosition; //last frame alarm position
    private Vector3 dir;//the vector direction of Robot points to Player
    private Transform player;
    private Animator player_Ani; 
    private RaycastHit hit;
    private NavMeshAgent nav;
    private NavMeshPath path;
    private SphereCollider sphereCollider;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        player = PlayerBag.instance.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        player_Ani = player.GetComponent<Animator>();
        personalAlarmPosition = AlarmSystem.instance.safePosition; //initialization
        previousAlarmPosition = AlarmSystem.instance.safePosition; //initialization
    }
    private void Update()
    {
        CheckAlarmPositionChanged();//detect update of alarm position    
    }
    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag(GameConsts.PLAYER))
        {
            return;
        }
        if(playerHealth.playerHP <= 0)
        {
            return;
        }
        Sighting(); //detect sighting
        Hearing(); //detect hearing
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(GameConsts.PLAYER))
        {
            return;
        }
        playerInsight = false;
    }
    private void CheckAlarmPositionChanged()
    {
        if (previousAlarmPosition != AlarmSystem.instance.alarmPosition)//compare last frame and current frame alarm position
        {
            personalAlarmPosition = AlarmSystem.instance.alarmPosition; //send new alarm position to current robotEnemy
        }
        previousAlarmPosition = AlarmSystem.instance.alarmPosition; //save current frame alarm position for next frame using
    }
    private void Sighting()
    {
        playerInsight = false;
        dir = player.position - transform.position;
        float angle =  Vector3.Angle(dir, transform.forward);//calculate angle of vector direction and robot forward vector

        if(angle > fieldOfView/2)//player is not located at Robot's field of view
        {
            return;
        }
        if(Physics.Raycast(transform.position 
            + Vector3.up* GameConsts.ENEMY_EYES_OFFSET,dir,out hit)) //send a physical ray to player
        {
            if (hit.collider.CompareTag(GameConsts.PLAYER))//if ray detected player
            {
                playerInsight = true;
                AlarmSystem.instance.alarmPosition = player.position; //Robot finds player, and triggers alarm
            }
        }
    }
    private void Hearing()
    {
        bool isPlayingLocomotion = player_Ani.GetCurrentAnimatorStateInfo(0).shortNameHash == GameConsts.LOCOMOTION_STATE;

        bool isShout = player_Ani.GetCurrentAnimatorStateInfo(1).shortNameHash == GameConsts.SHOUT_PARAM;

        if(!isPlayingLocomotion && !isShout) //if player is not moving or shouting, then return
        {
            return;
        }
        bool canArrive = nav.CalculatePath(player.position, path);//pretend if robot can find player by navigation

        //if (path.status == NavMeshPathStatus.PathPartial)
        //{
        //    return;
        //}
        if (!canArrive)
        {
            Vector3[] points = new Vector3[path.corners.Length + 2];
            points[0] = transform.position; //start point
            points[points.Length - 1] = player.position; //end point
            for (int i = 0; i < points.Length-1; i++)
            {
                points[i] = path.corners[i - 1];
            }
            float distance = 0;
            for (int i = 0; i< points.Length - 1; i++)
            {
                distance += Vector3.Distance(points[i], points[i + 1]); //calculate distance between two points
            }
            if (distance <= sphereCollider.radius)//if distance is short enough, then robot can hear
            {
                personalAlarmPosition = player.position; //trigger personal alarm
            }
        }
    }
}
