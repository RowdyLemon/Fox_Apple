using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Naming : MonoBehaviour
{
    public void Name_Player()
    {
        GameObject Game_Obj = GameObject.Find("InputField");
        InputField iField = Game_Obj.GetComponent<InputField>();
        Game_Manager.instance.Player.Name = iField.text;
        Debug.Log(Game_Manager.instance.Player.Name);
        Game_Manager.instance.current_state = Game_Manager.Game_States.CHOOSE_EDUCATION;
        Game_Manager.instance.scene_loaded = false;
    }
}
