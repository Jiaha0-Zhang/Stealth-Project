using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("The Number of Gears For Observation Points")]
    public int gear = 5;
    [Header("Move Speed")]
    public float moveSpeed = 1f;
    [Header("Turn Speed")]
    public float turnSpeed = 1f;

    private Transform followTarget; //follow target
    private Vector3 dir; //the vector direction of camera follows target
    private RaycastHit hit;

    private void Awake()
    {
        followTarget = GameObject.FindWithTag(GameConsts.PLAYER).transform;
    }
    private void Start()
    {
        dir = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET - transform.position;//calculate initial vector direction
    }
    private void Update()
    {
        Vector3 bestPos = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET - dir; //camera has the best position to follow target
        Vector3 worstPos = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET
            + Vector3.up * (dir.magnitude + GameConsts.VIEWPOINT_OFFSET); //worst position to follow target
        Vector3[] viewPointPositions = new Vector3[gear]; //view points array
        viewPointPositions[0] = bestPos; //setup the 1st view points
        viewPointPositions[viewPointPositions.Length - 1] = worstPos; //setup the last  view points
        
        for (int i = 1; i < viewPointPositions.Length - 1; i++) //setup all middle view points
        {
            viewPointPositions[i] = Vector3.Lerp(bestPos, worstPos, (float)i / (gear - 1));
        }

        Vector3 fitPos = bestPos; //find the most fittest view point
        for(int i = 0;i < viewPointPositions.Length; i++)//traverse all view points for finding a point which can monitor player
        {
            if(CanSeeTarget(viewPointPositions[i])) //this point can monitor player
            {
                fitPos = viewPointPositions[i];
                break;//no need to analysis other points
            }
        }
        transform.position = Vector3.Lerp(transform.position, fitPos, Time.deltaTime * moveSpeed);

        Vector3 lookDir = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET - transform.position;
        Quaternion targetQua = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQua, Time.deltaTime * turnSpeed);

        Vector3 eularAngles = transform.eulerAngles; //get current camera eualrAngle
        eularAngles.y = 0; //set Y and Z to 0, for stablishing camera
        eularAngles.z = 0;
        transform.eulerAngles = eularAngles;
    }
    private bool CanSeeTarget(Vector3 pos)
    {
        Vector3 currentDir = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET - pos;
        if (Physics.Raycast(pos, currentDir, out hit)) //ray detected colliders
        {
            if (hit.collider.CompareTag(GameConsts.PLAYER))//if detected player
            {
                return true;
            }
        }
        return false; //all other situations, can't detect player
    }
}
