using UnityEngine;
using System.Collections;
public class Player {

    public Job Player_Job;
    public Car Player_Car;
    public House Player_House;
    public string Degree;
    public string Name;
    public int Debt;
    public int Happiness;
    public base_trait Player_Traits;
    public int Time_Played;
    public int Promotion_Count;
    public int Rested;
    public int Job_Level;

    public Player()
    {
        Player_Job = new Job();
        Player_Car = new Car();
        Player_House = new House();
        Degree = "";
        Name = "";
        Debt = 0;
        Happiness = 50;
        Player_Traits = new base_trait();
        Time_Played = 0;
        Promotion_Count = 0;
        Rested = 50;
        Job_Level = 0;
    }
}
