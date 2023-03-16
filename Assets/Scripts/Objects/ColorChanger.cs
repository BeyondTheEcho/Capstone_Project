using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : InteractableBase
{
    private void Start()
    {
        StoreRef();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnInteract(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override string ReturnTextPrompt()
    {
        throw new System.NotImplementedException();
    }

    public enum MachineColor
    {
        Red,
        Green,
        Blue
    }

    private void OnDestroy()
    {
        DeleteRef();
    }
}
