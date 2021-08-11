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
    [Header("Resolution")]
    public Resolution[] resolutions; //a Vector2 specifically for int
    public string[] resolutionName; //reminder: string is a list
    public bool showResOptions;
    public string resoDropDownLabel = "Resolution";
    public string fullsScreenToggleName = "Windowed";
    public Vector2 scrollPosition = Vector2.zero;

    private void Awake() //codes run in order or written unless it's a specified function (event trigger) like Awake
    {
        if(!GameObject.FindGameObjectWithTag("Music"))//Needed the ! in front of GameObject in order to activate when it ISN'T already playing
        {
            Instantiate(music);
        }
        #region Resolution
        //grab all the resoulations of our screen and add them to a list
        resolutions = Screen.resolutions;

        // set the size of our array of names to the length of our resolution array.
        resolutionName = new string[resolutions.Length];
        //for every resoulution create the display name
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionName[i] = resolutions[i].width + "x" + resolutions[i].height;
        }
        #endregion
        #region Keybinds

        #endregion
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
        //GUI.Box(new Rect(4 * scr.x, 2 * scr.y, 8 * scr.x, 2 * scr.y), "Title");

        //Options
        GUI.Box(new Rect(5 * scr.x, 1.25f* scr.y, 6 * scr.x, 0.5f * scr.y), "Options");
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
            if(GUI.Button(new Rect(4*scr.x+(i*2*scr.x), 0.75f*scr.y, 2*scr.x, 0.4f*scr.y),idList[i]))
            {
                currentOption = i;
                scrollPosition = Vector2.zero;
            }
        }
        switch (currentOption)
        {
            case 0:
                #region Audio
                audi.SetFloat("VolumeMaster", volumeMaster = GUI.HorizontalSlider(new Rect(6 * scr.x, 6 * scr.y, 2 * scr.x, 0.25f * scr.y), volumeMaster, -80, 20));

                audi.SetFloat("VolumeMusic", volumeMusic = GUI.HorizontalSlider(new Rect(6 * scr.x, 6.5f * scr.y, 2 * scr.x, 0.25f * scr.y), volumeMusic, -80, 20));

                audi.SetFloat("VolumeSFX", volumeSFX = GUI.HorizontalSlider(new Rect(6 * scr.x, 7 * scr.y, 2 * scr.x, 0.25f * scr.y), volumeSFX, -80, 20));
                #endregion

                //HOMEWORK CREATE A MUTE TOGGLE

                break;
            case 1:
                #region Resoultion Settings
                if (GUI.Button(new Rect(4*scr.x, 3*scr.y, 3*scr.x, 0.5f*scr.y),resoDropDownLabel))
                {
                    showResOptions = !showResOptions; //! = not
                    //if true becomes false
                    //if false become true
                }
                if (showResOptions)
                {
                    //create a background
                    GUI.Box(new Rect(4 * scr.x, 3.5f * scr.y, 3 * scr.x, 4 * scr.y), "");
                    //create a scroll view
                    scrollPosition = GUI.BeginScrollView(new Rect(4 * scr.x, 3.5f * scr.y, 3 * scr.x, 4 * scr.y), scrollPosition, new Rect(0,0,0,0.5f* scr.y * resolutions.Length), false, true);
                    //fill the scroll view with buttons
                    for (int i = 0; i < resolutions.Length; i++)
                    {
                        //every element creates a button according to our arrays
                        if (GUI.Button(new Rect(0,i*0.5f*scr.y,2.75f*scr.x,0.5f*scr.y), resolutionName[i]))
                        {
                            //set our resolution to the selected resolution
                            Screen.SetResolution(resolutions[i].width, resolutions[i].height, Screen.fullScreen);
                            //close dropdown
                            showResOptions = false;
                        }
                    }
                    //end scroll view
                    GUI.EndScrollView();//if missing then you get a pushing more clips error
                }
                #endregion
                break;

            case 2:

                break;

            case 3:

                break;
            default:
                currentOption = 0; //sends back to audio in case something breaks
                break;

        }
        #endregion


        if (GUI.Button(new Rect(5 * scr.x, 8 * scr.y, 2 * scr.x, 0.5f * scr.y), "Back"))
        {
            showOptions = false;
        }
    }
}
//scr.x - 1/16th 