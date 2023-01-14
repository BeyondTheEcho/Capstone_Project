using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    public Vector2 speed = new Vector2(5, 5);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        transform.Translate(input.x * speed.x * Time.deltaTime, input.y * speed.y * Time.deltaTime, 0.0f);
    }
}
