using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Start_Screen : MonoBehaviour
{
    public GameObject play_button;


	// Use this for initialization
	void Start ()
    {
        play_button.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Game_Start()
    {
        Game_Manager.instance.Player = new Player();
        Game_Manager.instance.current_state = Game_Manager.Game_States.NAME_SELECTION;
        Game_Manager.instance.scene_loaded = false;
    }
}
