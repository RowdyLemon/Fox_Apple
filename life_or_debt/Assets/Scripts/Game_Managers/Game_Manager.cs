using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    public enum Game_States
    {
        START,
        CHOOSE_EDUCATION,
        CHOOSE_HOUSE,
        CHOOSE_JOB,
        NAME_SELECTION,
        CAR_SELECTION,
        PERSONALITY_QUIZ,
        LOSE_SCENE,
        WIN_SCENE,
        TUTORIAL_SCENE,
        MAIN
    };

    public static Game_Manager instance;
    public Game_States current_state;
    public Player Player;
    public string test;
    public bool scene_loaded;

    //GUI Variables
    private int font_size;



    //Getters and Setters
    public int Font_Size { get { return font_size; } }


	// Use this for initialization
	void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        current_state = Game_States.START;
        DontDestroyOnLoad(gameObject);
        scene_loaded = false;
        Init();
	}
	
    // Initialize GUI variables
    private void Init()
    {
        font_size = (int)(Screen.width * 0.018f);
    }

	// Update is called once per frame
	void Update ()
    {
	    switch(current_state)
        {
            case Game_States.START:
                if(!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Start_Scene");              
                }
                break;
            case Game_States.MAIN:
                if(!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Main_Scene");
                }
                break;
            case Game_States.CHOOSE_EDUCATION:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Education_Scene");
                }
                break;
            case Game_States.CHOOSE_JOB:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Job_Scene");
                }
                break;
            case Game_States.CHOOSE_HOUSE:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("House_Scene");
                }
                break;
            case Game_States.NAME_SELECTION:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Name_Selection_Scene");
                }
                break;
            case Game_States.CAR_SELECTION:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Car_Scene");
                }
                break;
            case Game_States.PERSONALITY_QUIZ:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Personality_Quiz_Scene");
                }
                break;
            case Game_States.LOSE_SCENE:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Lose_Screen");
                }
                break;
            case Game_States.WIN_SCENE:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Win_Screen");
                }
                break;
            case Game_States.TUTORIAL_SCENE:
                if (!scene_loaded)
                {
                    scene_loaded = true;
                    SceneManager.LoadScene("Tutorial_Scene");
                }
                break;
        }
	}
}
