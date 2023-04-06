using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Demon : MonoBehaviour
{
    [SerializeField] private float m_Speed = 1.0f;

    private Player m_Target;
    private bool m_IsMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        //change to false when done
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.activeSelf)
        {
            if (m_Target != null && m_Target.m_HeldItem != null) 
            { 
                if (!m_IsMoving)
                {
                    StartCoroutine(MoveDemon());
                }
            }
            else
            { 
                m_Target = PickPlayer();
            }
        }
    }

    IEnumerator MoveDemon()
    {
        m_IsMoving= true;
        transform.position = Vector3.MoveTowards(transform.position, m_Target.transform.position, m_Speed * Time.deltaTime);
        yield return new WaitForSeconds(0.01f);
        m_IsMoving= false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_Target.PlayerDrop();
        }
    }

    [CanBeNull]
    Player PickPlayer()
    {
        var pickedPlayer = FindObjectsOfType<Player>();

        foreach (var player in pickedPlayer)
        {
            if (player.m_HeldItem != null)
            {
                return player;
            }
        }
        return null;
    }
}
