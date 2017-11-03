using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConsoleTextFunction
{ 
    public static string ColorText(string text, Color color)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
    }
}
