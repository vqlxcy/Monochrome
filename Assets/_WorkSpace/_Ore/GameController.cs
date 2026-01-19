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
    [SerializeField,Header("色変更の回数制限(ステージ毎)")]
    int _firstStageColorControlLimit;
    [SerializeField]
    int _secondStageColorControlLimit;
    [SerializeField]
    int _thirdStageColorControlLimit;
    [SerializeField, Header("ステージ回転の回数制限(ステージ毎)")]
    int _firstStageRotateLimit;
    [SerializeField]
    int _secondStageRotateLimit;
    [SerializeField]
    int _thirdStageRotateLimit;
    [SerializeField]
    GameObject _player;
    [SerializeField]
    GameObject _firstStageGoalInstanceButton;

    int _colorControlLimit;
    int _stageRotateLimit;
    int _stageNumber;
    Vector3 _savePlayerPosition;
    Vector3 _saveWhiteMovePosition;
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
            if (_sc._colorControlNumber % 2 == 0)
            {
                _whiteMoveBlock.transform.position = _saveWhiteMovePosition;
            }

            if (Input.GetKeyDown(KeyCode.Return) && _sc._colorControlCount <= _colorControlLimit)
            {
                if (_sc._colorControlNumber % 2 == 0)
                {
                    _savePlayerPosition = _player.transform.position;
                }
                else if (_sc._colorControlNumber % 2 == 1)
                {
                    _player.transform.position = _savePlayerPosition;
                    _saveWhiteMovePosition = _whiteMoveBlock.transform.position;
                }
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
                if (Input.GetKeyDown(KeyCode.R) && _sr._stageRotateCount <= _stageRotateLimit)
                {
                    _sr.StageRotate();
                    _sr._stageRotateCount++;
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
        _sc._colorControlCount = 0;
        _player = GameObject.FindGameObjectWithTag("Player");
        _whiteMoveBlock = GameObject.FindGameObjectWithTag("WhiteMove");

        _saveWhiteMovePosition = _whiteMoveBlock.transform.position;
        _savePlayerPosition = _player.transform.position;

        if (SceneManager.GetActiveScene().name == _firstStage)
        {
            _stageNumber = 1;
            _colorControlLimit = _firstStageColorControlLimit;
            _stageRotateLimit = _firstStageRotateLimit;
        }
        else if (SceneManager.GetActiveScene().name == _secondStage)
        {
            _stageNumber = 2;
            _colorControlLimit = _secondStageColorControlLimit;
            _stageRotateLimit = _secondStageRotateLimit;
        }
        else if (SceneManager.GetActiveScene().name == _thirdStage)
        {
            _stageNumber = 3;
            _colorControlLimit = _thirdStageColorControlLimit;
            _secondStageRotateLimit = _thirdStageRotateLimit;
        }
        else
        {
            _stageNumber = 0;
        }
    }
}
