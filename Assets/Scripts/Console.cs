using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    [Header("Text Info")]
    [SerializeField] private Text consoleTextUI;
    [SerializeField][TextArea] private string consoleText;
    [SerializeField] private string consoleCommand;

    [Header("Console Info")]
    [SerializeField] private KeyCode submitKey;
    [SerializeField] private KeyCode deleteKey;

    [Space]
    [SerializeField] private float tickRate = 1f;
    private float tickTime;
    private bool indicatorVisible;
    private char indicatorChar = '_';
    private char wordDelimeter = ' ';

    [Header("Console Commands")]
    [SerializeField] private string invalidText;
    [SerializeField] private string invalidSyntax;

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
                    keyWord.OnConsoleSubmit.Invoke(commandWords);
                }
                else
                {
                    consoleText += "\n" + invalidText;
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
        foreach (string arg in args)
            Debug.Log(arg);
    }

    public void Clear(string[] args)
    {

    }
}
