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

    public Player()
    {
        Player_Job = new Job();
        Player_Car = new Car();
        Player_House = new House();
        Degree = "";
        Name = "";
        Debt = 0;
        Happiness = 10;
    }
}
