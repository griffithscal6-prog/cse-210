namespace Word;
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }


    public void Reveal()
    {
        _isHidden = false;
    }


    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return "_____";
        }

        return _text;
    }
}
