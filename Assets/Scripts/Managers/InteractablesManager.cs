using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InteractablesManager : MonoBehaviour
{
    public static InteractablesManager s_Instance { get; private set; }

    public List<InteractableBase> m_InteractableObjects;
    public List<InteractableBase> m_TextPromptInteractables;

    private void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(this);
        }
        else
        {
            s_Instance = this;
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public InteractableBase? ReturnClosestInteractableInRange(Player player, float pickupRange)
    {
        if (m_InteractableObjects.Count == 0)
        {
            return null;
        }

        float distance = float.MaxValue;
        InteractableBase closestObject = null;

        foreach (var interactable in m_InteractableObjects)
        {
            float delta = (player.transform.position - interactable.gameObject.transform.position).sqrMagnitude;

            if (delta < distance)
            {
                closestObject = interactable;
                distance = delta;
            }
        }

        if (distance > pickupRange)
        {
            return null;
        }

        return closestObject;
    }

    public void AddInteractable(InteractableBase interactableObject)
    {
        m_InteractableObjects.Add(interactableObject);
    }

    public void RemoveInteractable(InteractableBase interactableObject)
    {
        InteractableBase toDelete = m_InteractableObjects.Find(x => x == interactableObject);
        m_InteractableObjects.Remove(toDelete);
    }

    public void AddPromptObject(Player player, InteractableBase promptObject)
    {
        m_TextPromptInteractables.Add(promptObject);

        UpdateInteractableTextPrompt(player, promptObject);
    }

    public void RemovePromptObject(Player player, InteractableBase promptObject)
    {
        InteractableBase toDelete = m_TextPromptInteractables.Find(x => x == promptObject);
        m_TextPromptInteractables.Remove(toDelete);

        UpdateInteractableTextPrompt(player, promptObject);
    }

    public void UpdateInteractableTextPrompt(Player player, InteractableBase promptObject)
    {
        if (m_TextPromptInteractables.Count == 0)
        {
            player.UpdatePrompt("");
            return;
        }

        float distance = float.MaxValue;
        InteractableBase closestObject = null;

        foreach (var textItem in m_TextPromptInteractables)
        {
            float delta = (player.transform.position - textItem.gameObject.transform.position).sqrMagnitude;

            if (delta < distance)
            {
                closestObject = textItem;
                distance = delta;
            }
        }

        player.UpdatePrompt(closestObject.ReturnTextPrompt());
    }  
}
