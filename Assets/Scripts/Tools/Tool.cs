using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Tool : MonoBehaviour
{
    [SerializeField] private float m_DespawnTime = 30.0f;
    [SerializeField] private ToolType m_ToolType;

    void Awake()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.radius = 1;
        col.isTrigger = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_DespawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ToolType ReturnToolType()
    {
        return m_ToolType;
    }

    public void DestroyTool()
    {
        Destroy(gameObject);
    }
}
