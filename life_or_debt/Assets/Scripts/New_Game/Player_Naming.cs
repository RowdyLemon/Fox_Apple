using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Naming : MonoBehaviour
{
    public GameObject choose_name;
    public GameObject name_text;
    public GameObject placeholder;

    void Start ()
    {
        choose_name.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        name_text.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        placeholder.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
    }

    public void Name_Player()
    {
        GameObject Game_Obj = GameObject.Find("InputField");
        InputField iField = Game_Obj.GetComponent<InputField>();
        Game_Manager.instance.Player.Name = iField.text;
        if(iField.text.ToLower() == "mike")
        {
            Game_Manager.instance.Player.Name = "Bitch";
        }
        Game_Manager.instance.current_state = Game_Manager.Game_States.PERSONALITY_QUIZ;
        Game_Manager.instance.scene_loaded = false;
    }
}
