using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUIManager : MonoBehaviour
{
    public Button playAgainButton;
    public Button backToTitleButton;

    private void Start()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MonoStage01");
        });

        backToTitleButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MonoTitle");
        });
    }
}
