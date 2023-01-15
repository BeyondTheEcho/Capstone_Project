using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ToolType? m_CurrentTool = null;

    void OnCollisionStay2D(Collision2D col)
    {
        if (!Input.GetKeyDown(KeyCode.F))
        {
            return;
        }

        if (TryGetComponent(out Tool tool))
        {
            m_CurrentTool = tool.PickupTool();
        }
    }

    void DropTool()
    {

    }

    public ToolType? GetCurrentTool()
    {
        return m_CurrentTool;
    }
}
