using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//implement the interactable interface
public class NpcController : MonoBehaviour, Interactable
{
    public void Interact()   //create implementation of interaction
    {
        Debug.Log("Interacting with NPC!!!!!");
    }
}
