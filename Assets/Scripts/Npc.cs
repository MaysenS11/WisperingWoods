using System;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
    }
}
