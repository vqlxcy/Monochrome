using UnityEngine;

public class StageColor : MonoBehaviour
{
    [SerializeField, Header("”’‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _blackDeleteParent;
    [SerializeField, Header("•‚Ì¢ŠE‚Ì‚É‚¾‚¯Œ©‚¦‚é‚à‚Ì")]
    GameObject _whiteDeleteParent;
    [SerializeField, Header("—¼•û‚Å‘¶İ‚·‚é‚à‚Ì")]
    GameObject _monoParent;
    [SerializeField, Header("—¼•û‚Ì¢ŠE‚Å‘¶İ‚·‚é‚à‚Ì‚ÌŒ©‚½–Ú(”’‚Ì)")]
    Sprite _blackBrock;
    [SerializeField, Header("—¼•û‚Ì¢ŠE‚Å‘¶İ‚·‚é‚à‚Ì‚ÌŒ©‚½–Ú(•‚Ì)")]
    Sprite _whiteBrock;

    public int _colorControlNumber = 0;

    SpriteRenderer[] _monoParentColor;

    bool _white;
    bool _black;

    public void ColorChange()
    {
        _monoParentColor = _monoParent.GetComponentsInChildren<SpriteRenderer>();
        switch(_colorControlNumber % 2)
        {
            case 0:
                _white = true;
                for (int i = 0; i < _monoParentColor.Length; i++)
                {
                    _monoParentColor[i].sprite = _blackBrock;
                }
                _blackDeleteParent.SetActive(true);
                _whiteDeleteParent.SetActive(false);
            break;
            case 1:
                _black = true;
                for (int i = 0; i < _monoParentColor.Length; i++)
                {
                    _monoParentColor[i].sprite = _whiteBrock;
                }
                _blackDeleteParent.SetActive(false);
                _whiteDeleteParent.SetActive(true);
            break;
        }
    }
}
