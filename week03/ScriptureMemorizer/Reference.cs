public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse; // if equal to _startVerse or 0, treated as single verse

    // Constructor supports single verse or verse range (set endVerse = 0 or same as start for single)
    public Reference(string book, int chapter, int startVerse, int endVerse = 0)
    {
        _book = book ?? "";
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse == 0 ? startVerse : endVerse;
    }

    // Encapsulated display logic for references
    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}
