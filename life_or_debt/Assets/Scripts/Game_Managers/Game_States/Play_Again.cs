using UnityEngine;
using System.Collections;

public class Play_Again : MonoBehaviour {

    public void play_again()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.START;
        Game_Manager.instance.scene_loaded = false;
    }
}
