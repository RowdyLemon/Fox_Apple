using UnityEngine;
using System.Collections;

public class Start_Screen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Game_Start()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.NAME_SELECTION;
        Game_Manager.instance.scene_loaded = false;
    }
}
