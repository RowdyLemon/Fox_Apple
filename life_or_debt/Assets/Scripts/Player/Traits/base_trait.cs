using UnityEngine;
using System.Collections;

public class base_trait
{
    private int p_mood;
    private int p_work_ethic;
    private int p_laziness;
    private int p_thriftyness;

    // Represents % rate at which player completes tasks
    // 0% = 0, 100% = 1 
    // e.g. A hard worker might work 150% harder than your average joe therefor his work_speed = 1.5
    protected double work_speed;
    protected double rest_rate;
    protected double mood;
    protected double thriftyness;

    public base_trait()
    {
        work_speed = rest_rate = mood = thriftyness = 0;
    }

    public base_trait(int player_mood, int player_work_ethic, int player_laziness, int player_thriftyness)
    {
        if (player_mood < 5)
            mood = .8;
        if (player_mood > 8)
            mood = 1.2;
        else 
            mood = 1;

        if (player_work_ethic < 5)
            work_speed = .8;
        if (player_work_ethic > 8)
            work_speed = 1.2;
        else
            work_speed = 1;


        if (player_laziness < 5)
        {
            rest_rate = 1.2;
            
        }
        else if (player_laziness > 8)
        {
            rest_rate = .8;
            Game_Manager.instance.Player.Rested -= 10;
        }
        else
            rest_rate = 1;


        if (player_thriftyness < 5)
            thriftyness = 1.2;
        else if (player_thriftyness > 8)
            thriftyness = .8;
        else
            thriftyness = 1;
    }

    

    // public getters for attributes
    public double Work_Speed { get { return work_speed; } }
    public double Rest_Rate { get { return rest_rate; } }
    public double Mood { get { return mood; } }
    public double Thriftyness { get { return thriftyness; } }

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
