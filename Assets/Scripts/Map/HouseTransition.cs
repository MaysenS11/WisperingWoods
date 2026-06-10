using System.Collections;
using UnityEngine;

public class HouseTransition : MonoBehaviour
{
    public float fadeDurationSeconds = 0.4f;
    public GameObject houseInside;
    public Transform spawnPointInside;
    private bool _isTransitioning;

    public void EnterHouse(GameObject player)
    {
        if (_isTransitioning) return;
        StartCoroutine(TransitionSequence(player));
    }

    private IEnumerator TransitionSequence(GameObject player)
    {
        _isTransitioning = true;

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        yield return StartCoroutine(MenuManager.Instance.FadeRoutine(1f, fadeDurationSeconds));

        houseInside.SetActive(true);
        player.transform.position = spawnPointInside.position;

        if (player.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.linearVelocity = Vector2.zero;
        }

        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(MenuManager.Instance.FadeRoutine(0f, fadeDurationSeconds));

        if (movement != null) movement.enabled = true;

        _isTransitioning = false;
    }
}
