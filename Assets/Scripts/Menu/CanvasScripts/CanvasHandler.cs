using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace CanvasExample //if the name of this script was the same as "Main Menu", you can place this namespace to separate the two.
{
    public class CanvasHandler : MonoBehaviour
    {
        public AudioMixer masterAudio;
        public void ChangeMasterVolume(float volume)
        {
            masterAudio.SetFloat("VolumeMaster", volume);
        }
        public void ToggleMasterMute(bool isMuted)
        {
            if (isMuted)
            {
                masterAudio.SetFloat("VolumeMaster", -80);
            }
            else
            {
                masterAudio.SetFloat("VolumeMaster", 0);
            }
        }

        public void ChangeMusicVolume(float volume)
        {
            masterAudio.SetFloat("VolumeMusic", volume);
        }
        public void ToggleMusicMute(bool isMuted)
        {
            if (isMuted)
            {
                masterAudio.SetFloat("VolumeMusic", -80);
            }
            else
            {
                masterAudio.SetFloat("VolumeMusic", 0);
            }
        }
        public void ChangeSFXVolume(float volume)
        {
            masterAudio.SetFloat("VolumeSFX", volume);
        }
        public void ToggleSFXMute(bool isMuted)
        {
            if (isMuted)
            {
                masterAudio.SetFloat("VolumeSFX", -80);
            }
            else
            {
                masterAudio.SetFloat("VolumeSFX", 0);
            }
        }
        public void Quality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
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
        public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }
        public Resolution[] resolutions;
        public Dropdown resolution;

        private void Start()
        {
            resolutions = Screen.resolutions;
            resolution.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            resolution.AddOptions(options);
            resolution.value = currentResolutionIndex;
            resolution.RefreshShownValue();
        }
        public void SetResolution(int resolutionIndex)
        {
            Resolution res = resolutions[resolutionIndex];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }
    }
}