using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    public Button playButton;
    public Button explanationButton;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip buttonSE;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            PlaySE();
            SceneManager.LoadScene("MonoStage01");
        });

        explanationButton.onClick.AddListener(() =>
        {
            PlaySE();
            SceneManager.LoadScene("MonoExplanation01");
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
