using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player_Values : MonoBehaviour {

    public Sprite pos_car;
    public Sprite decent;
    public Sprite nice_car;


    public Sprite Cheap_House;
    public Sprite Medium_House;
    public Sprite Large_House;
    // Use this for initialization
	void Start () {

        // Fill in the players name
        GameObject Name_Update = GameObject.Find("PlayerName");
        Text Player_Name = Name_Update.GetComponent<Text>();
        Player_Name.text = Game_Manager.instance.Player.Name;

        // Display the starting amount of debt
        GameObject Debt_Amount = GameObject.Find("Debt_Amount");
        Debt_Amount.GetComponent<Text>().text = "-$" + Game_Manager.instance.Player.Debt.ToString("N0");

        // Display the car the player selected
        GameObject Car_Update = GameObject.Find("nice_car");
        SpriteRenderer Player_Car = Car_Update.GetComponent<SpriteRenderer>();
        if (Game_Manager.instance.Player.Player_Car.Name == "pos_car")
            Player_Car.sprite = pos_car;
        else if (Game_Manager.instance.Player.Player_Car.Name == "decent")
            Player_Car.sprite = decent;
        else
            Player_Car.sprite = nice_car;

        // Display the selected house
        GameObject House_Update = GameObject.Find("house_temp");
        SpriteRenderer Player_House = House_Update.GetComponent<SpriteRenderer>();
        if (Game_Manager.instance.Player.Player_House.Buy_Value == 50000)
            Player_House.sprite = Cheap_House;
        else if (Game_Manager.instance.Player.Player_House.Buy_Value == 150000)
            Player_House.sprite = Medium_House;
        else
            Player_House.sprite = Large_House;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
