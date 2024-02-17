using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    public float moveSpeed = 3f;
    [HideInInspector]
    public bool beginMove = false; //elevator start to rise

    private AudioSource aud;
    private float timer;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    public void BeginMove()
    {
        beginMove = true;
        aud.Play();
    }
    private void Update()
    {
        if(beginMove)
        {
            timer += Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime * moveSpeed; //elevator rises up
            PlayerBag.instance.transform.position += Vector3.up * Time.deltaTime * moveSpeed; //player rises up

            if(timer > aud.clip.length)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);//reload scence(Game END)
            }
        }
    }
}
