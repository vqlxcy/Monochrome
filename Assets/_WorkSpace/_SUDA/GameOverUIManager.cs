using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverUIManager : MonoBehaviour
{
    public Button retryButton;
    public Button backToButton;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip buttonSE;

    [Header("ëJà⁄Ç‹Ç≈ÇÃë“Çøéûä‘")]
    public float waitTime = 0.3f; 

    private void Start()
    {
        retryButton.onClick.AddListener(() =>
        {
            StartCoroutine(PlaySEAndLoad("MonoStage01"));
        });

        backToButton.onClick.AddListener(() =>
        {
            StartCoroutine(PlaySEAndLoad("MonoTitle"));
        });
    }

    IEnumerator PlaySEAndLoad(string sceneName)
    {
        // SEçƒê∂
        if (audioSource != null && buttonSE != null)
        {
            audioSource.PlayOneShot(buttonSE);
        }

        yield return new WaitForSeconds(waitTime);
        
        SceneManager.LoadScene(sceneName);
    }
}
