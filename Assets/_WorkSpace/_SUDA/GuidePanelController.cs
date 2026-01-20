using UnityEngine;
using UnityEngine.SceneManagement;

public class GuidePanelController : MonoBehaviour
{
    [Header("各ステージの攻略パネル")]
    public GameObject stage01Panel;
    public GameObject stage02Panel;
    public GameObject stage03Panel;

    GameObject currentPanel;

    void Start()
    {
        if (stage01Panel != null) stage01Panel.SetActive(false);
        if (stage02Panel != null) stage02Panel.SetActive(false);
        if (stage03Panel != null) stage03Panel.SetActive(false);

        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MonoStage01":
                currentPanel = stage01Panel;
                break;
            case "MonoStage02":
                currentPanel = stage02Panel;
                break;
            case "MonoStage03":
                currentPanel = stage03Panel;
                break;
            default:
                currentPanel = null;
                break;
        }
    }

    void Update()
    {
        if (currentPanel == null) return;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            currentPanel.SetActive(!currentPanel.activeSelf);
        }
    }
}
