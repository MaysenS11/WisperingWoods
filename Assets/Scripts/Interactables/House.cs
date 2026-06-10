using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class Houses : MonoBehaviour
{
    public enum HouseTriggerType { OutsideEntry, InsideExit }

    [Header("Trigger Configuration")]
    public HouseTriggerType triggerType;
    
    [Header("References")]
    public GameObject houseInside;
    public Transform destinationSpawnPoint;
    public CinemachineCamera cameraFixed;
    public float fadeDuration = 0.4f;

    private static bool _isTransitioning = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isTransitioning || !other.CompareTag("Player")) return;
        StartCoroutine(TransitionSequence(other.gameObject));
    }

    private IEnumerator TransitionSequence(GameObject player)
    {
        _isTransitioning = true;

        var movement = player.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        yield return StartCoroutine(MenuManager.Instance.FadeRoutine(1f, fadeDuration));

        if (triggerType == HouseTriggerType.OutsideEntry)
        {
            if (houseInside != null) houseInside.SetActive(true);
            if (cameraFixed != null) cameraFixed.Priority = 20; 
        }
        else
        {
            if (houseInside != null) houseInside.SetActive(false);
            if (cameraFixed != null) cameraFixed.Priority = 0; 
        }

        if (destinationSpawnPoint != null)
        {
            player.transform.position = destinationSpawnPoint.position;
        }

        if (player.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.linearVelocity = Vector2.zero;
        }

        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(MenuManager.Instance.FadeRoutine(0f, fadeDuration));

        if (movement != null) movement.enabled = true;

        _isTransitioning = false;
    }
}