using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public static Console instance;

    [Header("Text Info")]
    [SerializeField] private Text consoleTextUI;
    [SerializeField][TextArea] private string consoleText;
    [SerializeField] private string consoleCommand;

    [Header("Console Keys")]
    [SerializeField] private KeyCode submitKey;
    [SerializeField] private KeyCode deleteKey;

    [Header("Console Info")]
    [SerializeField] private float tickRate = 1f;
    private float tickTime;
    private bool indicatorVisible;
    private char indicatorChar = '_';
    private char wordDelimeter = ' ';

    [Header("Console Commands")]
    [SerializeField] private Color errorColor;
    [SerializeField] private Color helpColor;

    [SerializeField] private string invalidText;
    [SerializeField] private string invalidSyntax;

    [SerializeField] private int commandsPerPage = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tickTime = 0f;
        consoleTextUI.text = "";
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(submitKey))
            {
                consoleText += (consoleText.Length > 0) ? "\n" + consoleCommand : consoleCommand;

                string[] commandWords = consoleCommand.Split(wordDelimeter);
                Keyword keyWord = null;

                if (commandWords.Length > 0 && KeywordManager.instance.CheckKeyword(commandWords[0], out keyWord))
                {
                    if(keyWord.CheckParameters(commandWords))
                    {
                        keyWord.OnConsoleSubmit.Invoke(commandWords);
                    }
                    else
                    {
                        string errorText = invalidSyntax + "\n" + keyWord.keyString;

                        if (keyWord.allParamInfo.Count > 0)
                        {
                            foreach (Keyword.ParameterInfo info in keyWord.allParamInfo)
                            {
                                errorText += wordDelimeter + "<" + info.paramDescription + ">";
                            }
                        }

                        PrintLine(ConsoleTextFunction.ColorText(errorText, errorColor));
                    }
                }
                else
                {
                    PrintLine(ConsoleTextFunction.ColorText(invalidText, errorColor));
                }
                consoleCommand = "";

                return;
            }
            else if (Input.GetKeyDown(deleteKey))
            {
                if(consoleCommand.Length > 0)
                    consoleCommand = consoleCommand.Remove(consoleCommand.Length - 1);
            }
            else
            {
                consoleCommand += Input.inputString;
            }
        }

        
        DisplayConsole();
    }

    private void DisplayConsole()
    {
        tickTime += Time.deltaTime;

        if (tickTime > tickRate)
        {
            tickTime = 0;
            indicatorVisible = !indicatorVisible;
        }

        string finalText = (consoleText.Length > 0) ? consoleText + "\n> " + consoleCommand : "> " + consoleCommand;
        consoleTextUI.text = (indicatorVisible) ? finalText + indicatorChar : finalText;
    }

    public void Help(string[] args)
    {
        int pageNo = int.Parse(args[1]);

        List<Keyword> allKeyWords = KeywordManager.instance.AllKeyWords;
        int totalPages = (int) Math.Ceiling((allKeyWords.Count / (decimal)commandsPerPage));

        PrintLine(ConsoleTextFunction.ColorText("---help command : page " + pageNo + " of " + totalPages + "---", helpColor));
        
        int curPageCommandNo = commandsPerPage * (1 - (pageNo / totalPages))
            + (allKeyWords.Count - (commandsPerPage * (totalPages - 1))) * (pageNo / totalPages);
        
        for (int i = 0; i < curPageCommandNo; i++)
        {
            Keyword curKeyWord = allKeyWords[ ((pageNo - 1) * commandsPerPage) + i];
            string paramText = curKeyWord.keyString;

            if (curKeyWord.allParamInfo.Count > 0)
            {
                foreach (Keyword.ParameterInfo paramInfo in curKeyWord.allParamInfo)
                {
                    paramText += wordDelimeter + "<" + paramInfo.paramDescription + ">";
                }
            }
            PrintLine(paramText + " |\t\t" + curKeyWord.wordDescription);
        }
    }

    public void ClearConsole(string[] args)
    {
        consoleText = "";
    }

    public static void PrintLine(string text)
    {
        Console.instance.consoleText += "\n" + text;
    }

    public static void Print(string text)
    {
        Console.instance.consoleText += text;
    }
}
