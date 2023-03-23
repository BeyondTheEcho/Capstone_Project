using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    public Vector3 originalPosition;
    private Vector3 startPos;
    private Transform thisTransform;
    public float sensitivity = 0.2f;

    void Start()
    {
        thisTransform = transform;
        this.transform.position = originalPosition;
    }

    void Update()
    {
        Vector3 inputDirection = Vector3.zero * sensitivity;
        inputDirection.x = Input.GetAxis("MouseX") * 0.1f;
        inputDirection.y = Input.GetAxis("MouseY") * 0.1f;
        thisTransform.position = startPos + inputDirection * sensitivity;
        startPos = thisTransform.position;

        this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1f, 1f), Mathf.Clamp(transform.position.y, -2f, 2f), transform.position.z);
    }
}