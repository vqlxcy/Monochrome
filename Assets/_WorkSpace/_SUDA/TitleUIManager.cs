using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleUIManager : MonoBehaviour
{
    public Button playButton;
    public Button explanationButton;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip buttonSE;

    [Header("‘JˆÚ‚Ü‚Å‚Ì‘Ò‚¿ŽžŠÔ")]
    public float waitTime = 0.3f; 

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            StartCoroutine(PlaySEAndLoad("MonoStage01"));
        });

        explanationButton.onClick.AddListener(() =>
        {
            StartCoroutine(PlaySEAndLoad("MonoExplanation01"));
        });
    }

    IEnumerator PlaySEAndLoad(string sceneName)
    {
        if (audioSource != null && buttonSE != null)
        {
            audioSource.PlayOneShot(buttonSE);
        }

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(sceneName);
    }
}
