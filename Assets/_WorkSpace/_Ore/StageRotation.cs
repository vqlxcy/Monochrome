using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class StageRotation : MonoBehaviour
{
    [SerializeField]
    Transform _stage;
    [SerializeField]
    int _rotationAngle;
    [SerializeField]
    GameObject _player;

    public int _stageRotateCount;

    public void StageRotate()
    {
        _stage.eulerAngles = new Vector3(0, 0, _rotationAngle);
        _player.transform.eulerAngles = new Vector3(0, 0, _rotationAngle);
    }
}
