using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class IMGUIScript : MonoBehaviour
{
    public Vector2 scr; //in place of the "layout" public code. No need for that with this
    
    
    public struct Layout
    {
        public float Horizontal;
        public float Vertical;
    }
    [Header("Screen Display")]
    public Layout screen;
    public bool showOptions;
    [Header ("Audio")]
    [Tooltip ("Reference to Unity's Audio Mixer")]
    public AudioMixer audi;
    public float volumeMaster, volumeMusic, volumeSFX;
    [Tooltip ("Reference to Audio Source prefab")]
    public GameObject music;
    [Header("Options Tabs")]
    public string[] idList;
    public int currentOption;

    private void Awake()
    {
        if(!GameObject.FindGameObjectWithTag("Music"))//Needed the ! in front of GameObject in order to activate when it ISN'T already playing
        {
            Instantiate(music);
        }
    }
    private void OnGUI()//runs per frame same as update...
    {
        //screen width and height is broken up into 16 by 9 sections in a grid
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        Grid();
        if(showOptions) //if show options is true
        {
            //display our options
            OptionsScreen();
        }
        else//if show options is false
        {
            //Display our menu
            MenuLayout();
        }
        
        //OptionsScreen();
    }

    void Grid()
    {
        //Aspect Ratio 16:9
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                //im a GUI Element
                //type BOx
                // new Poz x,y new Size, x,y
                //my content
                GUI.Box(new Rect(x*scr.x,y*scr.y,scr.x,scr.y), "");
                
                //below shows the grid count in numbers. Remove comment to show at corner
                //GUI.Label(new Rect(x * scr.x, y * scr.y, scr.x, scr.y), x+"."+y);
            }
        }
    }
    void MenuLayout()
    {

        //Background
        GUI.Box(new Rect(1*scr.x, 0*scr.y, 14*scr.x, 9*scr.y), "Background");

        //Title
        GUI.Box(new Rect(4*scr.x, 2*scr.y, 8*scr.x, 2*scr.y), "Title");
        
        //Play
        if(GUI.Button(new Rect(7*scr.x, 6*scr.y, 2*scr.x, 0.5f*scr.y), "Play"))
        {
            //this button allows us to start the game
            //changes scenes
            SceneManager.LoadScene(1);
        }
        
        //Options
        if(GUI.Button(new Rect(7*scr.x, 7*scr.y, 2*scr.x, 0.5f*scr.y), "Options"))
        {
            showOptions = true;
        }
        
        //Exit
        if(GUI.Button(new Rect(7*scr.x, 8*scr.y, 2*scr.x, 0.5f*scr.y), "Exit"))
        {
            #if UNITY_EDITOR //developer code only in unity doesn't build into the game
            UnityEditor.EditorApplication.isPlaying = false; //makes unity look like it is quitting
            #endif
            Application.Quit(); //quits applications
            Debug.Log("Exit");
        }
    }
    void OptionsScreen()
    {
        //this whole part is essentially a drop down panel as a new "screen" without loading a new scene.

        GUI.Box(new Rect(1 * scr.x, 0 * scr.y, 14 * scr.x, 9 * scr.y), "Background");

        //Title
        GUI.Box(new Rect(4 * scr.x, 2 * scr.y, 8 * scr.x, 2 * scr.y), "Title");

        //Options
        GUI.Box(new Rect(5 * scr.x, 4 * scr.y, 6 * scr.x, 0.5f * scr.y), "Options");
        #region Forloop Buttons
        /*
         
        For - iterates a set number of times
            - needs to know its size or amount of iterations

        int i = 0; this part is creating an iteration reference variable
                   we can start at any number we want...standard is 0 for counter up
                   or the size for counting down
            i < 5; this is the amount of iterations we can reach...this is our size or amount
                   Counting up we say < our max value
                   Counting down we say > 0
             i++ - go to next iteration - Count up (eg for (int i = 0; i < 5; i++)
             i-- - go to the next iteration - Count Down eg (for (int i = 5; i > 0; i--)
             i = identification number (buttonIndexNumber for class)
        */

        for (int i = 0; i < idList.Length; i++)
        {
            if(GUI.Button(new Rect(scr.x, scr.y, scr.x, scr.y),idList[i]))
            {

            }
        }
        switch (currentOption)
        {
            case 0:

                break;
            case 1:

                break;

            case 2:

                break;

            case 3:

                break;
            default:
                break;

        }
        #endregion

        #region Audio
        audi.SetFloat("VolumeMaster", volumeMaster = GUI.HorizontalSlider(new Rect(6 * scr.x, 6 * scr.y, 2 * scr.x, 0.25f * scr.y), volumeMaster, -80, 20));

        audi.SetFloat("VolumeMusic", volumeMusic = GUI.HorizontalSlider(new Rect(6 * scr.x, 6.5f * scr.y, 2 * scr.x, 0.25f * scr.y), volumeMusic, -80, 20));

        audi.SetFloat("VolumeSFX", volumeSFX = GUI.HorizontalSlider(new Rect(6 * scr.x, 7 * scr.y, 2 * scr.x, 0.25f * scr.y), volumeSFX, -80, 20));
        #endregion

        if (GUI.Button(new Rect(5 * scr.x, 8 * scr.y, 2 * scr.x, 0.5f * scr.y), "Back"))
        {
            showOptions = false;
        }
    }
}
//scr.x - 1/16th 