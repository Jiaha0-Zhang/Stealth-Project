using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    public static PlayerBag instance; //singleton

    [Header("Player has Keycard")]
    public bool hasKey = false;

    private void Awake()
    {
        instance = this;
    }
}
