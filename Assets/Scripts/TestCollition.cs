using Unity.VisualScripting;
using UnityEngine;

public class TestCollition : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
    }
}
