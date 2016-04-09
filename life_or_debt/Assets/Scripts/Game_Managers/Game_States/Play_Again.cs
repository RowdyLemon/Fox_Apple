using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play_Again : MonoBehaviour {

    public GameObject win;
    public GameObject button;

    void Start()
    {
        win.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        button.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
    }

    public void play_again()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.START;
        Game_Manager.instance.scene_loaded = false;
    }
}
