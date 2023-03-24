using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBin : InteractableBase
{
    [SerializeField] GameObject m_OutputItem;

    // Start is called before the first frame update
    void Start()
    {
        StoreRef();
    }

    public override void OnInteract(Player player)
    {
        if (player.m_HeldItem == null)
        {
            var vial = Instantiate(m_OutputItem);

            vial.GetComponent<Vials>().OnInteract(player);
        }
    }

    public override string ReturnTextPrompt()
    {
        return LanguageSettings.s_Instance.GetLocalizedString("Retrieve");
    }

    private void OnDestroy()
    {
        DeleteRef();
    }
}
