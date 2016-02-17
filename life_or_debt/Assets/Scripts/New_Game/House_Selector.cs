using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class House_Selector : MonoBehaviour {


    public void House_Selection()
    {
        string House_Button = EventSystem.current.currentSelectedGameObject.name;
        Game_Manager.instance.Player.Player_House.House_Selection(House_Button);

        Game_Manager.instance.current_state = Game_Manager.Game_States.CAR_SELECTION;
        Game_Manager.instance.scene_loaded = false;
    }
}
