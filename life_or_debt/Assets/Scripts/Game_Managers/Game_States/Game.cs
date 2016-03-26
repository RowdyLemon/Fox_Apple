using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{

    public GameObject top;
    public GameObject happiness;

	// Use this for initialization
	void Start ()
    {
        font_init();
        set_top_values();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void rest(int amount)
    {      
        day_passed(amount);

        Game_Manager.instance.Player.Happiness += 1;
        Game_Manager.instance.Player.Rested += 1;
        Game_Manager.instance.Player.Time_Played += amount;
    }

    public void entertainment(int amount, int cost, int happiness_change)
    {
        day_passed(amount);

        Game_Manager.instance.Player.Debt -= cost;
        Game_Manager.instance.Player.Happiness =+ happiness_change;
        Game_Manager.instance.Player.Time_Played += amount;

    }

    public void work(int amount)
    {
        day_passed(amount);

        Game_Manager.instance.Player.Promotion_Count++;
        promotion_check();
        Game_Manager.instance.Player.Debt += Game_Manager.instance.Player.Player_Job.Hourly_Wage * amount;
        Game_Manager.instance.Player.Happiness--;
        Game_Manager.instance.Player.Rested--;
        rested_check();
    }

    // Helper Functions

    /*
     *  Sets font size based on screen size of all text
     */

    private void rested_check()
    {
        if (Game_Manager.instance.Player.Rested < 5)
            Game_Manager.instance.Player.Promotion_Count--;
    }

    private void happiness_check()
    {
        if(Game_Manager.instance.Player.Happiness < 5)
        {
            Game_Manager.instance.Player.Rested--;
            rested_check();
        }
    }

    private void promotion_check()
    {
        if(Game_Manager.instance.Player.Promotion_Count == 1)
        {
            // Promotion
            float current_wage = Game_Manager.instance.Player.Player_Job.Hourly_Wage;
            float new_wage = current_wage * 1.1f;
            
            int res = (int)Math.Ceiling(current_wage * 1.1);
            Debug.Log(res);
            Game_Manager.instance.Player.Player_Job.Hourly_Wage = res;
            Debug.Log(Game_Manager.instance.Player.Player_Job.Hourly_Wage);


            Game_Manager.instance.Player.Promotion_Count = 0;
        }
    }

    private void day_passed(int amount)
    {
        if ((Game_Manager.instance.Player.Time_Played + amount) % 24 == 0)
            Game_Manager.instance.Player.Happiness--;
        // anything else we want to happen overnight
    }

    private void font_init()
    {
        top.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size * 2;
        top.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size * 2;
        top.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size * 2;
    }

    private void set_top_values()
    {
        top.GetComponentsInChildren<Text>()[0].text = Game_Manager.instance.Player.Name;
        top.GetComponentsInChildren<Text>()[2].text = Game_Manager.instance.Player.Debt.ToString("N0");
    }
}
