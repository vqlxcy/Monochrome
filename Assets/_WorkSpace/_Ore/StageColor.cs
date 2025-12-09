using UnityEngine;

public class StageColor : MonoBehaviour
{
    int _colorControlNumber;

    bool _white;
    bool _black;

    void ColorChange()
    {
        switch(_colorControlNumber % 2)
        {
            case 0:
                _white = true;
            break;
            case 1:
                _black = true;
            break;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _colorControlNumber++;
        }
    }
}
