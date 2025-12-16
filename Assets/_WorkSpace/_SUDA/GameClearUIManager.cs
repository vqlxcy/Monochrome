using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUIManager : MonoBehaviour
{
    public Button playAgainButton;
    public Button backToTitleButton;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip buttonSE;

    private void Start()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            PlaySE();
            SceneManager.LoadScene("MonoStage01");
        });

        backToTitleButton.onClick.AddListener(() =>
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
