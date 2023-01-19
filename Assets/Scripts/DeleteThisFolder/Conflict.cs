using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conflict : MonoBehaviour
{
    public void DoPublicStuff()
    {
        Debug.LogError("This Function Dsdfdsfoes Public Stuff Because Reasons.");
        int newVal = 0;

        if (newVal != 0)
        {
            Debug.Log("HEYO!");
        }
    }
}
