using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Job_Generator : MonoBehaviour
{
    GameObject Congrats_Text;
    public void Awake()
    {
        Congrats_Text = GameObject.Find("Congrats");
        Congrats_Text.SetActive(false);
    }

    public void Job_Selection()
    {
        // Hide click me button after pressed
        GameObject Game_Obj = GameObject.Find("Click_Me_Button");
        Game_Obj.SetActive(false);

        // Display congrats, set players job values, then display their job
        Congrats_Text.SetActive(true);
        GameObject Job_String = GameObject.Find("Job_Title");
        Text Player_Job_Text = Job_String.GetComponent<Text>();
        Game_Manager.instance.Player.Player_Job.Job_Choice(Game_Manager.instance.Player.Degree);
        Player_Job_Text.text = Game_Manager.instance.Player.Player_Job.Name;

        // Display hourly wage
        GameObject Hourly_Wage = GameObject.Find("Hourly_Wage");
        Text Player_Wage = Hourly_Wage.GetComponent<Text>();
        Player_Wage.text = "$" + Game_Manager.instance.Player.Player_Job.Hourly_Wage.ToString() + "/hr";
        
        // Display Continue button
        GameObject Continue_Button = GameObject.Find("Text");
        Text Continue_Text = Continue_Button.GetComponent<Text>();
        Continue_Text.text = "Continue >";
    }

    public void Continue()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.CHOOSE_HOUSE;
        Game_Manager.instance.scene_loaded = false;
    }
}
