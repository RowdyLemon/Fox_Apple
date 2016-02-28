using UnityEngine;
using System.Collections;

public class Random_Event {

    private string Event_Description;
    private int Debt_Change;
    private int Happiness_Change;
    private Random_Event[] Event_Array = new Random_Event[3]; 

    public Random_Event(string event_description, int debt_change, int happiness_change)
    {
        Event_Description = event_description;
        Debt_Change = debt_change;
        Happiness_Change = happiness_change;
    }

    public Random_Event()
    {
        int Current_Wage = Game_Manager.instance.Player.Player_Job.Hourly_Wage;
        int Promotion = (Game_Manager.instance.Player.Player_Job.Hourly_Wage / 3) + Current_Wage;
        Event_Array[0] = new Random_Event("Your car has broken down, -$500", -500, 0);
        Event_Array[1] = new Random_Event("Congrats! You've been promoted. You now make $" + Promotion + " an hour", Promotion, 3);
        Event_Array[2] = new Random_Event("You've won the lottery!", 1000, 1);
    }


    public string Execute_Event()
    {
        int Random_Value = Random.Range(0, 15);
        if(Random_Value == 10)
        {
            Game_Manager.instance.Player.Debt -= Event_Array[0].Debt_Change;
            return Event_Array[0].Event_Description;
        }
        else if (Random_Value == 13)
        {
            Game_Manager.instance.Player.Player_Job.Hourly_Wage = Event_Array[1].Debt_Change;
            Debug.Log(Game_Manager.instance.Player.Player_Job.Hourly_Wage);
            Game_Manager.instance.Player.Happiness += Event_Array[1].Happiness_Change;
            return Event_Array[1].Event_Description;
        }
        else if (Random_Value == 15)
        {
            Game_Manager.instance.Player.Debt += Event_Array[2].Debt_Change;
            Game_Manager.instance.Player.Happiness += Event_Array[2].Happiness_Change;
            return Event_Array[2].Event_Description;
        }
        return "";
    }
}
