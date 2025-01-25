using System.Collections.Generic;
using UnityEngine;

public class GravityBubble : MonoBehaviour
{
    List<GravityTraveller> travellers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        travellers = new List<GravityTraveller>();

        // TODO: When this object is created, we need to do a raycast to check
        // for objects that are already inside the Gravity Bubble so we can add them to the list of travellers
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<GravityTraveller>() != null)
        {
            AddTraveller(other.GetComponentInParent<GravityTraveller>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<GravityTraveller>() != null)
        {
            RemoveTraveller(other.GetComponentInParent<GravityTraveller>());
        }
    }

    void AddTraveller(GravityTraveller traveller)
    {
        if (travellers.Contains(traveller))
        {
            // already in list, so doesn't matter
        }
        else
        {
            travellers.Add(traveller);
            traveller.SwapGravity(GravityState.Up);
        }
    }

    void RemoveTraveller(GravityTraveller traveller)
    {
        if (travellers.Contains(traveller))
        {
            travellers.Remove(traveller);
            traveller.SwapGravity(GravityState.Down);
        }
        // else, doesn't matter
    }

    private void OnDestroy()
    {
        foreach(var tvlr in travellers)
        {
            tvlr.SwapGravity(GravityState.Down);
        }

        travellers.Clear();
    }
}
