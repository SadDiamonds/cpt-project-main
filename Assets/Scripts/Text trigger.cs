using UnityEngine;
using TMPro; // Ensure TextMeshPro is included
using System.Collections; // Needed for IEnumerator and Coroutines

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject uiTextObject; // Assign UI Text in Inspector
    public string cutsceneText = "Part 1 - The Beginning...";
    public float textDisplayDuration = 5f; // Time in seconds to show the text before hiding it
    public float fadeDuration = 1f; // Duration for fade-in and fade-out

    private CanvasGroup canvasGroup;

    private void Start()
    {
        // Ensure the UI Text object has a CanvasGroup component attached for fade effect
        canvasGroup = uiTextObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = uiTextObject.AddComponent<CanvasGroup>(); // Add CanvasGroup if it's missing
        }

        // Initially hide the text
        uiTextObject.SetActive(false);
        canvasGroup.alpha = 0f; // Start with fully transparent text
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure Player has the right tag
        {
            if (uiTextObject != null)
            {
                uiTextObject.SetActive(true);
                TextMeshProUGUI textComponent = uiTextObject.GetComponent<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = cutsceneText; // Update the text
                }

                // Fade-in effect
                StartCoroutine(FadeIn());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optionally, you could start fading out immediately when the player leaves the trigger
            // StartCoroutine(FadeOut()); // This is optional
        }
    }

    private IEnumerator FadeIn()
    {
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure alpha is fully 1 after fade-in
        canvasGroup.alpha = 1f;

        // Wait for the display duration before starting fade-out
        yield return new WaitForSeconds(textDisplayDuration);

        // Fade-out effect
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure alpha is fully 0 after fade-out
        canvasGroup.alpha = 0f;
        uiTextObject.SetActive(false); // Hide the text after fade-out is complete
    }
}
