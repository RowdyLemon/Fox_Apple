using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Car_Selector : MonoBehaviour {

	// Use this for initialization
    public void Car_Selection()
    {
        string Car_Button = EventSystem.current.currentSelectedGameObject.name;
        Game_Manager.instance.Player.Player_Car.Car_Selection(Car_Button);

        Game_Manager.instance.current_state = Game_Manager.Game_States.MAIN;
        Game_Manager.instance.scene_loaded = false;
    }
}
