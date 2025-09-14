using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private static Random _random = new Random(); // single random so repeated calls vary

    // Scripture accepts a Reference and the verse text as a single string.
    // It is responsible for splitting the text into Word objects (encapsulation).
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        if (!string.IsNullOrWhiteSpace(text))
        {
            // Split on spaces and keep tokens (simple split is fine for this project)
            var tokens = text.Split(' ');
            foreach (var token in tokens)
            {
                _words.Add(new Word(token));
            }
        }
    }

    // Hide a small number of random visible words. Default hides 3 each call.
    // Scripture uses Word.Hide() internally so Word handles its own state.
    public void HideRandomWords(int count = 3)
    {
        if (count <= 0) return;

        var visibleWords = _words.Where(w => !w.IsHidden).ToList();
        if (!visibleWords.Any()) return;

        int hideCount = Math.Min(count, visibleWords.Count);

        for (int i = 0; i < hideCount; i++)
        {
            // Recompute visible list each iteration to avoid trying to hide the same word twice
            var stillVisible = _words.Where(w => !w.IsHidden).ToList();
            if (!stillVisible.Any()) break;

            int index = _random.Next(stillVisible.Count);
            stillVisible[index].Hide();
        }
    }

    // Return display text: reference followed by the words (which generate their own display)
    public string GetDisplayText()
    {
        var wordsText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()} - {wordsText}";
    }

    // True when every word is hidden
    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden);
    }

    // Expose words count if caller needs it (read-only)
    public int WordCount => _words.Count;
}
