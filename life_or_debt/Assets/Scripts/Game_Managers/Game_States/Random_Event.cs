using UnityEngine;
using System.Collections;

public class Random_Event {

    private string Event_Description;
    private int Debt_Change;
    private int Happiness_Change;
    private Random_Event[] Event_Array = new Random_Event[5];
    private double thriftyness_multiplier;

    public Random_Event(string event_description, int debt_change, int happiness_change)
    {
        Event_Description = event_description;
        Debt_Change = debt_change;
        Happiness_Change = happiness_change;
    }

    public Random_Event()
    {
        thriftyness_multiplier = Game_Manager.instance.Player.Player_Traits.Thriftyness;
        Event_Array[0] = new Random_Event("Your car has broken down, $"+(int)(-500 * thriftyness_multiplier), (int)(-500 * thriftyness_multiplier), -5);
        Event_Array[1] = new Random_Event("Steam Sale. You know the drill $"+(int)(-50 * thriftyness_multiplier), (int)(-50 * thriftyness_multiplier), 0);
        Event_Array[2] = new Random_Event("You've won the lottery! $"+(int)(1000 * thriftyness_multiplier)+"!", (int)(1000 * thriftyness_multiplier), 10);
        Event_Array[3] = new Random_Event("It is your cousins birthday, better not make Thanksgiving awkward..."+(int)(-25 * thriftyness_multiplier)+"", (int)(-25 * thriftyness_multiplier), 0);
        Event_Array[4] = new Random_Event("Your Alternative Game Development Teacher is the Best. Of course you have to buy him a gift!" + (int)(-25 * thriftyness_multiplier) + "", (int)(-25 * thriftyness_multiplier), 0);
    }


    public string Execute_Event()
    {
        int Random_Value = Random.Range(0, 50);
        if(Random_Value == 3)
        {
            Game_Manager.instance.Player.Checking_Account = (Game_Manager.instance.Player.Checking_Account - Event_Array[0].Debt_Change < 0) ? 0 : Game_Manager.instance.Player.Checking_Account - Event_Array[0].Debt_Change;
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness - Event_Array[0].Happiness_Change <= 0) ? 0 : Game_Manager.instance.Player.Happiness + Event_Array[0].Happiness_Change;
            return Event_Array[0].Event_Description;
        }
        else if (Random_Value == 4)
        {
            Game_Manager.instance.Player.Checking_Account = (Game_Manager.instance.Player.Checking_Account - Event_Array[1].Debt_Change < 0) ? 0 : Game_Manager.instance.Player.Checking_Account - Event_Array[1].Debt_Change;
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + Event_Array[1].Happiness_Change > 100) ? 100 : Game_Manager.instance.Player.Happiness + Event_Array[1].Happiness_Change;
            return Event_Array[1].Event_Description;
        }
        else if (Random_Value == 5)
        {
            Game_Manager.instance.Player.Checking_Account += (int)(1000 * thriftyness_multiplier);
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + Event_Array[2].Happiness_Change > 100) ? 100 : Game_Manager.instance.Player.Happiness + Event_Array[2].Happiness_Change;
            return Event_Array[2].Event_Description;
        }
        else if (Random_Value == 22)
        {
            Game_Manager.instance.Player.Checking_Account = (Game_Manager.instance.Player.Checking_Account - Event_Array[1].Debt_Change < 0) ? 0 : Game_Manager.instance.Player.Checking_Account - Event_Array[1].Debt_Change;
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + Event_Array[1].Happiness_Change > 100) ? 100 : Game_Manager.instance.Player.Happiness + Event_Array[1].Happiness_Change;
            return Event_Array[1].Event_Description;
        }
        else if (Random_Value == 23)
        {
            Game_Manager.instance.Player.Checking_Account = (Game_Manager.instance.Player.Checking_Account - Event_Array[1].Debt_Change < 0) ? 0 : Game_Manager.instance.Player.Checking_Account - Event_Array[1].Debt_Change;
            Game_Manager.instance.Player.Happiness = (Game_Manager.instance.Player.Happiness + Event_Array[1].Happiness_Change > 100) ? 100 : Game_Manager.instance.Player.Happiness + Event_Array[1].Happiness_Change;
            return Event_Array[1].Event_Description;
        }
        return "";
    }
}
