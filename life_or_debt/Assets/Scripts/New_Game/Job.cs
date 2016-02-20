using UnityEngine;
using System.Collections;
public class Job {

    public int Hourly_Wage;
    public string Name;
    public string Education_Requirement;

    private Job[] Jobs = new Job[9];
 
    public Job()
    {
        Hourly_Wage = 0;
        Name = "";
        Education_Requirement = "";

        Jobs[0] = new Job(8, "Barista", "Bachelor's");
        Jobs[1] = new Job(12, "Server", "Bachelor's");
        Jobs[2] = new Job(30, "Web Developer", "Bachelor's");

        Jobs[3] = new Job(15, "Bank Teller", "Master's");
        Jobs[4] = new Job(30, "Salesmen", "Master's");
        Jobs[5] = new Job(50, "Software Developer", "Master's");

        Jobs[6] = new Job(40, "College Professor", "Doctorate");
        Jobs[7] = new Job(80, "Medical Doctor", "Doctorate");
        Jobs[8] = new Job(100, "Astronaut", "Doctorate");
    }
    public Job(int hourly_wage, string name, string ed_requirement)
    {
            Hourly_Wage = hourly_wage;
            Name = name;
            Education_Requirement = ed_requirement;
    }

    public void Job_Choice(string Degree)
    {
        Job Player_Job;
        if(Degree.Equals("Bachelor's"))
        {
            Player_Job = Jobs[Random.Range(0, 3)];
        }
        else if(Degree.Equals("Master's"))
        {
            Player_Job = Jobs[Random.Range(3, 6)];
        }
        else
            Player_Job = Jobs[Random.Range(6, 9)];

        Game_Manager.instance.Player.Player_Job.Hourly_Wage = Player_Job.Hourly_Wage;
        Game_Manager.instance.Player.Player_Job.Name = Player_Job.Name;
        Game_Manager.instance.Player.Player_Job.Education_Requirement = Player_Job.Education_Requirement;
    }
}
