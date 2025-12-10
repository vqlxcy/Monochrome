using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeObjectClick : MonoBehaviour
{
    public static bool isPaused = false;

    private void OnMouseDown()
    {
        if (isPaused) return;

        switch (gameObject.name)
        {
            case "PlayObject":
                SceneManager.LoadScene("MonoStage01");
                break;

            case "ExplanationObject":
                SceneManager.LoadScene("MonoExplanation01");
                break;
        }
    }
}
