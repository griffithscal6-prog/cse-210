namespace Word;
using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        string[] wordArray = text.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries
        );

        foreach (string word in wordArray)
        {
            _words.Add(new Word(word));
        }
    }

    
    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        List<int> visibleIndices = new List<int>();

        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
            {
                visibleIndices.Add(i);
            }
        }

        for (int i = 0; i < numberToHide && visibleIndices.Count > 0; i++)
        {
            int randomPosition = random.Next(visibleIndices.Count);
            int wordIndex = visibleIndices[randomPosition];

            _words[wordIndex].Hide();
            visibleIndices.RemoveAt(randomPosition);
        }
    }


    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }

        return true;
    }

    public string GetDisplayText()
    {
        string displayText = _reference.GetDisplayText() + " ";

        foreach (Word word in _words)
        {
            displayText += word.GetDisplayText() + " ";
        }

        return displayText.Trim();
    }


    public void RevealAllWords()
    {
        foreach (Word word in _words)
        {
            word.Reveal();
        }
    }

}
