using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{

    public GameObject top;
    public GameObject side;
    public GameObject bottom;
    public GameObject go_to_work;
    public GameObject go_out;
    public GameObject time;


    // Player bars
    public GameObject happiness_bar;
    public GameObject rest_bar;
    public GameObject job_bar;

    private Random_Event random_event = new Random_Event();

    private double bar_full; // player bars full width

    private int day;
    private int hour;

	// Use this for initialization
	void Start ()
    {
        font_init();
        set_top_values();
        set_side_values();
        bar_full = GameObject.Find("happiness_bar_empty").GetComponent<RectTransform>().rect.width;
        alter_bar_values();
        day = 0;
        hour = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void rest(int amount)
    {      
        day_passed(amount);

        Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + 10 > 100) ? 100 : Game_Manager.instance.Player.Happiness + 10;
        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested + 10 > 100) ? 100 : Game_Manager.instance.Player.Rested + 10;
    }

    public void entertainment(Entertainment_Param entertainment_param)
    {
        day_passed(entertainment_param.amount);
        Game_Manager.instance.Player.Debt -= entertainment_param.cost;
        Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + entertainment_param.happiness_change > 100) ? 100 : Game_Manager.instance.Player.Happiness + entertainment_param.happiness_change;
        Game_Manager.instance.Player.Time_Played += entertainment_param.amount;
        alter_bar_values();
    }

    public void work(int amount)
    {
        day_passed(amount);

        Game_Manager.instance.Player.Promotion_Count += 10;
        Game_Manager.instance.Player.Debt += Game_Manager.instance.Player.Player_Job.Hourly_Wage * amount;
        Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness - 10 <= 0) ? 0 : Game_Manager.instance.Player.Happiness - 5;
        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - 10 <= 0) ? 0 : Game_Manager.instance.Player.Rested - 10;


        promotion_check();
        rested_check();
    }

    // Helper Functions

    /*
     *  Sets font size based on screen size of all text
     */

    private void rested_check()
    {
        if (Game_Manager.instance.Player.Rested < 40)
            Game_Manager.instance.Player.Promotion_Count-=10;
    }

    private void happiness_check()
    {
        if(Game_Manager.instance.Player.Happiness < 50)
        {
            Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - 10 <= 0) ? 0 : Game_Manager.instance.Player.Rested - 10;
            rested_check();
        }
    }

    private void promotion_check()
    {
        int job_level = Game_Manager.instance.Player.Job_Level;

        if (job_level > 2)
            return;

        if(Game_Manager.instance.Player.Promotion_Count == 100)
        {
            // Promotion
            float current_wage = Game_Manager.instance.Player.Player_Job.Hourly_Wage;
            
            int res = (int)Math.Ceiling(current_wage * 1.1);
            Game_Manager.instance.Player.Player_Job.Hourly_Wage = res;

            if(job_level < 2)
            {
                Game_Manager.instance.Player.Player_Job.Promotion(Game_Manager.instance.Player.Player_Job.Job_Id);
                Game_Manager.instance.Player.Job_Level++;
            }
            Game_Manager.instance.Player.Promotion_Count = 0;
        }
    }

    private void day_passed(int amount)
    {
        Game_Manager.instance.Player.Time_Played += amount;
        hour = Game_Manager.instance.Player.Time_Played % 24;
        day = Game_Manager.instance.Player.Time_Played / 24;
        if (hour == 0)
        {
            time.GetComponentsInChildren<Text>()[2].text = day.ToString();
            time.GetComponentsInChildren<Text>()[3].text = "0";

            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness - 5 <= 0) ? 0 : Game_Manager.instance.Player.Happiness - 5;
            random_event.Execute_Event();
        }
        else
        {
            time.GetComponentsInChildren<Text>()[2].text = day.ToString();
            time.GetComponentsInChildren<Text>()[3].text = hour.ToString();
        }

        // anything else we want to happen overnight
    }

    private void font_init()
    {
        top.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size * 2;
        top.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size * 2;
        top.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size * 2;

        side.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        side.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        side.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;
        side.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size;
        side.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size;
        side.GetComponentsInChildren<Text>()[5].fontSize = Game_Manager.instance.Font_Size;
        side.GetComponentsInChildren<Text>()[6].fontSize = Game_Manager.instance.Font_Size;

        bottom.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        bottom.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;

        go_to_work.GetComponentInChildren<Text>().fontSize = Game_Manager.instance.Font_Size;
        go_out.GetComponentInChildren<Text>().fontSize = Game_Manager.instance.Font_Size;

        time.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        time.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        time.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;
        time.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size;
    }

    private void set_top_values()
    {
        top.GetComponentsInChildren<Text>()[0].text = Game_Manager.instance.Player.Name;
        top.GetComponentsInChildren<Text>()[2].text = Game_Manager.instance.Player.Debt.ToString("N0");
    }

    private void set_side_values()
    {
        side.GetComponentsInChildren<Text>()[4].text = Game_Manager.instance.Player.Player_Job.Name;
        side.GetComponentsInChildren<Text>()[6].text = Game_Manager.instance.Player.Player_Job.Hourly_Wage + "/hour";
    }

    private void alter_bar_values()
    {
        double h_bar = bar_full - (bar_full) * ((double)Game_Manager.instance.Player.Happiness / 100.0);
        happiness_bar.GetComponent<RectTransform>().sizeDelta = new Vector3(-(float)h_bar, 0f);
        if (Game_Manager.instance.Player.Happiness <= 25)
        {
            happiness_bar.GetComponentsInChildren<Image>()[0].color = new Color(255, 0, 0);
            happiness_bar.GetComponentsInChildren<Image>()[1].color = new Color(255, 0, 0);
            happiness_bar.GetComponentsInChildren<Image>()[2].color = new Color(255, 0, 0);
        }
        else if (Game_Manager.instance.Player.Happiness <= 60)
        {
            happiness_bar.GetComponentsInChildren<Image>()[0].color = new Color(255, 204, 0);
            happiness_bar.GetComponentsInChildren<Image>()[1].color = new Color(255, 204, 0);
            happiness_bar.GetComponentsInChildren<Image>()[2].color = new Color(255, 204, 0);
        }
        else
        {
            happiness_bar.GetComponentsInChildren<Image>()[0].color = new Color(0, 204, 0);
            happiness_bar.GetComponentsInChildren<Image>()[1].color = new Color(0, 204, 0);
            happiness_bar.GetComponentsInChildren<Image>()[2].color = new Color(0, 204, 0);
        }
        double r_bar = bar_full - (bar_full) * ((double)Game_Manager.instance.Player.Rested / 100.0);
        rest_bar.GetComponent<RectTransform>().sizeDelta = new Vector3(-(float)r_bar, 0f);
        if (Game_Manager.instance.Player.Rested <= 25)
        {
            rest_bar.GetComponentsInChildren<Image>()[0].color = new Color(255, 0, 0);
            rest_bar.GetComponentsInChildren<Image>()[1].color = new Color(255, 0, 0);
            rest_bar.GetComponentsInChildren<Image>()[2].color = new Color(255, 0, 0);
        }
        else if (Game_Manager.instance.Player.Rested <= 60)
        {
            rest_bar.GetComponentsInChildren<Image>()[0].color = new Color(255, 204, 0);
            rest_bar.GetComponentsInChildren<Image>()[1].color = new Color(255, 204, 0);
            rest_bar.GetComponentsInChildren<Image>()[2].color = new Color(255, 204, 0);
        }
        else
        {
            rest_bar.GetComponentsInChildren<Image>()[0].color = new Color(0, 204, 0);
            rest_bar.GetComponentsInChildren<Image>()[1].color = new Color(0, 204, 0);
            rest_bar.GetComponentsInChildren<Image>()[2].color = new Color(0, 204, 0);
        }
        double w_bar = bar_full - (bar_full) * ((double)Game_Manager.instance.Player.Promotion_Count / 100.0);
        job_bar.GetComponent<RectTransform>().sizeDelta = new Vector3(-(float)w_bar, 0f);
    }
}
