using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    public Button playButton;
    public Button explanationButton;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MonoStage01");
        });

        explanationButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MonoExplanation01");
        });
    }
}
