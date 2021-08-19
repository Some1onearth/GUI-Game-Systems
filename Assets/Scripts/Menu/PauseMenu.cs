using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseMenu : MonoBehaviour
{
#if UNITY_EDITOR
    [Serializable]
    public struct KeySetup
    {
        public string keyName;
        public string defaultKey;
        public string tempKey;
    }
    [Header("Keybinds")]
    public KeySetup[] keySetUp;
#endif
    void Start()
    {
#if UNITY_EDITOR
        //Set up for this scene the screen or scr reference for 16:9
        IMGUIScript.scr.x = Screen.width / 16;
        IMGUIScript.scr.y = Screen.width / 9;

        //if we don't have an entry in our key dictionary
        /*
        if (!IMGUIScript.inputKeys.ContainsKey("Forward"))
        {
            
        }
        if (IMGUIScript.inputKeys == null)
        {
            
        }
        These two above can work the same as below
        */
        if (IMGUIScript.inputKeys.Count <= 0) //Count is a dictionary's length
        {
            #region Keybinds
            //For loop to add the keys to the Dictionary with Save or Default depending on load
            for (int i = 0; i < keySetUp.Length; i++)//for all the keys in our base set up array
            {
                //add key according to the saved string or default
                IMGUIScript.inputKeys.Add(keySetUp[i].keyName, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keySetUp[i].keyName, keySetUp[i].defaultKey)));
            }
            #endregion
        }

        

        //loop through and set up our keys according to keySetUp
        //For loop to add the keys to the Dictionary with Save or Default depending on load
        //for all the keys in our base set up array
        //add key according to the save string or default



#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
