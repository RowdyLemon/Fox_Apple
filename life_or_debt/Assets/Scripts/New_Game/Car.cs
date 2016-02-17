using UnityEngine;
using System.Collections;

public class Car {

    public int Sell_Value;
    public int Buy_Value;
    public int Age;

    Car POS;
    Car Decent;
    Car Quality;

    public Car()
    {
        POS = new Car(2500, 5000, 18);
        Decent = new Car(10000, 5000, 8);
        Quality = new Car(25000, 15000, 3);
    }

    public Car(int sell_value, int buy_value, int age)
    {
        Sell_Value = sell_value;
        Buy_Value = buy_value;
        Age = age;
    }

    public void Car_Selection(string quality)
    {
        if(quality.Equals("Bad_Car"))
        {
            Game_Manager.instance.Player.Player_Car = POS;
            Game_Manager.instance.Player.Debt += POS.Buy_Value;
        }
        else if (quality.Equals("Decent_Car"))
        {
            Game_Manager.instance.Player.Player_Car = Decent;
            Game_Manager.instance.Player.Debt += Decent.Buy_Value;
        }
        else
        {
            Game_Manager.instance.Player.Player_Car = Quality;
            Game_Manager.instance.Player.Debt += Quality.Buy_Value;
        }
    }
}
