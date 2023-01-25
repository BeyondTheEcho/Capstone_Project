using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public abstract void OnInteract(Player player);
    public abstract string ReturnTextPrompt();

    /// <summary>
    /// Stores an instance of the interactable object in the Interactables Manager
    /// </summary>
    public void StoreRef()
    {
        InteractablesManager.s_Instance.AddInteractable(this);
    }

    /// <summary>
    /// Removes this instance of the interactable object in the Interactables Manager
    /// </summary>
    public void DeleteRef()
    {
        InteractablesManager.s_Instance.RemoveInteractable(this);
    }
}
