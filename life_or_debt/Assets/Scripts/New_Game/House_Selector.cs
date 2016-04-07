using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class House_Selector : MonoBehaviour {

    public GameObject small;
    public GameObject medium;
    public GameObject large;
    public GameObject title;
    public GameObject cheap_loan;
    public GameObject medium_loan;
    public GameObject expensive_loan;

    void Start()
    {
        small.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        medium.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        large.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        title.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        cheap_loan.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
        medium_loan.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
        expensive_loan.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
    }

    public void House_Selection()
    {
        string House_Button = EventSystem.current.currentSelectedGameObject.name;
        Game_Manager.instance.Player.Player_House.House_Selection(House_Button);

        Game_Manager.instance.current_state = Game_Manager.Game_States.CAR_SELECTION;
        Game_Manager.instance.scene_loaded = false;
    }
}
