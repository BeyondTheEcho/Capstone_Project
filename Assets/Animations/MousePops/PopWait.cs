using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopWait : MonoBehaviour
{
    public static bool waitingOver = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingOver==false)
        {
            StartCoroutine(NextMousePops());
        }
    }
    IEnumerator NextMousePops()
    {
        // wait for 1 second
        Debug.Log("coroutineA created");
        yield return new WaitForSeconds(5.0f);
        waitingOver = true;
        Debug.Log("coroutineA running again");
    }
}
