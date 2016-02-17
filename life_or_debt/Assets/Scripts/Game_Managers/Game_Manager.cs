﻿using UnityEngine;
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
        MAIN
    };

    public static Game_Manager instance;
    public Game_States current_state;
    public Player Player;
    public string test;
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
        Player = new Player();
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
        }
	}
}