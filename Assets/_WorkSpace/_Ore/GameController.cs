using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(StageColor))]
[RequireComponent(typeof(StageRotation))]
public class GameController : MonoBehaviour
{
    static GameController instance { get; set; }

    StageColor _sc;
    StageRotation _sr;

    [SerializeField]
    string _firstStage;
    [SerializeField]
    string _secondStage;
    [SerializeField]
    string _thirdStage;
    [SerializeField]
    GameObject _player;
    [SerializeField]
    GameObject _firstStageGoalInstanceButton;

    int _colorControlLimit;
    int _stageNumber;
    Vector3 _savePlayerPosition;
    GameObject _whiteMoveBlock;

    public bool _isGoal;
    public bool _isButtonClicked = false;
    public bool _isPlayerDeath;

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
        _sr = GetComponent<StageRotation>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (_stageNumber >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Return) && _sc._colorControlCount <= _colorControlLimit)
            {
                _sc._colorControlNumber++;

                _sc.BlockColorChange();
                _sc.BGColorChange();
                _sc._colorControlCount++;
            }

            if (_isGoal)
            {
                SceneManager.LoadScene(_secondStage);
            }

            if (_stageNumber >= 2)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _sr.StageRotate();
                }

                if (_isButtonClicked)
                {
                    _firstStageGoalInstanceButton.SetActive(true);
                }
                else
                {
                    _firstStageGoalInstanceButton.SetActive(false);
                }

                if (_isGoal)
                {
                    SceneManager.LoadScene(_thirdStage); ;
                }
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _isGoal = false;
        _player = GameObject.FindGameObjectWithTag("Player");
        _whiteMoveBlock = GameObject.FindGameObjectWithTag("WhiteMove");
        if (SceneManager.GetActiveScene().name == _firstStage)
        {
            _stageNumber = 1;
        }
        else if (SceneManager.GetActiveScene().name == _secondStage)
        {
            _stageNumber = 2;
        }
        else if (SceneManager.GetActiveScene().name == _thirdStage)
        {
            _stageNumber = 3;
        }
        else
        {
            _stageNumber = 0;
        }
    }
}
