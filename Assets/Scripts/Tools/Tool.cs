using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tool : MonoBehaviour
{
    [SerializeField] private float m_DespawnTime = 30.0f;
    [SerializeField] private ToolType m_ToolType; 

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.radius = 1;
        col.isTrigger = true;

        Destroy(gameObject, m_DespawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ToolType PickupTool()
    {
        Destroy(gameObject);
        return m_ToolType;
    }
}
