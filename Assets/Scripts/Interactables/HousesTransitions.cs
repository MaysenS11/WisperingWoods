using Unity.Cinemachine;
using UnityEngine;

public class Houses : MonoBehaviour
{
    [SerializeField] private GameObject houseOutside;
    [SerializeField] private GameObject houseInside;
    [SerializeField] private BoxCollider2D triggerOutside;
    [SerializeField] private BoxCollider2D triggerInside;
    
    public CinemachineCamera cameraFixed;

    void Start()
    {
        triggerInside = houseInside.GetComponentInChildren<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject == houseOutside)
        {
            EnterHouse(other.gameObject);
        }
        else
        {
            LeaveHouse(other.gameObject);
        }
    }
    void EnterHouse(GameObject player)
    {
        houseInside.SetActive(true);
        player.transform.position = triggerInside.transform.position;
        Debug.Log(triggerInside.transform.position + " " + player.transform.position);
    }
    void LeaveHouse(GameObject player)
    {
        houseInside.SetActive(false);
        player.transform.position = triggerInside.transform.position;
    }
}
