using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player_Values : MonoBehaviour {

    public Sprite pos_car;
    public Sprite decent;
    public Sprite nice_car;

    private GameObject Panel;
    private GameObject Fun_Toggle;
    private GameObject Work_Toggle;

    public Sprite Cheap_House;
    public Sprite Medium_House;
    public Sprite Large_House;

    public GameObject player_name;
    public GameObject debt;
    public GameObject happiness;
    public GameObject current_job;
    public GameObject current_pay;
    public GameObject work;
    public GameObject have_fun;
    public GameObject start_day;
    public GameObject debt_title;

    // Use this for initialization
	void Start () {
        Panel = GameObject.Find("Modal_Panel");
        GameObject.Find("Modal_Panel").SetActive(false);

        Work_Toggle = GameObject.Find("Work_Toggle");
        GameObject.Find("Work_Toggle").SetActive(false);
        Fun_Toggle = GameObject.Find("Fun_Toggle");
        GameObject.Find("Fun_Toggle").SetActive(false);

        // Fill in the players name
        GameObject Name_Update = GameObject.Find("PlayerName");
        Text Player_Name = Name_Update.GetComponent<Text>();
        Player_Name.text = Game_Manager.instance.Player.Name;

        // Fill in happiness, job_name, and pay
        GameObject.Find("Happiness").GetComponent<Text>().text = "Happiness: " + Game_Manager.instance.Player.Happiness.ToString();
        GameObject.Find("Job_Name").GetComponent<Text>().text = "Current Job: " + Game_Manager.instance.Player.Player_Job.Name;
        GameObject.Find("Pay").GetComponent<Text>().text = "Current Pay: $" + Game_Manager.instance.Player.Player_Job.Hourly_Wage.ToString() + "/hr";

        // Display the starting amount of debt
        GameObject Debt_Amount = GameObject.Find("Debt_Amount");
        Debt_Amount.GetComponent<Text>().text = "$" + Game_Manager.instance.Player.Debt.ToString("N0");

        GameObject Car_Update = GameObject.Find("Car");
        if (Game_Manager.instance.Player.Player_Car.Name == "pos_car")
            Car_Update.GetComponent<Image>().sprite = pos_car;
        else if (Game_Manager.instance.Player.Player_Car.Name == "decent")
            Car_Update.GetComponent<Image>().sprite = decent;
        else
            Car_Update.GetComponent<Image>().sprite = nice_car;

        GameObject House_Update = GameObject.Find("House");
        if (Game_Manager.instance.Player.Player_House.Buy_Value == 50000)
            House_Update.GetComponent<Image>().sprite = Cheap_House;
        else if (Game_Manager.instance.Player.Player_House.Buy_Value == 150000)
            House_Update.GetComponent<Image>().sprite = Medium_House;
        else
            House_Update.GetComponent<Image>().sprite = Large_House;

        Init();
	}

    private void Init()
    {
        player_name.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        debt.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        debt_title.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        happiness.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size/2;
        current_job.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        current_pay.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        work.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        have_fun.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        start_day.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
    }
	

    public void Start_Day()
    {
        Fun_Toggle.SetActive(true);
        Work_Toggle.SetActive(true);
        Panel.SetActive(true);
        GameObject.Find("Random_Event").GetComponent<Text>().text = "";
    }

    public void Execute_Day()
    {
        Random_Event Event = new Random_Event();
        string Description = Event.Execute_Event();

        Panel.SetActive(false);
        Fun_Toggle.SetActive(false);
        Work_Toggle.SetActive(false);

        // Increase happiness when fun is ticked
        if(Fun_Toggle.GetComponent<Toggle>().isOn)
        {
            Game_Manager.instance.Player.Happiness++;
            GameObject.Find("Happiness").GetComponent<Text>().text = "Happiness: " + Game_Manager.instance.Player.Happiness.ToString();
        }

        // Increase money, decrease happiness when working
        else
        {
            Game_Manager.instance.Player.Happiness--;
            GameObject.Find("Happiness").GetComponent<Text>().text = "Happiness: " + Game_Manager.instance.Player.Happiness.ToString();

            Game_Manager.instance.Player.Debt += Game_Manager.instance.Player.Player_Job.Hourly_Wage * 8;
            GameObject.Find("Debt_Amount").GetComponent<Text>().text = "$" + Game_Manager.instance.Player.Debt.ToString("N0");
        }
        
        // Random event when returned string isnt empty
        if(Description != "")
        {
            GameObject.Find("Happiness").GetComponent<Text>().text = "Happiness: " + Game_Manager.instance.Player.Happiness.ToString();
            GameObject Debt_Amount = GameObject.Find("Debt_Amount");
            Debt_Amount.GetComponent<Text>().text = "$" + Game_Manager.instance.Player.Debt.ToString("N0");
            GameObject.Find("Pay").GetComponent<Text>().text = "Current Pay: $" + Game_Manager.instance.Player.Player_Job.Hourly_Wage.ToString() + "/hr";
            
            // Display the message returned by Description.
            GameObject.Find("Random_Event").GetComponent<Text>().text = Description;
        }
    }
    
}
