using UnityEngine;
using System.Collections;

public class LavaInstantDeath : MonoBehaviour
{
    public Transform respawnPoint; // Respawn position
    public ScreenFade screenFade;  // Reference to fade effect
    public float sinkSpeed = 2f;   // How fast the player sinks
    public float fadeDuration = 1.5f; // How long the fade and sink last

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SinkAndFade(other.transform.root));
        }
    }

    private IEnumerator SinkAndFade(Transform playerRoot)
    {
        Rigidbody rb = playerRoot.GetComponent<Rigidbody>();

        // Disable player movement
        if (rb != null)
        {
            rb.isKinematic = true; // Prevents movement
        }

        float elapsedTime = 0f;
        Vector3 startPos = playerRoot.position;

        // Start fade while sinking
        StartCoroutine(screenFade.FadeToBlack());

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            playerRoot.position += Vector3.down * (sinkSpeed * Time.deltaTime);
            yield return null;
        }

        // Respawn player
        playerRoot.position = respawnPoint.position;

        // Wait a bit before fading back in
        yield return new WaitForSeconds(0.5f);

        // Fade back in
        yield return StartCoroutine(screenFade.FadeFromBlack());

        // Re-enable Rigidbody physics
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
