﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{

    public GameObject top;
    public GameObject side;
    public GameObject go_to_work;
    public GameObject go_out;
    public GameObject time;
    public GameObject bank_tab;
    public GameObject store_tab;
    public GameObject store_tab2;
    public GameObject work_or_fun_tab;
    public GameObject modal_panel;
    public GameObject fridge_tab;

    public GameObject player_house;

    public GameObject Min_Student;
    public GameObject Min_House;
    public GameObject Min_Car;
    public GameObject Checking_Account;
    public GameObject Life_Event;

    private Vector3 off_screen;
    private Vector3 on_screen;
    private Vector3 modal_on_screen;
    private Vector3 fridge_on_screen;
    private Vector2 gold_fish_on_screen;
    private Vector3 life_event_on_screen;

    private Vector2 house_on_screen;

    private Vector2 s_check_location;
    private Vector2 h_check_location;
    private Vector2 c_check_location;

    // Different Houses
    public GameObject small_house;
    public GameObject medium_house;
    public GameObject large_house;

    // Player bars
    public GameObject happiness_bar;
    public GameObject rest_bar;
    public GameObject job_bar;
    public GameObject health_bar;

    private Random_Event random_event = new Random_Event();

    private double bar_full; // player bars full width

    private int day;
    private int hour;

    private double health_change = Game_Manager.instance.Player.Player_Traits.Mood;
    private double happiness_change = Game_Manager.instance.Player.Player_Traits.Mood;
    private double work_rate = Game_Manager.instance.Player.Player_Traits.Work_Speed;
    private double rest_change = Game_Manager.instance.Player.Player_Traits.Rest_Rate;

    // Bank Variables
    public GameObject s_inc;
    public GameObject s_dec;
    public GameObject h_inc;
    public GameObject h_dec;
    public GameObject c_inc;
    public GameObject c_dec;
    public GameObject s_check;
    public GameObject h_check;
    public GameObject c_check;
    private int student_payment;
    private int house_payment;
    private int car_payment;

    private bool monthly_car_payment;
    private bool monthly_house_payment;
    private bool monthly_student_loan_payment;
    private bool same_day;


    private bool gold_fish_purchased = false;
    private bool television_purchased = false;
    private bool couch_purchased = false;

    // Use this for initialization
    void Start()
    {
        font_init();
        set_top_values();
        set_side_values();
        bar_full = GameObject.Find("happiness_bar_empty").GetComponent<RectTransform>().rect.width;
        happiness_bar.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        rest_bar.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        job_bar.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        health_bar.GetComponent<RectTransform>().pivot = new Vector2(0, 0);

        // Load Sound
        GameObject sound = GameObject.Find("GameManager");
        sound.GetComponent<AudioSource>().clip = Resources.Load("LevelSong", typeof(AudioClip)) as AudioClip;
        sound.GetComponent<AudioSource>().Play();

        if (Game_Manager.instance.Player.Player_House.Min_Payment == 800)
        {
            player_house.GetComponent<Image>().sprite = Resources.Load("Sprites/small_house", typeof(Sprite)) as Sprite;
            small_house.GetComponentsInChildren<Image>()[2].sprite = Resources.Load("Sprites/UI/Car/" + Game_Manager.instance.Player.Player_Car.Name, typeof(Sprite)) as Sprite;
        }
        else if (Game_Manager.instance.Player.Player_House.Min_Payment == 1200)
        {
            player_house.GetComponent<Image>().sprite = Resources.Load("Sprites/Medium_House", typeof(Sprite)) as Sprite;
            medium_house.GetComponentsInChildren<Image>()[2].sprite = Resources.Load("Sprites/UI/Car/" + Game_Manager.instance.Player.Player_Car.Name, typeof(Sprite)) as Sprite;
        }
        else
        {
            player_house.GetComponent<Image>().sprite = Resources.Load("Sprites/Large_House", typeof(Sprite)) as Sprite;
            large_house.GetComponentsInChildren<Image>()[2].sprite = Resources.Load("Sprites/UI/Car/" + Game_Manager.instance.Player.Player_Car.Name, typeof(Sprite)) as Sprite;
        }

        alter_bar_values();
        day = 0;
        hour = 0;
        monthly_car_payment = false;
        monthly_house_payment = false;
        monthly_student_loan_payment = false;
        same_day = false;
        off_screen = new Vector3(1000, 1000, 0);
        on_screen = bank_tab.transform.localPosition;
        Min_Car.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
        Min_House.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
        Min_Student.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;
        Min_Car.GetComponent<Text>().text = "Min: " + Game_Manager.instance.Player.Player_Car.Min_Payment;
        Min_House.GetComponent<Text>().text = "Min: " + Game_Manager.instance.Player.Player_House.Min_Payment;
        if(Game_Manager.instance.Player.Degree.Equals("Bachelor's"))
            Min_Student.GetComponent<Text>().text = "Min: " + 150;
        else if (Game_Manager.instance.Player.Degree.Equals("Master's"))
            Min_Student.GetComponent<Text>().text = "Min: " + 250;
        else
            Min_Student.GetComponent<Text>().text = "Min: " + 350;   

        // Per House
        small_house.GetComponentsInChildren<Image>()[3].enabled = false;
        small_house.GetComponentsInChildren<Image>()[4].enabled = false;
        small_house.GetComponentsInChildren<Image>()[5].enabled = false;
        small_house.GetComponentsInChildren<Image>()[6].enabled = false;

        medium_house.GetComponentsInChildren<Image>()[3].enabled = false;
        medium_house.GetComponentsInChildren<Image>()[4].enabled = false;
        medium_house.GetComponentsInChildren<Image>()[5].enabled = false;
        medium_house.GetComponentsInChildren<Image>()[6].enabled = false;

        large_house.GetComponentsInChildren<Image>()[3].enabled = false;
        large_house.GetComponentsInChildren<Image>()[4].enabled = false;
        large_house.GetComponentsInChildren<Image>()[5].enabled = false;
        large_house.GetComponentsInChildren<Image>()[6].enabled = false;

        life_event_on_screen = Life_Event.transform.localPosition;
        modal_on_screen = modal_panel.transform.localPosition;
        c_check_location = c_check.transform.localPosition;
        h_check_location = h_check.transform.localPosition;
        s_check_location = s_check.transform.localPosition;


        Life_Event.transform.localPosition = off_screen;
        c_check.transform.localPosition = off_screen;
        h_check.transform.localPosition = off_screen;
        s_check.transform.localPosition = off_screen;

        bank_tab.transform.localPosition = off_screen;
        modal_panel.transform.localPosition = off_screen;
        fridge_tab.transform.localPosition = off_screen;
        store_tab.transform.localPosition = off_screen;
        store_tab2.transform.localPosition = off_screen;
        work_or_fun_tab.transform.localPosition = off_screen;

        // Per House
        if(Game_Manager.instance.Player.Player_House.Min_Payment == 800)
        {
            medium_house.transform.localPosition = off_screen;
            large_house.transform.localPosition = off_screen;
        }
        else if(Game_Manager.instance.Player.Player_House.Min_Payment == 1200)
        {
            small_house.transform.localPosition = off_screen;
            large_house.transform.localPosition = off_screen;
        }
        else
        {
            small_house.transform.localPosition = off_screen;
            medium_house.transform.localPosition = off_screen;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void rest(Sleeping_Param sleeping_param)
    {
        int rest_time = (int)(rest_change * sleeping_param.rested_gain);
        int health_val = (int)(health_change * 5);

        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested + rest_time > 100) ? 100 : Game_Manager.instance.Player.Rested + rest_time;
        Game_Manager.instance.Player.Health = (Game_Manager.instance.Player.Health - health_val <= 0) ? 0 : Game_Manager.instance.Player.Health - health_val;
        Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + sleeping_param.entertainment_gain > 100) ? 100 : Game_Manager.instance.Player.Happiness + sleeping_param.entertainment_gain;

        health_check();
        day_passed(sleeping_param.sleep_length);
        alter_bar_values();
    }

    public void entertainment(Entertainment_Param entertainment_param)
    {
       

        int happiness_val = (int)(happiness_change * entertainment_param.happiness_change);
        int rest_val = (int)(rest_change * 5);

        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - rest_val <= 0) ? 0 : Game_Manager.instance.Player.Rested - rest_val;
        Game_Manager.instance.Player.Checking_Account -= entertainment_param.cost;
        Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + happiness_val > 100) ? 100 : Game_Manager.instance.Player.Happiness + happiness_val;
        Game_Manager.instance.Player.Time_Played += entertainment_param.amount;
        day_passed(entertainment_param.amount);
        alter_bar_values();
        set_top_values();
        set_side_values();

        if (!checking_account_check(entertainment_param.cost))
        {
            work_or_fun_tab.GetComponentsInChildren<Button>()[2].interactable = false;
            return;
        }
    }

    public void work(int amount)
    {
        int work_val = (int)(work_rate * 10);
        int happiness_val = (int)(happiness_change * 5);
        int health_val = (int)(health_change * 10);
        int rest_val = (int)(rest_change * 10);

        Game_Manager.instance.Player.Promotion_Count = (Game_Manager.instance.Player.Job_Level > 2) ? 100 : Game_Manager.instance.Player.Promotion_Count + work_val;
        Game_Manager.instance.Player.Checking_Account += Game_Manager.instance.Player.Player_Job.Hourly_Wage * amount;
        Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness - happiness_val <= 0) ? 0 : Game_Manager.instance.Player.Happiness - happiness_val;
        Game_Manager.instance.Player.Health = (Game_Manager.instance.Player.Health - health_val <= 0) ? 0 : Game_Manager.instance.Player.Health - health_val;
        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - rest_val <= 0) ? 0 : Game_Manager.instance.Player.Rested - rest_val;

        promotion_check();
        rested_check();
        happiness_check();
        health_check();
        day_passed(amount);
        alter_bar_values();
        set_top_values();
        set_side_values();
    }

    public void eat(Eating_Param eating_param)
    {
        food_check(eating_param.health_gain);

        int health_val = (int)(health_change * eating_param.health_gain);
        int rest_val = (int)(rest_change * 5);

        Game_Manager.instance.Player.Health = (Game_Manager.instance.Player.Health + health_val >= 100) ? 100 : Game_Manager.instance.Player.Health + health_val;
        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - rest_val <= 0) ? 0 : Game_Manager.instance.Player.Rested - rest_val;

        if(Game_Manager.instance.Player.Rested == 0)
            Game_Manager.instance.Player.Health = (Game_Manager.instance.Player.Health - 20 <= 0) ? 0 : Game_Manager.instance.Player.Health - 20;

        health_check();
        day_passed(eating_param.amount);
        alter_bar_values();
        set_top_values();
    }

    public void open_bank()
    {
        modal_panel.transform.localPosition = modal_on_screen;
        bank_tab.transform.localPosition = on_screen;

        bank_tab.GetComponentsInChildren<Text>()[1].text = "$" + Game_Manager.instance.Player.Checking_Account.ToString("N0");
        bank_tab.GetComponentsInChildren<Text>()[10].text = "$" + Game_Manager.instance.Player.Student_Loan.ToString("N0");
        bank_tab.GetComponentsInChildren<Text>()[11].text = "$" + Game_Manager.instance.Player.House_Loan.ToString("N0");
        bank_tab.GetComponentsInChildren<Text>()[12].text = "$" + Game_Manager.instance.Player.Car_Loan.ToString("N0");

        student_payment = 0;
        house_payment = 0;
        car_payment = 0;

        bank_tab.GetComponentsInChildren<Text>()[5].text = "$0";
        bank_tab.GetComponentsInChildren<Text>()[6].text = "$0";
        bank_tab.GetComponentsInChildren<Text>()[8].text = "$0";

        bank_tab.GetComponentsInChildren<Button>()[0].interactable = false;
        bank_tab.GetComponentsInChildren<Button>()[3].interactable = false;
        bank_tab.GetComponentsInChildren<Button>()[5].interactable = false;

        if (monthly_car_payment)
            c_check.transform.localPosition = c_check_location;
        if (monthly_house_payment)
            h_check.transform.localPosition = h_check_location;
        if (monthly_student_loan_payment)
            s_check.transform.localPosition = s_check_location;


        if(Game_Manager.instance.Player.Degree == "Bachelor's")
        {
            if(Game_Manager.instance.Player.Checking_Account < 150)
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = true;
            }
        }
        else if(Game_Manager.instance.Player.Degree == "Master's")
        {
            if(Game_Manager.instance.Player.Checking_Account < 250)
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = true;
            }
        }
        else if(Game_Manager.instance.Player.Degree == "Doctorate")
        {
            if(Game_Manager.instance.Player.Checking_Account < 350)
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = true;
            }
        }

        if(Game_Manager.instance.Player.Player_House.Neighborhood == "Sketchy")
        {
            if(Game_Manager.instance.Player.Checking_Account < 800)
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = true;
            }
        }
        else if(Game_Manager.instance.Player.Player_House.Neighborhood == "Decent")
        {
            if(Game_Manager.instance.Player.Checking_Account < 1200)
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = true;
            }
        }
        else if(Game_Manager.instance.Player.Player_House.Neighborhood == "Prime")
        {
            if(Game_Manager.instance.Player.Checking_Account < 2500)
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = true;
            }
        }

        if(Game_Manager.instance.Player.Player_Car.Name == "pos_car")
        {
            if(Game_Manager.instance.Player.Checking_Account < 200)
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = true;
            }
        }
        else if(Game_Manager.instance.Player.Player_Car.Name == "decent")
        {
            if(Game_Manager.instance.Player.Checking_Account < 300)
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = true;
            }
        }
        else if(Game_Manager.instance.Player.Player_Car.Name == "nice_car")
        {
            if(Game_Manager.instance.Player.Checking_Account < 500)
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = true;
            }
        }

        loan_paid_off();
    }

    public void close_bank()
    {
        modal_panel.transform.localPosition = off_screen;
        bank_tab.transform.localPosition = off_screen;
    }

    public void open_store()
    {
        modal_panel.transform.localPosition = modal_on_screen;
        store_tab.transform.localPosition = on_screen;
        store_tab2.transform.localPosition = off_screen;

        if(Game_Manager.instance.Player.Checking_Account - 25 < 0)
            store_tab.GetComponentsInChildren<Button>()[1].interactable = false;
        else
            store_tab.GetComponentsInChildren<Button>()[1].interactable = true;


        if (Game_Manager.instance.Player.Checking_Account - 50 < 0)
            store_tab.GetComponentsInChildren<Button>()[2].interactable = false;
        else 
            store_tab.GetComponentsInChildren<Button>()[2].interactable = true;


        if (Game_Manager.instance.Player.Checking_Account - 100 < 0)
            store_tab.GetComponentsInChildren<Button>()[3].interactable = false;
        else
            store_tab.GetComponentsInChildren<Button>()[3].interactable = true;
    }

    public void open_store_tab_2()
    {
        store_tab.transform.localPosition = off_screen;
        store_tab2.transform.localPosition = on_screen;

        if (Game_Manager.instance.Player.Checking_Account - 25 < 0 || gold_fish_purchased)
            store_tab2.GetComponentsInChildren<Button>()[1].interactable = false;
        else
            store_tab2.GetComponentsInChildren<Button>()[1].interactable = true;


        if (Game_Manager.instance.Player.Checking_Account - 500 < 0 || couch_purchased)
            store_tab2.GetComponentsInChildren<Button>()[2].interactable = false;
        else
            store_tab2.GetComponentsInChildren<Button>()[2].interactable = true;


        if (Game_Manager.instance.Player.Checking_Account - 1000 < 0 || television_purchased)
            store_tab2.GetComponentsInChildren<Button>()[3].interactable = false;
        else
            store_tab2.GetComponentsInChildren<Button>()[3].interactable = true;

    }

    public void close_store()
    {
        modal_panel.transform.localPosition = off_screen;
        store_tab.transform.localPosition = off_screen;
        store_tab2.transform.localPosition = off_screen;
    }

    public void open_fridge()
    {
        modal_panel.transform.localPosition = modal_on_screen;
        fridge_tab.transform.localPosition = fridge_on_screen;

        if (Game_Manager.instance.Player.Poor_Food == 0)
            fridge_tab.GetComponentsInChildren<Button>()[1].interactable = false;
        else
            fridge_tab.GetComponentsInChildren<Button>()[1].interactable = true;


        if (Game_Manager.instance.Player.Middle_Class_Food == 0)
            fridge_tab.GetComponentsInChildren<Button>()[2].interactable = false;
        else
            fridge_tab.GetComponentsInChildren<Button>()[2].interactable = true;


        if (Game_Manager.instance.Player.Rich_Food == 0)
            fridge_tab.GetComponentsInChildren<Button>()[3].interactable = false;
        else
            fridge_tab.GetComponentsInChildren<Button>()[3].interactable = true;

        fridge_tab.GetComponentsInChildren<Text>()[2].text = "X" + Game_Manager.instance.Player.Poor_Food.ToString();
        fridge_tab.GetComponentsInChildren<Text>()[3].text = "X" + Game_Manager.instance.Player.Middle_Class_Food.ToString();
        fridge_tab.GetComponentsInChildren<Text>()[4].text = "X" + Game_Manager.instance.Player.Rich_Food.ToString();
    }

    public void close_fridge()
    {
        modal_panel.transform.localPosition = off_screen;
        fridge_tab.transform.localPosition = off_screen;
    }

    public void open_work_or_fun()
    {
        modal_panel.transform.localPosition = modal_on_screen;
        work_or_fun_tab.transform.localPosition = on_screen;
        if (Game_Manager.instance.Player.Checking_Account < 100)
            work_or_fun_tab.GetComponentsInChildren<Button>()[2].interactable = false;
        else
            work_or_fun_tab.GetComponentsInChildren<Button>()[2].interactable = true;
    }

    public void close_work_or_fun()
    {
        modal_panel.transform.localPosition = off_screen;
        work_or_fun_tab.transform.localPosition = off_screen;
    }

    public void close_life_event()
    {
        Life_Event.transform.localPosition = off_screen;
        modal_panel.transform.localPosition = off_screen;
    }

    public void purchase_food(int price)
    {
        switch (price)
        {
            case 25:
                    Game_Manager.instance.Player.Checking_Account -= 25;
                    Game_Manager.instance.Player.Poor_Food++;
                    food_purchase_check();
                    break;
            case 50:
                    Game_Manager.instance.Player.Middle_Class_Food++;
                    Game_Manager.instance.Player.Checking_Account -=50;
                    food_purchase_check();
                    break;
            case 100:
                    Game_Manager.instance.Player.Rich_Food++;
                    Game_Manager.instance.Player.Checking_Account -= 100;
                    food_purchase_check();
                    break;
            }
        set_side_values();
        }
    
    public void purchase_house_items(int price)
    {
        switch (price)
        {
            // gold fish
            case 25:
                if (Game_Manager.instance.Player.Player_House.Min_Payment == 800)
                    small_house.GetComponentsInChildren<Image>()[3].enabled = true;
                else if (Game_Manager.instance.Player.Player_House.Min_Payment == 1200)
                    medium_house.GetComponentsInChildren<Image>()[3].enabled = true;
                else
                    large_house.GetComponentsInChildren<Image>()[3].enabled = true;       
                Game_Manager.instance.Player.Checking_Account -= 25;
                Game_Manager.instance.Player.Player_Traits.alter_thriftyness(.2);
                gold_fish_purchased = true;
                store_tab2.GetComponentsInChildren<Button>()[1].interactable = false;
                break;
            // couch
            case 500:
                if(Game_Manager.instance.Player.Player_House.Min_Payment == 800)
                {
                    small_house.GetComponentsInChildren<Image>()[4].enabled = true;
                    small_house.GetComponentsInChildren<Image>()[5].enabled = true;
                }
                else if(Game_Manager.instance.Player.Player_House.Min_Payment == 1200)
                {
                    medium_house.GetComponentsInChildren<Image>()[4].enabled = true;
                    medium_house.GetComponentsInChildren<Image>()[5].enabled = true;
                }
                else
                {
                    large_house.GetComponentsInChildren<Image>()[4].enabled = true;
                    large_house.GetComponentsInChildren<Image>()[5].enabled = true;
                }
                Game_Manager.instance.Player.Checking_Account -= 500;
                couch_purchased = true;
                store_tab2.GetComponentsInChildren<Button>()[2].interactable = false;
                break;
            // tv
            case 1000:
                if (Game_Manager.instance.Player.Player_House.Min_Payment == 800)
                    small_house.GetComponentsInChildren<Image>()[6].enabled = true;
                else if (Game_Manager.instance.Player.Player_House.Min_Payment == 1200)
                    medium_house.GetComponentsInChildren<Image>()[6].enabled = true;
                else
                    large_house.GetComponentsInChildren<Image>()[6].enabled = true;
                Game_Manager.instance.Player.Checking_Account -= 1000;
                television_purchased = true;
                store_tab2.GetComponentsInChildren<Button>()[3].interactable = false;
                break;
        }
        set_side_values();
    }

    private void food_purchase_check()
    {
        if (Game_Manager.instance.Player.Checking_Account - 25 < 0)
            store_tab.GetComponentsInChildren<Button>()[1].interactable = false;

        if (Game_Manager.instance.Player.Checking_Account - 50 < 0)
            store_tab.GetComponentsInChildren<Button>()[2].interactable = false;

        if (Game_Manager.instance.Player.Checking_Account - 100 < 0)
            store_tab.GetComponentsInChildren<Button>()[3].interactable = false;
    }

    private void food_check(int health_gain)
    {
        switch (health_gain)
        {
            case 10:
                Game_Manager.instance.Player.Poor_Food--;
                fridge_tab.GetComponentsInChildren<Text>()[2].text = "X" + Game_Manager.instance.Player.Poor_Food.ToString();
                if (Game_Manager.instance.Player.Poor_Food == 0)
                    fridge_tab.GetComponentsInChildren<Button>()[1].interactable = false;
                break;
            case 20:
                Game_Manager.instance.Player.Middle_Class_Food--;
                fridge_tab.GetComponentsInChildren<Text>()[3].text = "X" + Game_Manager.instance.Player.Middle_Class_Food.ToString();
                if (Game_Manager.instance.Player.Middle_Class_Food == 0)
                    fridge_tab.GetComponentsInChildren<Button>()[2].interactable = false;
                break;
            case 30:
                Game_Manager.instance.Player.Rich_Food--;
                fridge_tab.GetComponentsInChildren<Text>()[4].text = "X" + Game_Manager.instance.Player.Rich_Food.ToString();
                if (Game_Manager.instance.Player.Rich_Food == 0)
                    fridge_tab.GetComponentsInChildren<Button>()[3].interactable = false;
                break;
        }       
    }

    private bool checking_account_check(int amount)
    {
        if (amount > Game_Manager.instance.Player.Checking_Account)
            return false;
        return true;
    }

    private void rested_check()
    {
        if (Game_Manager.instance.Player.Rested < 30)
            Game_Manager.instance.Player.Promotion_Count = (Game_Manager.instance.Player.Promotion_Count - 20 <= 0) ? 0 : Game_Manager.instance.Player.Promotion_Count - 20;
        if (Game_Manager.instance.Player.Promotion_Count == 0 && Game_Manager.instance.Player.Job_Level > 0)
        {
            Game_Manager.instance.Player.Job_Level--;
            float current_wage = Game_Manager.instance.Player.Player_Job.Hourly_Wage;
            int res = (int)Math.Ceiling(current_wage * .9);
            Game_Manager.instance.Player.Player_Job.Hourly_Wage = res;
            Game_Manager.instance.Player.Player_Job.Promotion();
            Game_Manager.instance.Player.Promotion_Count = 80;
            set_side_values();
        }
    }

    private void happiness_check()
    {
        if (Game_Manager.instance.Player.Happiness < 40)
        {
            Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - 5 <= 0) ? 0 : Game_Manager.instance.Player.Rested - 5;
            rested_check();
        }
    }

    private void health_check()
    {
        if (Game_Manager.instance.Player.Health < 40)
        {
            Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - 5 <= 0) ? 0 : Game_Manager.instance.Player.Rested - 5;
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness - 5 <= 0) ? 0 : Game_Manager.instance.Player.Happiness - 5;
            happiness_check();
        }
        if (Game_Manager.instance.Player.Health <= 0)
            lose();

    }

    private void promotion_check()
    {
        int job_level = Game_Manager.instance.Player.Job_Level;

        if (job_level >= 3)
            return;

        if (Game_Manager.instance.Player.Promotion_Count >= 100)
        {
            if (job_level < 3)
            {
                Game_Manager.instance.Player.Job_Level++;
                Game_Manager.instance.Player.Player_Job.Promotion();
            }
            Game_Manager.instance.Player.Promotion_Count = 10;
            set_side_values();
        }
    }

    private void day_passed(int amount)
    {
        Game_Manager.instance.Player.Time_Played += amount;
        hour = Game_Manager.instance.Player.Time_Played % 24;
        bool day_passed = false;
        if (day < Game_Manager.instance.Player.Time_Played / 24)
            day_passed = true;
        day = Game_Manager.instance.Player.Time_Played / 24;
        if (day_passed)
        {
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness - 5 <= 0) ? 0 : Game_Manager.instance.Player.Happiness - 5;
            string r_event = random_event.Execute_Event();
            if(!r_event.Equals(""))
            {
                work_or_fun_tab.transform.localPosition = off_screen;
                modal_panel.transform.localPosition = modal_on_screen;
                Life_Event.transform.localPosition = life_event_on_screen;
                Life_Event.GetComponentsInChildren<Text>()[2].text = r_event;
            }
            set_top_values();
            set_side_values(); 
        }

        if(day % 30 == 0 && day > 1 && (!same_day))
        {
            if (monthly_car_payment)
            {
                if (monthly_house_payment)
                {
                    if (monthly_student_loan_payment)
                    {
                        monthly_student_loan_payment = (Game_Manager.instance.Player.Student_Loan == 0) ? true : false;
                        monthly_house_payment = (Game_Manager.instance.Player.House_Loan == 0) ? true : false;
                        monthly_car_payment = (Game_Manager.instance.Player.Car_Loan == 0) ? true : false;
                        c_check.transform.localPosition = off_screen;
                        h_check.transform.localPosition = off_screen;
                        s_check.transform.localPosition = off_screen;
                        same_day = true;
                    }
                    else
                        lose();
                }
                else
                    lose();
            }
            else
                lose();
        }
        if (day % 31 == 0 && day > 1)
            same_day = false;

        time.GetComponentsInChildren<Text>()[2].text = day.ToString();
        time.GetComponentsInChildren<Text>()[3].text = hour.ToString();

        // anything else we want to happen overnight
    }

    public void increment_student_payment()
    {
        if (Game_Manager.instance.Player.Student_Loan == 0)
        {
            s_dec.GetComponent<Button>().interactable = false;
            s_inc.GetComponent<Button>().interactable = false;
            return;
        }
        if(Game_Manager.instance.Player.Degree == "Bachelor's")
        {
            s_dec.GetComponent<Button>().interactable = true;
            student_payment = (student_payment + 150 < Game_Manager.instance.Player.Student_Loan) ? student_payment + 150 : student_payment = Game_Manager.instance.Player.Student_Loan;
            if (student_payment == Game_Manager.instance.Player.Student_Loan)
                s_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[5].text = "$" + student_payment.ToString("N0");
        }
        else if(Game_Manager.instance.Player.Degree == "Master's")
        {
            s_dec.GetComponent<Button>().interactable = true;
            student_payment = (student_payment + 250 < Game_Manager.instance.Player.Student_Loan) ? student_payment + 250 : student_payment = Game_Manager.instance.Player.Student_Loan;
            if (student_payment == Game_Manager.instance.Player.Student_Loan)
                s_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[5].text = "$" + student_payment.ToString("N0");
        }
        else
        {
            s_dec.GetComponent<Button>().interactable = true;
            student_payment = (student_payment + 350 < Game_Manager.instance.Player.Student_Loan) ? student_payment + 350 : student_payment = Game_Manager.instance.Player.Student_Loan;
            if (student_payment == Game_Manager.instance.Player.Student_Loan)
                s_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[5].text = "$" + student_payment.ToString("N0");
        }
        unlock_increment();
    }

    public void decrement_student_payment()
    {
        if (Game_Manager.instance.Player.Degree == "Bachelor's")
        {
            student_payment = (student_payment - 150 <= 0) ? 0 : student_payment - 150;
            bank_tab.GetComponentsInChildren<Text>()[5].text = "$" + student_payment.ToString("N0");
            if (student_payment <= 0)
            {
                s_dec.GetComponent<Button>().interactable = false;
            }
        }
        else if (Game_Manager.instance.Player.Degree == "Master's")
        {
            student_payment = (student_payment - 250 <= 0) ? 0 : student_payment - 250;
            bank_tab.GetComponentsInChildren<Text>()[5].text = "$" + student_payment.ToString("N0");
            if (student_payment <= 0)
            {
                s_dec.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            student_payment = (student_payment - 350 <= 0) ? 0 : student_payment - 350;
            bank_tab.GetComponentsInChildren<Text>()[5].text = "$" + student_payment.ToString("N0");
            if (student_payment <= 0)
            {
                s_dec.GetComponent<Button>().interactable = false;
            }
        }
        unlock_increment();
    }

    public void increment_house_payment()
    {
        if(Game_Manager.instance.Player.House_Loan == 0)
        {
            h_dec.GetComponent<Button>().interactable = false;
            h_inc.GetComponent<Button>().interactable = false;
            return;
        }
        if(Game_Manager.instance.Player.Player_House.Neighborhood == "Sketchy")
        {
            h_dec.GetComponent<Button>().interactable = true;
            house_payment = (house_payment + 800 < Game_Manager.instance.Player.House_Loan) ? house_payment + 800 : house_payment = Game_Manager.instance.Player.House_Loan;
            if (house_payment == Game_Manager.instance.Player.House_Loan)
                h_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[6].text = "$" + house_payment.ToString("N0");
        }
        else if(Game_Manager.instance.Player.Player_House.Neighborhood == "Decent")
        {
            h_dec.GetComponent<Button>().interactable = true;
            house_payment = (house_payment + 1200 < Game_Manager.instance.Player.House_Loan) ? house_payment + 1200 : house_payment = Game_Manager.instance.Player.House_Loan;
            if (house_payment == Game_Manager.instance.Player.House_Loan)
                h_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[6].text = "$" + house_payment.ToString("N0");
        }
        else
        {
            h_dec.GetComponent<Button>().interactable = true;
            house_payment = (house_payment + 2500 < Game_Manager.instance.Player.House_Loan) ? house_payment + 2500 : house_payment = Game_Manager.instance.Player.House_Loan;
            if (house_payment == Game_Manager.instance.Player.House_Loan)
                h_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[6].text = "$" + house_payment.ToString("N0");
        }
        unlock_increment();
    }

    public void decrement_house_payment()
    {
        if(Game_Manager.instance.Player.Player_House.Neighborhood == "Sketchy")
        {
            house_payment = (house_payment - 800 <= 0) ? 0 : house_payment - 800;
            bank_tab.GetComponentsInChildren<Text>()[6].text = "$" + house_payment.ToString("N0");
            if (house_payment <= 0)
            {
                h_dec.GetComponent<Button>().interactable = false;
            }
        }
        else if(Game_Manager.instance.Player.Player_House.Neighborhood == "Decent")
        {
            house_payment = (house_payment - 1200 <= 0) ? 0 : house_payment - 1200;
            bank_tab.GetComponentsInChildren<Text>()[6].text = "$" + house_payment.ToString("N0");
            if (house_payment <= 0)
            {
                h_dec.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            house_payment = (house_payment - 2500 <= 0) ? 0 : house_payment - 2500;
            bank_tab.GetComponentsInChildren<Text>()[6].text = "$" + house_payment.ToString("N0");
            if (house_payment <= 0)
            {
                h_dec.GetComponent<Button>().interactable = false;
            }
        }
        unlock_increment();
    }

    public void increment_car_payment()
    {
        if (Game_Manager.instance.Player.Car_Loan == 0)
        {
            c_dec.GetComponent<Button>().interactable = false;
            c_inc.GetComponent<Button>().interactable = false;
            return;
        }
        if (Game_Manager.instance.Player.Player_Car.Name == "pos_car")
        {
            
            c_dec.GetComponent<Button>().interactable = true;
            car_payment = (car_payment + 200 < Game_Manager.instance.Player.Car_Loan) ? car_payment + 200 : car_payment = Game_Manager.instance.Player.Car_Loan;
            if (car_payment == Game_Manager.instance.Player.Car_Loan)
                c_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[8].text = "$" + car_payment.ToString("N0");
        }
        else if (Game_Manager.instance.Player.Player_Car.Name == "decent")
        {
            c_dec.GetComponent<Button>().interactable = true;
            car_payment = (car_payment + 300 < Game_Manager.instance.Player.Car_Loan) ? car_payment + 300 : car_payment = Game_Manager.instance.Player.Car_Loan;
            if (car_payment == Game_Manager.instance.Player.Car_Loan)
                c_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[8].text = "$" + car_payment.ToString("N0");
        }
        else
        {
            c_dec.GetComponent<Button>().interactable = true;
            car_payment = (car_payment + 500 < Game_Manager.instance.Player.Car_Loan) ? car_payment + 300 : car_payment = Game_Manager.instance.Player.Car_Loan;
            if (car_payment == Game_Manager.instance.Player.Car_Loan)
                c_inc.GetComponent<Button>().interactable = false;
            bank_tab.GetComponentsInChildren<Text>()[8].text = "$" + car_payment.ToString("N0");
        }
        unlock_increment();
    }

    public void decrement_car_payment()
    {
        if(Game_Manager.instance.Player.Player_Car.Name == "pos_car")
        {
            if (Game_Manager.instance.Player.Car_Loan == 0)
            {
                c_dec.GetComponent<Button>().interactable = false;
                return;
            }
            car_payment = (car_payment - 200 <= 0) ? 0 : car_payment - 200;
            bank_tab.GetComponentsInChildren<Text>()[8].text = "$" + car_payment.ToString("N0");
            if (car_payment <= 0)
            {
                c_dec.GetComponent<Button>().interactable = false;
            }
        }
        else if(Game_Manager.instance.Player.Player_Car.Name == "decent")
        {
            car_payment = (car_payment - 300 <= 0) ? 0 : car_payment - 300;
            bank_tab.GetComponentsInChildren<Text>()[8].text = "$" + car_payment.ToString("N0");
            if (car_payment <= 0)
            {
                c_dec.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            car_payment = (car_payment - 500 <= 0) ? 0 : car_payment - 500;
            bank_tab.GetComponentsInChildren<Text>()[8].text = "$" + car_payment.ToString("N0");
            if (car_payment <= 0)
            {
                c_dec.GetComponent<Button>().interactable = false;
            }
        }
        unlock_increment();
    }

    private void unlock_increment()
    {
        if (Game_Manager.instance.Player.Degree == "Bachelor's")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 150 || Game_Manager.instance.Player.Student_Loan == 0 || Game_Manager.instance.Player.Student_Loan == student_payment)
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = true;
            }
        }
        else if (Game_Manager.instance.Player.Degree == "Master's")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 250 || Game_Manager.instance.Player.Student_Loan == 0 || Game_Manager.instance.Player.Student_Loan == student_payment)
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = true;
            }
        }
        else if (Game_Manager.instance.Player.Degree == "Doctorate")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 350 || Game_Manager.instance.Player.Student_Loan == 0 || Game_Manager.instance.Player.Student_Loan == student_payment)
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[1].interactable = true;
            }
        }

        if (Game_Manager.instance.Player.Player_House.Neighborhood == "Sketchy")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 800 || Game_Manager.instance.Player.House_Loan == 0 || Game_Manager.instance.Player.House_Loan == house_payment)
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = true;
            }
        }
        else if (Game_Manager.instance.Player.Player_House.Neighborhood == "Decent")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 1200 || Game_Manager.instance.Player.House_Loan == 0 || Game_Manager.instance.Player.House_Loan == house_payment) 
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = true;
            }
        }
        else if (Game_Manager.instance.Player.Player_House.Neighborhood == "Prime")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 2500 || Game_Manager.instance.Player.House_Loan == 0 || Game_Manager.instance.Player.House_Loan == house_payment)
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[2].interactable = true;
            }
        }

        if (Game_Manager.instance.Player.Player_Car.Name == "pos_car")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 200 || Game_Manager.instance.Player.Car_Loan == 0 || Game_Manager.instance.Player.Car_Loan == car_payment)
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = true;
            }
        }
        else if (Game_Manager.instance.Player.Player_Car.Name == "decent")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 300 || Game_Manager.instance.Player.Car_Loan == 0 || Game_Manager.instance.Player.Car_Loan == car_payment)
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = true;
            }
        }
        else if (Game_Manager.instance.Player.Player_Car.Name == "nice_car")
        {
            if (Game_Manager.instance.Player.Checking_Account < (student_payment + house_payment + car_payment) + 500 || Game_Manager.instance.Player.Car_Loan == 0 || Game_Manager.instance.Player.Car_Loan == car_payment)
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = false;
            }
            else
            {
                bank_tab.GetComponentsInChildren<Button>()[4].interactable = true;
            }
        }

        loan_paid_off();
    }

    public void pay_loans()
    {
        Game_Manager.instance.Player.Checking_Account -= (student_payment + house_payment + car_payment);
        Game_Manager.instance.Player.Student_Loan -= student_payment;
        Game_Manager.instance.Player.House_Loan -= house_payment;
        Game_Manager.instance.Player.Car_Loan -= car_payment;

        Game_Manager.instance.Player.Debt += (student_payment + house_payment + car_payment);
        top.GetComponentsInChildren<Text>()[2].text = Game_Manager.instance.Player.Debt.ToString("N0");

        if (student_payment > 0 || Game_Manager.instance.Player.Student_Loan == 0)
        {
            monthly_student_loan_payment = true;
            s_check.transform.localPosition = s_check_location;
        }

        if (house_payment > 0 || Game_Manager.instance.Player.House_Loan == 0)
        {
            monthly_house_payment = true;
            h_check.transform.localPosition = h_check_location;
        }

        if (car_payment > 0 || Game_Manager.instance.Player.Car_Loan == 0)
        {
            monthly_car_payment = true;
            c_check.transform.localPosition = c_check_location;
        }
        loan_paid_off();
        set_side_values();
        open_bank();

        if (Game_Manager.instance.Player.Debt == 0)
        {
            Game_Manager.instance.current_state = Game_Manager.Game_States.WIN_SCENE;
            Game_Manager.instance.scene_loaded = false;
        }
    }

    private void lose()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.LOSE_SCENE;
        Game_Manager.instance.scene_loaded = false;
    }

    private void loan_paid_off()
    {
        if (Game_Manager.instance.Player.House_Loan == 0)
        {
            h_dec.GetComponent<Button>().interactable = false;
            h_inc.GetComponent<Button>().interactable = false;
        }
        if (Game_Manager.instance.Player.Student_Loan == 0)
        {
            s_dec.GetComponent<Button>().interactable = false;
            s_inc.GetComponent<Button>().interactable = false;
        }
        if (Game_Manager.instance.Player.Car_Loan == 0)
        {
            c_dec.GetComponent<Button>().interactable = false;
            c_inc.GetComponent<Button>().interactable = false;
        }
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
        side.GetComponentsInChildren<Text>()[7].fontSize = Game_Manager.instance.Font_Size;
        side.GetComponentsInChildren<Text>()[8].fontSize = Game_Manager.instance.Font_Size;

        go_to_work.GetComponentInChildren<Text>().fontSize = Game_Manager.instance.Font_Size;
        go_out.GetComponentInChildren<Text>().fontSize = Game_Manager.instance.Font_Size;

        time.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        time.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        time.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;
        time.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size;

        bank_tab.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size * 2;
        bank_tab.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[5].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[6].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[7].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[8].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[9].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[10].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[11].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[12].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[13].fontSize = Game_Manager.instance.Font_Size;
        bank_tab.GetComponentsInChildren<Text>()[14].fontSize = Game_Manager.instance.Font_Size;

        store_tab.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size * 2;
        store_tab.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        store_tab.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;
        store_tab.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size;
        store_tab.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size;
       
        store_tab2.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size * 2;
        store_tab2.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        store_tab2.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;
        store_tab2.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size;
        store_tab2.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size;

        fridge_tab.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size * 2;
        fridge_tab.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        fridge_tab.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;
        fridge_tab.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size;
        fridge_tab.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size;

        Life_Event.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size * 2;
        Life_Event.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        Life_Event.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;

        GameObject.Find("Store_Button").GetComponentInChildren<Text>().fontSize = Game_Manager.instance.Font_Size;
        GameObject.Find("Bank_button").GetComponentInChildren<Text>().fontSize = Game_Manager.instance.Font_Size;
    }

    private void set_top_values()
    {
        top.GetComponentsInChildren<Text>()[0].text = Game_Manager.instance.Player.Name;
        top.GetComponentsInChildren<Text>()[2].text = Game_Manager.instance.Player.Debt.ToString("N0");
    }

    private void set_side_values()
    {
        side.GetComponentsInChildren<Text>()[5].text = Game_Manager.instance.Player.Player_Job.Name;
        side.GetComponentsInChildren<Text>()[7].text = Game_Manager.instance.Player.Player_Job.Hourly_Wage + "/hour";
        side.GetComponentsInChildren<Text>()[8].text = "Checking Account: $" + Game_Manager.instance.Player.Checking_Account.ToString();
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

        double he_bar = bar_full - (bar_full) * ((double)Game_Manager.instance.Player.Health / 100.0);
        health_bar.GetComponent<RectTransform>().sizeDelta = new Vector3(-(float)he_bar, 0f);
        if (Game_Manager.instance.Player.Health <= 25)
        {
            health_bar.GetComponentsInChildren<Image>()[0].color = new Color(255, 0, 0);
            health_bar.GetComponentsInChildren<Image>()[1].color = new Color(255, 0, 0);
            health_bar.GetComponentsInChildren<Image>()[2].color = new Color(255, 0, 0);
        }
        else if (Game_Manager.instance.Player.Health <= 60)
        {
            health_bar.GetComponentsInChildren<Image>()[0].color = new Color(255, 204, 0);
            health_bar.GetComponentsInChildren<Image>()[1].color = new Color(255, 204, 0);
            health_bar.GetComponentsInChildren<Image>()[2].color = new Color(255, 204, 0);
        }
        else
        {
            health_bar.GetComponentsInChildren<Image>()[0].color = new Color(0, 204, 0);
            health_bar.GetComponentsInChildren<Image>()[1].color = new Color(0, 204, 0);
            health_bar.GetComponentsInChildren<Image>()[2].color = new Color(0, 204, 0);
        }
    }
}
