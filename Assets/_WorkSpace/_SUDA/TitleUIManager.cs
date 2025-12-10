using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button playButton;
    public Button explanationButton;

    private void Start()
    {
        playButton.onClick.AddListener(GoToPlayScene);
        explanationButton.onClick.AddListener(GoToExplanationScene);
    }

    private void GoToPlayScene()
    {
        SceneManager.LoadScene("MonoStage01");
    }

    private void GoToExplanationScene()
    {
        SceneManager.LoadScene("MonoExplanation01");
    }
}
