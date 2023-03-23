using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager s_Instance { get; private set; }

    //Add Delegates
    public delegate void AddPlayer(int playerNumber);
    public AddPlayer AddPlayerFunc;

    public delegate void RemovePlayer(int playerNumberR);
    public RemovePlayer RemovePlayerFunc;

    //Player events for player 1
    public event Action Player_1_Interact;
    public event Action Player_1_Drop;
    public event Action Player_1_Move;
 
    //Player events for player 2
    public event Action Player_2_Interact;
    public event Action Player_2_Drop;
    public event Action Player_2_Move;

    //Player events for player 3
    public event Action Player_3_Interact;
    public event Action Player_3_Drop;
    public event Action Player_3_Move;

    //Player events for player 4
    public event Action Player_4_Interact;
    public event Action Player_4_Drop;
    public event Action Player_4_Move;

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
        if (Input.GetAxis("Horizontal1") > 0) Player_1_Move?.Invoke();
        if (Input.GetAxis("Vertical1") < 0) Player_1_Move?.Invoke();
        if (Input.GetAxis("Add1") > 0 && AddPlayerFunc != null) AddPlayerFunc(0);
        if (Input.GetAxis("Remove1") > 0 && RemovePlayerFunc != null) RemovePlayerFunc(0);

        if (Input.GetAxis("Joy_2_Interact") > 0) Player_2_Interact?.Invoke();
        if (Input.GetAxis("Joy_2_Interact") < 0) Player_2_Drop?.Invoke();
        if (Input.GetAxis("Horizontal2") > 0) Player_2_Move?.Invoke();
        if (Input.GetAxis("Vertical2") < 0) Player_2_Move?.Invoke();
        if (Input.GetAxis("Add2") > 0 && AddPlayerFunc != null) AddPlayerFunc(1);
        if (Input.GetAxis("Remove2") > 0 && RemovePlayerFunc != null) RemovePlayerFunc(1);

        if (Input.GetAxis("Joy_3_Interact") > 0) Player_3_Interact?.Invoke();
        if (Input.GetAxis("Joy_3_Interact") < 0) Player_3_Drop?.Invoke();
        if (Input.GetAxis("Horizontal3") > 0) Player_3_Move?.Invoke();
        if (Input.GetAxis("Vertical3") < 0) Player_3_Move?.Invoke();
        if (Input.GetAxis("Add3") > 0 && AddPlayerFunc != null) AddPlayerFunc(2);
        if (Input.GetAxis("Remove3") > 0 && RemovePlayerFunc != null) RemovePlayerFunc(2);

        if (Input.GetAxis("Joy_4_Interact") > 0) Player_4_Interact?.Invoke();
        if (Input.GetAxis("Joy_4_Interact") < 0) Player_4_Drop?.Invoke();
        if (Input.GetAxis("Horizontal4") > 0) Player_4_Move?.Invoke();
        if (Input.GetAxis("Vertical4") < 0) Player_4_Move?.Invoke();
        if (Input.GetAxis("Add4") > 0 && AddPlayerFunc != null) AddPlayerFunc(3);
        if (Input.GetAxis("Remove4") > 0 && RemovePlayerFunc != null) RemovePlayerFunc(3);
    }
}
