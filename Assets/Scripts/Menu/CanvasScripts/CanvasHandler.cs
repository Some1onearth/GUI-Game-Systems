using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
namespace CanvasExample //if the name of this script was the same as "Main Menu", you can place this namespace to separate the two.
{
    public class CanvasHandler : MonoBehaviour
    {
        public void ChangeSceneViaIndex(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }        
        public void ChangeSceneViaName(string sceneName) // This works the same as above but with names instead of scene numbers (eg. 1)
        {
            SceneManager.LoadScene(sceneName);
        }
        public void ExitTheGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }

}