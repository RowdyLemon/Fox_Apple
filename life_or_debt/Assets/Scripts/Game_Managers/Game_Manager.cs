using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    public enum Game_States
    {
        START,
        MAIN
    }

    public static Game_Manager instance;
    public Game_States current_state;

    public bool scene_loaded;

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
        }
	}
}
