using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class ToolBase : MonoBehaviour
{
    public abstract string m_ToolType { get; }

    private float m_DespawnTime = 30.0f;

    void Start()
    {
        Destroy(gameObject, m_DespawnTime);
    }

    public void OnCollisionEnter2D(Collider col)
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {

        }
    }
}
