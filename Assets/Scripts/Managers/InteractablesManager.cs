using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    private float m_BeltItemRange = 1.0f;
    private float m_BeltMoveSpeed = 0.5f;
    private float m_BeltItemDistancePadding = 1.0f;

    public static InteractablesManager s_Instance { get; private set; }
    public List<InteractableBase> m_InteractableObjects;
    public List<InteractableBase> m_TextPromptInteractables;
    public List<InteractableBase> m_Dropables;
    public List<Belts> m_ConveyorBelts;
    public List<InteractableBase> m_ObjectOnConveyors;
    private List<InteractableBase> m_TempObjectsOnConveyors;

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

    void Update()
    {
        MoveAllItemsOnConveyorBelts();
    }

    /// <summary>
    /// Returns the closest object that inherits from InteractableBase in the range provided as InteractableBase
    /// </summary>
    /// <param name="player">Instance of the player calling the method</param>
    /// <param name="pickupRange">Range the player can interact in</param>
    /// <returns></returns>
    public InteractableBase? GetClosestInteractableInRange(Player player, float interactRange)
    {
        return GetClosestFromCollection(player, interactRange, m_InteractableObjects);
    }

    public Belts? GetClosestBeltInRange(Player player, float interactRange)
    {
        return GetClosestFromCollection(player, interactRange, m_ConveyorBelts);
    }

    /// <summary>
    /// Returns the closest object that inherits from InteractableBase in the range provided that does not have the Belts script attached as InteractableBase
    /// </summary>
    /// <param name="player">Instance of the player calling the method</param>
    /// <param name="pickupRange">Range the player can interact in</param>
    /// <returns></returns>
    public InteractableBase? GetClosestPickupableInRange(Player player, float interactRange)
    {
        return GetClosestFromCollection(player, interactRange, m_Dropables);
    }

    private T GetClosestFromCollection<T>(Player player, float interactRange, IEnumerable<T> myList) where T : MonoBehaviour
    {
        return myList
            .Select(belt => (belt, (player.transform.position - belt.gameObject.transform.position).sqrMagnitude))
            .Where(tuple => tuple.sqrMagnitude < interactRange * interactRange)
            .OrderBy(tuple => tuple.sqrMagnitude)
            .Select(tuple => tuple.belt)
            .FirstOrDefault();
    }

    public void MoveAllItemsOnConveyorBelts()
    {
        //Guard
        if (m_ObjectOnConveyors.Count == 0)
        {
            return;
        }

        //Iterates through each item that is on a conveyor belt
        foreach (var item in m_ObjectOnConveyors)
        {
            float distance = float.MaxValue;
            Belts closestBelt = null;

            //Iterates through each conveyor belt object and checks the distance to find the closest belt to the item (ie the belt the item is on)
            foreach (var belt in m_ConveyorBelts)
            {
                float delta = (item.transform.position - belt.gameObject.transform.position).sqrMagnitude;

                if (delta < distance)
                {
                    closestBelt = belt;
                    distance = delta;
                }
            }

            //Copies the list of items on the conveyor belt and removes the current item in the current iteration of the foreach loop we are scope in
            m_TempObjectsOnConveyors = m_ObjectOnConveyors.ToList();
            m_TempObjectsOnConveyors.Remove(item);

            float distanceBetweenObjectsOnBelt = float.MaxValue;

            //Iterates through the list of all OTHER items on conveyors to find the closest one
            foreach (var itemTwo in m_TempObjectsOnConveyors)
            {
                float deltaBetweenObjectsOnBelt = (item.transform.position - itemTwo.gameObject.transform.position).sqrMagnitude;

                if (deltaBetweenObjectsOnBelt < distanceBetweenObjectsOnBelt)
                {
                    distanceBetweenObjectsOnBelt = deltaBetweenObjectsOnBelt;
                }
            }

            //Checks first to see if the next closest item on the belt is not too close then moves the item to the next belt in the chain
            if (distanceBetweenObjectsOnBelt > m_BeltItemDistancePadding * m_BeltItemDistancePadding)
            {
                //Checks that the distance between the item and the closest belt is not too far into the next belts range
                if (distance < m_BeltItemRange * m_BeltItemRange)
                {
                    item.transform.position = Vector3.MoveTowards(item.transform.position, closestBelt.m_NextWaypoint.transform.position, m_BeltMoveSpeed * Time.deltaTime);
                }
            }

            //Clears the list for the next iteration
            m_TempObjectsOnConveyors.Clear();
        }
    }

    public void AddInteractable(InteractableBase interactableObject)
    {
        m_InteractableObjects.Add(interactableObject);
    }

    public void RemoveInteractable(InteractableBase interactableObject)
    {
        m_InteractableObjects.Remove(interactableObject);
    }

    public void AddItemToBelt(InteractableBase interactableObject)
    {
        m_ObjectOnConveyors.Add(interactableObject);
    }

    public void RemoveItemFromBelt(InteractableBase interactableObject)
    {
        m_ObjectOnConveyors.Remove(interactableObject);
    }

    public void AddConveyorBelt(Belts belt)
    {
        m_ConveyorBelts.Add(belt);
    }

    public void RemoveConveyorBelt(Belts belt)
    {
        m_ConveyorBelts.Remove(belt);
    }

    public void AddIDropable(InteractableBase drop)
    {
        m_Dropables.Add(drop);
    }

    public void RemoveIDropable(InteractableBase drop)
    {
        m_Dropables.Remove(drop);
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
