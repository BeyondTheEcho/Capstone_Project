using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager s_Instance { get; private set; }
    
    //Player events for player 1
    public event Action Player_1_Interact;
    public event Action Player_1_Drop;

    //Player events for player 2
    public event Action Player_2_Interact;
    public event Action Player_2_Drop;

    //Player events for player 3
    public event Action Player_3_Interact;
    public event Action Player_3_Drop;

    //Player events for player 4
    public event Action Player_4_Interact;
    public event Action Player_4_Drop;

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

        if (Input.GetAxis("Joy_2_Interact") > 0) Player_2_Interact?.Invoke();
        if (Input.GetAxis("Joy_2_Interact") < 0) Player_2_Drop?.Invoke();

        if (Input.GetAxis("Joy_3_Interact") > 0) Player_3_Interact?.Invoke();
        if (Input.GetAxis("Joy_3_Interact") < 0) Player_3_Drop?.Invoke();

        if (Input.GetAxis("Joy_4_Interact") > 0) Player_4_Interact?.Invoke();
        if (Input.GetAxis("Joy_4_Interact") < 0) Player_4_Drop?.Invoke();
    }
}
