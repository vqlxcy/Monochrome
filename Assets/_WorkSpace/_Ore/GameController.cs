using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(StageColor))]
[RequireComponent(typeof(StageRotation))]
public class GameController : MonoBehaviour
{
    StageColor _sc;
    StageRotation _sr;
    Rigidbody2D[] _rb;
    Rigidbody2D _prb;

    [SerializeField]
    string _nextStage;
    [SerializeField, Header("色変更の回数制限")]
    int _colorControlLimit;
    [SerializeField, Header("ステージ回転の回数制限")]
    int _stageRotateLimit;
    [SerializeField]
    GameObject _player;
    [SerializeField]
    GameObject _goal;
    [SerializeField,Header("スイッチ")]
    GameObject _switch;
    [SerializeField,Header("Emptyいれてくれぇ")]
    public GameObject _decoy;
    [SerializeField, Header("取得できるコイン")]
    GameObject[] _goalCoins;
    [SerializeField, Header("取得できないコイン")]
    GameObject[] _interfaceCoins;
    [SerializeField]
    Transform[] _goalCoinPos;
    [SerializeField]
    Transform[] _interfaceCoinPos;
    [SerializeField,Header("重力を反転させるオブジェクト")]
    GameObject _gravityReverse;
    [SerializeField,Header("生存している敵のリスト")]
    public GameObject[] _enemys;

    List<GameObject> _useCoinList = new(); //取得できるコインをランダムに選出するときに使うリスト
    List<GameObject> _goalCoinList = new List<GameObject>(); //ランダムに並べ替えた取得できるコインを保存するリスト
    List<GameObject> _nonUseCoinList = new List<GameObject>(); //取得できるコインをランダムに選出するときに使うリスト
    List<GameObject> _interfaceCoinList = new List<GameObject>(); //ランダムに並べ替えた取得できないコインを保存するリスト
    public List<GameObject> _collisionCoinList = new List<GameObject>(); //インスタンス化された取得できるコインを保存するリスト
    List<GameObject> _nonCollisionCoinList = new List<GameObject>(); //インスタンス化された取得できないコインを保存するリスト


    int _stageNumber;
    int _randomInt;
    public int _livingEnemyControlNumber;
    Vector3 _savePlayerPosition;
    Vector3 _saveWhiteMovePosition;
    GameObject _whiteMoveBlock;

    public bool _isGoal;
    public bool _buttonSpawn = false;
    public bool _isButtonClicked = false;
    public bool _isPlayerDeath;
    public bool _isRotate = false;

    void Start()
    {
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");
        _sc = GetComponent<StageColor>();
        _sr = GetComponent<StageRotation>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _whiteMoveBlock = GameObject.FindGameObjectWithTag("WhiteMove");
        _prb = _player.GetComponent<Rigidbody2D>();

        _saveWhiteMovePosition = _whiteMoveBlock.transform.localPosition;
        _isGoal = false;
        _sc._colorControlCount = 0;

        for (int i = 0; i < _goalCoins.Length; i++)
        {
            _useCoinList.Add(_goalCoins[i]);
            _nonUseCoinList.Add(_interfaceCoins[i]);
        }

        if (SceneManager.GetActiveScene().name == "MonoStage01")
        {
            _stageNumber = 1;
        }
        else if (SceneManager.GetActiveScene().name == "MonoStage02")
        {
            _stageNumber = 2;
            RandomCoinSelect();
        }
        else if (SceneManager.GetActiveScene().name == "MonoStage03")
        {
            _stageNumber = 3;
            RandomCoinSelect();
        }
        else if (SceneManager.GetActiveScene().name == "MonoStage04")
        {
            _stageNumber = 4;
            RandomCoinSelect();
        }
        else
        {
            _stageNumber = 0;
        }

        if (_enemys.Length == 1 && _enemys[0] == _decoy)
        {
            _buttonSpawn = true;
        }

        _saveWhiteMovePosition = _whiteMoveBlock.transform.localPosition;
        _savePlayerPosition = _player.transform.position;

        for (int i = 0; i < _collisionCoinList.Count; i++)
        {
            _collisionCoinList[i].gameObject.SetActive(false);
            _nonCollisionCoinList[i].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (_sc._colorControlNumber % 2 == 0)
        {
            _whiteMoveBlock.transform.localPosition = _saveWhiteMovePosition;
        }

        if (Input.GetKeyDown(KeyCode.Return) && _sc._colorControlCount <= _colorControlLimit)
        {
            _sc._colorControlNumber++;
            _sc._colorControlCount++;
            if (_sc._colorControlNumber % 2 == 0)
            {
                _saveWhiteMovePosition = _whiteMoveBlock.transform.localPosition;
                _player.transform.position = _savePlayerPosition;
                _goal.SetActive(false);
            }
            else if (_sc._colorControlNumber % 2 == 1)
            {
                _savePlayerPosition = _player.transform.position;
                _goal.SetActive(true);
            }

            _sc.BlockColorChange();
            _sc.BGColorChange();
        }

        if (_isGoal)
        {
            SceneManager.LoadScene(_nextStage);
        }

        if (_stageNumber >= 2)
        {
            if (_collisionCoinList.Count == 0)
            {
                _isGoal = true;
            }

            if (_buttonSpawn)
            {
                _switch.SetActive(true);
            }
            else
            {
                _switch.SetActive(false);
            }

            if (_sc._colorControlNumber >= 1 && _nonCollisionCoinList[2] == true)
            {
                for (int i = 0; i < _nonCollisionCoinList.Count; i++)
                {
                    _nonCollisionCoinList[i].SetActive(false);
                }
            }

            if (_isButtonClicked && _sc._colorControlNumber % 2 == 0)
            {
                for (int i = 0; i < _collisionCoinList.Count; i++)
                {
                    _collisionCoinList[i].SetActive(true);
                }
            }

            if (_isGoal)
            {
                SceneManager.LoadScene(_nextStage);
            }

            if (_stageNumber >= 3)
            {
                if (Input.GetKeyDown(KeyCode.R) && _sr._stageRotateCount <= _stageRotateLimit)
                {
                    _rb = _gravityReverse.GetComponentsInChildren<Rigidbody2D>();
                    for (int i = 0; i < _rb.Length; i++)
                    {
                        _rb[i].gravityScale *= -1;
                        Debug.Log("2" + _rb[i].gravityScale, _rb[i]);
                    }
                    _prb.gravityScale *= -1;
                    _sr.StageRotate();
                    _sr._stageRotateCount++;
                    _isRotate = !_isRotate;
                }
            }
        }
    }

    void RandomCoinSelect()
    {
        #region
        //https://www.pandanoir.info/entry/2013/03/04/193704
        //本当はFisher–Yatesアルゴリズムを使ったほうがいい(やってることはほぼ一緒)
        #endregion
        _randomInt = Random.Range(0, _useCoinList.Count);
        _goalCoinList.Add(_useCoinList[_randomInt]);
        _interfaceCoinList.Add(_nonUseCoinList[_randomInt]);
        _useCoinList.RemoveAt(_randomInt);
        _nonUseCoinList.RemoveAt(_randomInt);
        if (_useCoinList.Count != 0)
        {
            RandomCoinSelect();
        }
        else
        {
            for (int i = 0; i < _goalCoins.Length; i++)
            {
                GameObject obj = Instantiate(_interfaceCoinList[i], _interfaceCoinPos[i].position, Quaternion.identity);
                GameObject goalCoins = Instantiate(_goalCoinList[i], _goalCoinPos[i].position, Quaternion.identity);
                _collisionCoinList.Add(goalCoins);
                _nonCollisionCoinList.Add(obj);
            }
        }
    }
}
