using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StairsWalk : MonoBehaviour
{
    [SerializeField] private List<GameObject> Tilemaps;

    void Start()
    {
        new List<GameObject>(Tilemaps);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") == false) Debug.Log(other.tag);
        foreach (GameObject tilemap in Tilemaps)
        {
            tilemap.GetComponent<TilemapCollider2D>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        foreach (GameObject tilemap in Tilemaps)
        {
            tilemap.GetComponent<TilemapCollider2D>().enabled = true;
        }
    }
}
