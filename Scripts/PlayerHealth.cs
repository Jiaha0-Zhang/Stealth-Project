using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player HP")]
    public float playerHP = 100;
    [Header("Game Restart Audio Clip")]
    public AudioClip restartClip;

    private Animator ani;
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    public void DamageTaken(float damage)
    {
        if (playerHP <= 0)
        {
            return;
        }
        playerHP -= damage;
        if (playerHP <= 0)
        {
            ani.SetTrigger(GameConsts.DEAD_STATE);//trigger player dead audio clip
            AlarmSystem.instance.alarmPosition = AlarmSystem.instance.safePosition; //alarm clear
            AudioSource.PlayClipAtPoint(restartClip,transform.position); //play game over audio clip
            Invoke("RestartGame", 3f);
        }
    }
    public void  RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
