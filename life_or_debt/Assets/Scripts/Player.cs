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
    public int Health;
    public int Student_Loan;
    public int House_Loan;
    public int Car_Loan;
    public int Checking_Account;
    public int Poor_Food;
    public int Middle_Class_Food;
    public int Rich_Food;

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
        Health = 100;
        Student_Loan = 0;
        House_Loan = 0;
        Car_Loan = 0;
        Checking_Account = 0;
    }
}
