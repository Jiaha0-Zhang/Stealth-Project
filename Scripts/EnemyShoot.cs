using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private Animator ani;
    private Transform player;
    private PlayerHealth playerHealth;
    private ShootingEffect shootingEffect;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        shootingEffect = GetComponentInChildren<ShootingEffect>();
    }
    private void Start()
    {
        playerHealth = PlayerBag.instance.GetComponent<PlayerHealth>();
        player = PlayerBag.instance.transform;
    }

    private void OnAnimatorIK(int layerIndex)
    {   //如果当前机器人执行的不是射击动画，就不要开启IK
        if (ani.GetCurrentAnimatorStateInfo(1).shortNameHash != GameConsts.WEAPONSHOOT_STATE)
        {
            return;
        }
        ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);//set right hand ik position weight
        ani.SetLookAtWeight(1); //set eyes weight
        ani.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * GameConsts.PLAYER_HEART_OFFSET);
        ani.SetLookAtPosition(player.position + Vector3.up * GameConsts.PLAYER_HEART_OFFSET);
    }
    public void Shoot()
    {
        shootingEffect.PlayShootingEffect(); //play shooting effects

        playerHealth.DamageTaken(60);
    }
}
