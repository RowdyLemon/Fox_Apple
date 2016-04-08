using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public void go_back()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.START;
        Game_Manager.instance.scene_loaded = false;
    }
}
