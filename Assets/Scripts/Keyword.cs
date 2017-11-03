using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Keyword
{
    public enum PDataType
    {
        STRING,
        INT,
        FLOAT,
    }
    
    [System.Serializable]
    public class ParameterInfo
    {
        public string paramDescription;
        public PDataType dataType;
    }


    public string keyString;
    public string wordDescription;
    public List<ParameterInfo> allParamInfo;


    public KeywordEvent OnConsoleSubmit;

    public bool CheckParameters(string[] args)
    {
        if(args.Length - 1 != allParamInfo.Count)
        {
            return false;
        }

        for(int i = 0; i < allParamInfo.Count; i++)
        {
            if (!CheckType(args[i + 1], allParamInfo[i].dataType))
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckType(string arg, PDataType dataType)
    {
        switch(dataType)
        {
            case PDataType.FLOAT:
            {
                float outFloat;
                return float.TryParse(arg, out outFloat);
            }

            case PDataType.INT:
            {
                int outInt;
                return int.TryParse(arg, out outInt);
            }
            case PDataType.STRING:
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class KeywordEvent : UnityEvent<string[]> { }

