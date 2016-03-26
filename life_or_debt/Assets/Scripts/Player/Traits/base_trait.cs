using UnityEngine;
using System.Collections;

public class base_trait
{
    // Represents % rate at which player completes tasks
    // 0% = 0, 100% = 1 
    // e.g. A hard worker might work 150% harder than your average joe therefor his work_speed = 1.5
    protected double work_speed;
    protected double rest_rate;
    protected double mood;
    protected string name;

    // public getters for attributes
    public double Work_Speed { get { return work_speed; } }
    public double Rest_Rate { get { return rest_rate; } }
    public double Mood { get { return mood; } }
    public string Name { get { return name; } }

    /*
     * + operator overload, allows the addition of two traits using just the + operator
     * e.g. base_trait c = base_trait a + base_trait b; 
     */
    public static base_trait operator+ (base_trait a, base_trait b)
    {
        base_trait c = new base_trait();
        c.work_speed = a.Work_Speed + b.Work_Speed;
        c.rest_rate = a.Rest_Rate + b.Rest_Rate;
        c.mood = a.Mood + b.Mood;
        return c;
    } 
}
