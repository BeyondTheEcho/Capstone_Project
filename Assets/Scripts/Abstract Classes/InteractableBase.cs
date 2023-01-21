using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public abstract void OnInteract(Player player);
    public abstract string ReturnTextPrompt();

    public void StoreRef()
    {
        InteractablesManager.s_Instance.AddInteractable(this);
    }

    public void DeleteRef()
    {
        InteractablesManager.s_Instance.RemoveInteractable(this);
    }


}
