using UnityEngine;

public class StageRotation : MonoBehaviour
{
    [SerializeField]
    Transform _stage;
    [SerializeField]
    int _rotationAngle;

    public int _stageRotateCount;

    public void StageRotate()
    {
        _stage.eulerAngles = new Vector3(0, 0, _rotationAngle);
    }
}
