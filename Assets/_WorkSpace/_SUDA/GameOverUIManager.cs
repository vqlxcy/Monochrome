using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    public Button retryButton;
    public Button backToButton;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip buttonSE;

    private void Start()
    {
        retryButton.onClick.AddListener(() =>
        {
            PlaySE();
            SceneManager.LoadScene("MonoStage01");
        });

        backToButton.onClick.AddListener(() =>
        {
            PlaySE();
            SceneManager.LoadScene("MonoTitle");
        });
    }

    void PlaySE()
    {
        if (audioSource != null && buttonSE != null)
        {
            audioSource.PlayOneShot(buttonSE);
        }
    }
}
