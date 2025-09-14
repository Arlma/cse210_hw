using System;

public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text ?? "";
        _isHidden = false;
    }

    // Hide the word (letters become underscores, punctuation remains)
    public void Hide()
    {
        _isHidden = true;
    }

    // Returns true if the word is currently hidden
    public bool IsHidden => _isHidden;

    // Return either the original text or the hidden representation
    public string GetDisplayText()
    {
        if (!_isHidden)
        {
            return _text;
        }

        // Replace letters with underscores but keep punctuation and whitespace
        var chars = _text.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (Char.IsLetter(chars[i]))
            {
                chars[i] = '_';
            }
        }
        return new string(chars);
    }
}
