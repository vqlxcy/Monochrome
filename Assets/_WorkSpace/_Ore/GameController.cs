using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController instance { get; set; }


    StageColor _sc;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        _sc = GetComponent<StageColor>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MonoStage01")
        {
            _sc.ColorChange();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _sc._colorControlNumber++;
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
