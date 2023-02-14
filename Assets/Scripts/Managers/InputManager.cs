using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager s_Instance { get; private set; }

    public event Action Player_1_Interact;
    public event Action Player_1_Drop;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (s_Instance != null && s_Instance != this)
        {
            Destroy(this);
        }
        else
        {
            s_Instance = this;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Joy_1_Interact") > 0) Player_1_Interact?.Invoke();
        if (Input.GetAxis("Joy_1_Interact") < 0) Player_1_Drop?.Invoke();
    }
}
