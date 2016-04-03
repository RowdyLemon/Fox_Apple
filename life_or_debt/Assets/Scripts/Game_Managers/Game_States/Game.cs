﻿using UnityEngine;
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
    public GameObject car;
    public GameObject bank_tab;
    public GameObject store_tab;
    public GameObject modal_panel;
    public GameObject fridge_tab;

    private Vector2 off_screen;
    private Vector2 on_screen;
    private Vector2 modal_on_screen;
    private Vector2 fridge_on_screen;

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
    private int student_payment;
    private int house_payment;
    private int car_payment;

    private bool monthly_car_payment;
    private bool monthly_house_payment;
    private bool monthly_student_loan_payment;

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
        car.GetComponent<Image>().sprite = Resources.Load("Sprites/UI/Car/" + Game_Manager.instance.Player.Player_Car.Name, typeof(Sprite)) as Sprite;
        alter_bar_values();
        day = 0;
        hour = 0;
        monthly_car_payment = false;
        monthly_house_payment = false;
        monthly_student_loan_payment = false;
        off_screen = new Vector2(1000, 1000);
        on_screen = bank_tab.transform.localPosition;
        fridge_on_screen = fridge_tab.transform.localPosition;
        modal_on_screen = modal_panel.transform.localPosition;
        bank_tab.transform.localPosition = off_screen;
        modal_panel.transform.localPosition = off_screen;
        fridge_tab.transform.localPosition = off_screen;
        store_tab.transform.localPosition = off_screen;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void rest(int amount)
    {
        int rest_time = (int)(rest_change * 10);
        int health_val = (int)(health_change * 5);

        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested + rest_time > 100) ? 100 : Game_Manager.instance.Player.Rested + rest_time;
        Game_Manager.instance.Player.Health = (Game_Manager.instance.Player.Health - health_val <= 0) ? 0 : Game_Manager.instance.Player.Health - health_val;

        health_check();
        day_passed(amount);
        alter_bar_values();
    }

    public void entertainment(Entertainment_Param entertainment_param)
    {
        if (!checking_account_check(entertainment_param.cost))
        {
            Debug.Log("Insuffcient funds");
            return;
        }

        int happiness_val = (int)(happiness_change * entertainment_param.happiness_change);
        int rest_val = (int)(rest_change * 5);

        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - rest_val <= 0) ? 0 : Game_Manager.instance.Player.Rested - rest_val;
        Game_Manager.instance.Player.Checking_Account -= entertainment_param.cost;
        Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + happiness_val > 100) ? 100 : Game_Manager.instance.Player.Happiness + happiness_val;
        Game_Manager.instance.Player.Time_Played += entertainment_param.amount;
        day_passed(entertainment_param.amount);
        alter_bar_values();
        set_top_values();
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
    }

    public void eat(Eating_Param eating_param)
    {
        food_check(eating_param.health_gain);

        int health_val = (int)(health_change * eating_param.health_gain);
        int rest_val = (int)(rest_change * 5);

        Game_Manager.instance.Player.Health = (Game_Manager.instance.Player.Health + health_val >= 100) ? 100 : Game_Manager.instance.Player.Health + health_val;
        Game_Manager.instance.Player.Rested = (Game_Manager.instance.Player.Rested - rest_val <= 0) ? 0 : Game_Manager.instance.Player.Rested - rest_val;

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
        if (Game_Manager.instance.Player.Checking_Account < 100)
        {  
            bank_tab.GetComponentsInChildren<Button>()[1].interactable = false;
            bank_tab.GetComponentsInChildren<Button>()[2].interactable = false;
            bank_tab.GetComponentsInChildren<Button>()[4].interactable = false;
        }
        else
        {
            bank_tab.GetComponentsInChildren<Button>()[1].interactable = true;
            bank_tab.GetComponentsInChildren<Button>()[2].interactable = true;
            bank_tab.GetComponentsInChildren<Button>()[4].interactable = true;
        }
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

    public void close_store()
    {
        modal_panel.transform.localPosition = off_screen;
        store_tab.transform.localPosition = off_screen;
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
        {
            Game_Manager.instance.current_state = Game_Manager.Game_States.LOSE_SCENE;
            Game_Manager.instance.scene_loaded = false;
        }

    }

    private void promotion_check()
    {
        int job_level = Game_Manager.instance.Player.Job_Level;

        if (job_level >= 3)
            return;

        if (Game_Manager.instance.Player.Promotion_Count >= 100)
        {
            // Promotion
            float current_wage = Game_Manager.instance.Player.Player_Job.Hourly_Wage;

            int res = (int)Math.Ceiling(current_wage * 1.1);
            Game_Manager.instance.Player.Player_Job.Hourly_Wage = res;

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
        bottom.GetComponentsInChildren<Text>()[1].text = "";
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
            set_bottom_values(r_event);
            set_top_values();
        }

        if(day % 30 == 0 && day > 1)
        {
            if (!monthly_car_payment && !monthly_house_payment && !monthly_student_loan_payment)
            {
                Game_Manager.instance.current_state = Game_Manager.Game_States.LOSE_SCENE;
                Game_Manager.instance.scene_loaded = false;
            }
            else
                monthly_student_loan_payment = monthly_house_payment = monthly_car_payment = false;
        }
        time.GetComponentsInChildren<Text>()[2].text = day.ToString();
        time.GetComponentsInChildren<Text>()[3].text = hour.ToString();

        // anything else we want to happen overnight
    }

    public void increment_student_payment()
    {
        s_dec.GetComponent<Button>().interactable = true;
        student_payment += 100;
        bank_tab.GetComponentsInChildren<Text>()[5].text = "$" + student_payment.ToString("N0");
        if ((student_payment + house_payment + car_payment) + 100 > Game_Manager.instance.Player.Checking_Account)
        {
            s_inc.GetComponent<Button>().interactable = false;
            h_inc.GetComponent<Button>().interactable = false;
            c_inc.GetComponent<Button>().interactable = false;
        }
    }

    public void decrement_student_payment()
    {
        student_payment -= 100;
        bank_tab.GetComponentsInChildren<Text>()[5].text = "$" + student_payment.ToString("N0");
        s_inc.GetComponent<Button>().interactable = true;
        h_inc.GetComponent<Button>().interactable = true;
        c_inc.GetComponent<Button>().interactable = true;
        if (student_payment == 0)
        {
            s_dec.GetComponent<Button>().interactable = false;
        }
    }

    public void increment_house_payment()
    {
        h_dec.GetComponent<Button>().interactable = true;
        house_payment += 100;
        bank_tab.GetComponentsInChildren<Text>()[6].text = "$" + house_payment.ToString("N0");
        if ((student_payment + house_payment + car_payment) + 100 > Game_Manager.instance.Player.Checking_Account)
        {
            s_inc.GetComponent<Button>().interactable = false;
            h_inc.GetComponent<Button>().interactable = false;
            c_inc.GetComponent<Button>().interactable = false;
        }
    }

    public void decrement_house_payment()
    {
        house_payment -= 100;
        bank_tab.GetComponentsInChildren<Text>()[6].text = "$" + house_payment.ToString("N0");
        s_inc.GetComponent<Button>().interactable = true;
        h_inc.GetComponent<Button>().interactable = true;
        c_inc.GetComponent<Button>().interactable = true;
        if (house_payment == 0)
        {
            h_dec.GetComponent<Button>().interactable = false;
        }
    }

    public void increment_car_payment()
    {
        c_dec.GetComponent<Button>().interactable = true;
        car_payment += 100;
        bank_tab.GetComponentsInChildren<Text>()[8].text = "$" + car_payment.ToString("N0");
        if ((student_payment + house_payment + car_payment) + 100 > Game_Manager.instance.Player.Checking_Account)
        {
            s_inc.GetComponent<Button>().interactable = false;
            h_inc.GetComponent<Button>().interactable = false;
            c_inc.GetComponent<Button>().interactable = false;
        }
    }

    public void decrement_car_payment()
    {
        car_payment -= 100;
        bank_tab.GetComponentsInChildren<Text>()[8].text = "$" + car_payment.ToString("N0");
        s_inc.GetComponent<Button>().interactable = true;
        h_inc.GetComponent<Button>().interactable = true;
        c_inc.GetComponent<Button>().interactable = true;
        if (car_payment == 0)
        {
            c_dec.GetComponent<Button>().interactable = false;
        }
    }

    public void pay_loans()
    {
        Game_Manager.instance.Player.Checking_Account -= (student_payment + house_payment + car_payment);
        Game_Manager.instance.Player.Student_Loan -= student_payment;
        Game_Manager.instance.Player.House_Loan -= house_payment;
        Game_Manager.instance.Player.Car_Loan -= car_payment;

        Game_Manager.instance.Player.Debt += (student_payment + house_payment + car_payment);
        top.GetComponentsInChildren<Text>()[2].text = Game_Manager.instance.Player.Debt.ToString("N0");

        if (student_payment > 0)
            monthly_student_loan_payment = true;

        if(house_payment > 0)
            monthly_house_payment = true;

        if(car_payment > 0)
            monthly_car_payment = true;

        open_bank();
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


        bottom.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        bottom.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;

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

        fridge_tab.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size * 2;
        fridge_tab.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;
        fridge_tab.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size;
        fridge_tab.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size;
        fridge_tab.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size;

        GameObject.Find("Store_Button").GetComponentInChildren<Text>().fontSize = Game_Manager.instance.Font_Size;
        GameObject.Find("Bank_button").GetComponentInChildren<Text>().fontSize = Game_Manager.instance.Font_Size;
    }

    private void set_bottom_values(string s)
    {
        if(!(string.IsNullOrEmpty(s)))
            bottom.GetComponentsInChildren<Text>()[1].text = s;
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
