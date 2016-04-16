using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

    public string Neighborhood;
    public int Min_Payment;
    public int Buy_Value;
    public int Size;
    House Small_House;
    House Medium_House;
    House Large_House;

    public House()
    {
        Small_House = new House("Sketchy", 800, 50000);
        Medium_House = new House("Decent", 1200, 150000);
        Large_House = new House("Prime", 2500, 400000);
    }

    public House(string neighborhood, int min_payment, int buy_value)
    {
        Neighborhood = neighborhood;
        Min_Payment = min_payment;
        Buy_Value = buy_value;
        //Size = 0;
    }

    public void House_Selection(string House_Name)
    {
        if (House_Name.Equals("Small_House"))
        {
            Game_Manager.instance.Player.Player_House = Small_House;
            Game_Manager.instance.Player.Debt -= Small_House.Buy_Value;
            Game_Manager.instance.Player.House_Loan += Small_House.Buy_Value;
        }
        else if (House_Name.Equals("Medium_House"))
        {
            Game_Manager.instance.Player.Player_House = Medium_House;
            Game_Manager.instance.Player.Debt -= Medium_House.Buy_Value;
            Game_Manager.instance.Player.House_Loan += Medium_House.Buy_Value;
            Game_Manager.instance.Player.Happiness += 10;
        }
        else
        {
            Game_Manager.instance.Player.Player_House = Large_House;
            Game_Manager.instance.Player.Debt -= Large_House.Buy_Value;
            Game_Manager.instance.Player.House_Loan += Large_House.Buy_Value;
            Game_Manager.instance.Player.Happiness += 15;
        }
    }
}
