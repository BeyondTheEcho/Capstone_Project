using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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

    /// <summary>
    /// Returns the closest object that inherits from InteractableBase in the range provided as InteractableBase
    /// </summary>
    /// <param name="player">Instance of the player calling the method</param>
    /// <param name="pickupRange">Range the player can interact in</param>
    /// <returns></returns>
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

        if (distance > pickupRange * pickupRange)
        {
            return null;
        }

        return closestObject;
    }

    /// <summary>
    /// Returns the closest object that inherits from InteractableBase in the range provided that does not have the Belts script attached as InteractableBase
    /// </summary>
    /// <param name="player">Instance of the player calling the method</param>
    /// <param name="pickupRange">Range the player can interact in</param>
    /// <returns></returns>
    public InteractableBase? ReturnClosestPickupableInRange(Player player, float pickupRange)
    {
        if (m_InteractableObjects.Count == 0)
        {
            return null;
        }

        List<InteractableBase> pickupables = new List<InteractableBase>();
        float distance = float.MaxValue;
        InteractableBase closestObject = null;

        foreach (var interactable in m_InteractableObjects)
        {
            if (!interactable.TryGetComponent(out Belts belts))
            {
                pickupables.Add(interactable);
            }
        }

        foreach (var pickupable in pickupables)
        {
            float delta = (player.transform.position - pickupable.gameObject.transform.position).sqrMagnitude;

            if (delta < distance)
            {
                closestObject = pickupable;
                distance = delta;
            }
        }

        if (distance > pickupRange * pickupRange)
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
        m_InteractableObjects.Remove(interactableObject);
    }

    public void AddPromptObject(Player player, InteractableBase promptObject)
    {
        m_TextPromptInteractables.Add(promptObject);

        UpdateInteractableTextPrompt(player, promptObject);
    }

    public void RemovePromptObject(Player player, InteractableBase promptObject)
    {
        m_TextPromptInteractables.Remove(promptObject);

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
