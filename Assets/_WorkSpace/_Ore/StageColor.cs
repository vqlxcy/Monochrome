using UnityEngine;

public class StageColor : MonoBehaviour
{
    [SerializeField,Header("”’‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _blackDeleteParent;
    [SerializeField,Header("•‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _whiteDeleteParent;
    [SerializeField, Header("—¼•û‚Å‘¶İ‚·‚é‚à‚Ì")]
    GameObject _monoParent;

    public int _colorControlNumber = 0;

    Color _monoParentColor;

    bool _white;
    bool _black;

    public void ColorChange()
    {
        _monoParentColor = _monoParent.GetComponentInChildren<SpriteRenderer>().color;
        switch(_colorControlNumber % 2)
        {
            case 0:
                _white = true;
                _monoParentColor = Color.black;
                _blackDeleteParent.SetActive(true);
                _whiteDeleteParent.SetActive(false);
            break;
            case 1:
                _black = true;
                _monoParentColor = Color.white;
                _blackDeleteParent.SetActive(false);
                _whiteDeleteParent.SetActive(true);
            break;
        }
    }
}
