using UnityEngine;
using System.Collections;

public class Car {

    public int Buy_Value;
    public int Min_Payment;
    public int Age;
    public string Name;

    Car POS;
    Car Decent;
    Car Quality;

    public Car()
    {
        POS = new Car(2500, 200, 18, "pos_car");
        Decent = new Car(5000, 300, 8, "decent");
        Quality = new Car(15000, 500, 3, "nice_car");
    }

    public Car(int buy_Value, int min_payment, int age, string name)
    {
        Buy_Value = buy_Value;
        Min_Payment = min_payment;
        Age = age;
        Name = name;
    }

    public void Car_Selection(string quality)
    {
        if(quality.Equals("Bad_Car"))
        {
            Game_Manager.instance.Player.Player_Car = POS;
            Game_Manager.instance.Player.Debt -= POS.Buy_Value;
            Game_Manager.instance.Player.Car_Loan += POS.Buy_Value;
        }
        else if (quality.Equals("Decent_Car"))
        {
            Game_Manager.instance.Player.Player_Car = Decent;
            Game_Manager.instance.Player.Debt -= Decent.Buy_Value;
            Game_Manager.instance.Player.Car_Loan += Decent.Buy_Value;
            Game_Manager.instance.Player.Happiness += 5;
        }
        else
        {
            Game_Manager.instance.Player.Player_Car = Quality;
            Game_Manager.instance.Player.Debt -= Quality.Buy_Value;
            Game_Manager.instance.Player.Car_Loan += Quality.Buy_Value;
            Game_Manager.instance.Player.Happiness += 15;
        }
    }
}
