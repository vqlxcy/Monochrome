using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplanationBackButton : MonoBehaviour
{
    [Header("クリックSE")]
    public AudioClip clickSE;

    [Header("戻るシーン名")]
    public string backSceneName = "MonoTitle";

    public void OnClickBackButton()
    {
        if (clickSE != null && MonoAudioManager.Instance != null)
        {
            var audio = MonoAudioManager.Instance.bgmSource;
            if (audio != null)
            {
                audio.PlayOneShot(clickSE);
            }
        }

        SceneManager.LoadScene(backSceneName);
    }
}
