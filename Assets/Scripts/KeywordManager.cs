using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordManager : MonoBehaviour
{
    public static KeywordManager instance;

    [SerializeField] private List<Keyword> allKeyWords;

    public List<Keyword> AllKeyWords
    {
        get { return allKeyWords; }
    }

    private void Awake()
    {
        instance = this;
    }

    public bool CheckKeyword(string command, out Keyword keyWord)
    {
        if (allKeyWords.Count > 0)
        {
            foreach(Keyword word in allKeyWords)
            {
                if(command.ToLower() == word.keyString)
                {
                    keyWord = word;
                    return true;
                }
            }
        }
        keyWord = null; 
        return false;
    }

    public Keyword GetKeyword(string wordString)
    {
        if (allKeyWords.Count > 0)
        {
            foreach (Keyword word in allKeyWords)
            {
                if (wordString == word.keyString)
                {
                    return word;
                }
            }
        }
        return null;
    }
}
