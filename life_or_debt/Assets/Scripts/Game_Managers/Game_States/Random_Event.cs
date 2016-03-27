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
        Event_Array[0] = new Random_Event("Your car has broken down, -$500", -500, -5);
        Event_Array[1] = new Random_Event("Steam Sale. You know the drill", -50, 0);
        Event_Array[2] = new Random_Event("You've won the lottery!", 1000, 10);
    }


    public string Execute_Event()
    {
        int Random_Value = Random.Range(0, 10);
        if(Random_Value == 3)
        {
            Game_Manager.instance.Player.Debt -= Event_Array[0].Debt_Change;
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + Event_Array[0].Happiness_Change > 100) ? 100 : Game_Manager.instance.Player.Happiness + Event_Array[0].Happiness_Change;
            return Event_Array[0].Event_Description;
        }
        else if (Random_Value == 4)
        {
            Game_Manager.instance.Player.Debt -= Event_Array[1].Debt_Change;
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + Event_Array[1].Happiness_Change > 100) ? 100 : Game_Manager.instance.Player.Happiness + Event_Array[1].Happiness_Change;
            return Event_Array[1].Event_Description;
        }
        else if (Random_Value == 5)
        {
            Game_Manager.instance.Player.Debt += Event_Array[2].Debt_Change;
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + Event_Array[2].Happiness_Change > 100) ? 100 : Game_Manager.instance.Player.Happiness + Event_Array[2].Happiness_Change;
            return Event_Array[2].Event_Description;
        }
        return "";
    }
}
