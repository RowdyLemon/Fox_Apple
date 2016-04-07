using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Car_Selector : MonoBehaviour {

    public GameObject cheap;
    public GameObject medium;
    public GameObject expensive;
    public GameObject title;
    public GameObject cheap_loan;
    public GameObject medium_loan;
    public GameObject expensive_loan;


    void Start()
    {
        cheap.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        medium.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        expensive.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        title.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        cheap_loan.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
        medium_loan.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
        expensive_loan.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
    }

	// Use this for initialization
    public void Car_Selection()
    {
        string Car_Button = EventSystem.current.currentSelectedGameObject.name;
        Game_Manager.instance.Player.Player_Car.Car_Selection(Car_Button);

        Game_Manager.instance.current_state = Game_Manager.Game_States.MAIN;
        Game_Manager.instance.scene_loaded = false;
    }
}
