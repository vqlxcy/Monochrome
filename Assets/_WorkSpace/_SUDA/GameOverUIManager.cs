using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    public Button retryButton;
    public Button backToButton;

    private void Start()
    {
        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MonoStage01");
        });

        backToButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MonoTitle");
        });
    }
}
