using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameClearUIManager : MonoBehaviour
{
    public Button playAgainButton;
    public Button backToTitleButton;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip buttonSE;

    [Header("‘JˆÚ‚Ü‚Å‚Ì‘Ò‚¿ŽžŠÔ")]
    public float waitTime = 0.3f; 

    private void Start()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            StartCoroutine(PlaySEAndLoad("MonoStage01"));
        });

        backToTitleButton.onClick.AddListener(() =>
        {
            StartCoroutine(PlaySEAndLoad("MonoTitle"));
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
