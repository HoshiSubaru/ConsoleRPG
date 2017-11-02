using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public class InputKey
    {
        public string keyName;
        public KeyCode keyCode;
        
    }

    public List<InputKey> allKeyCodes;

    public KeyCode attackKey;
    
    public bool GetKeyDownAttack()
    {
        return Input.GetKey(attackKey);
    }

    private void Update()
    {

    }
}
