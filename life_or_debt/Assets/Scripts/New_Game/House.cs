using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

    public string Neighborhood;
    public int Sell_Value;
    public int Buy_Value;
    public int Size;
    House Small_House;
    House Medium_House;
    House Large_House;

    public House()
    {
        Small_House = new House("Sketchy", 25000, 50000);
        Medium_House = new House("Decent", 75000, 150000);
        Large_House = new House("Prime", 250000, 400000);
    }

    public House(string neighborhood, int sell_value, int buy_value)
    {
        Neighborhood = neighborhood;
        Sell_Value = sell_value;
        Buy_Value = buy_value;
        //Size = 0;
    }

    public void House_Selection(string House_Name)
    {
        if (House_Name.Equals("Small_House"))
        {
            Game_Manager.instance.Player.Player_House = Small_House;
            Game_Manager.instance.Player.Debt -= Small_House.Buy_Value;
        }
        else if (House_Name.Equals("Medium_House"))
        {
            Game_Manager.instance.Player.Player_House = Medium_House;
            Game_Manager.instance.Player.Debt -= Medium_House.Buy_Value;
        }
        else
        {
            Game_Manager.instance.Player.Player_House = Large_House;
            Game_Manager.instance.Player.Debt -= Large_House.Buy_Value;
        }
    }
}
