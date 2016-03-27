using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Job {

    public int Hourly_Wage;
    public string Name;
    public string Education_Requirement;
    public int Job_Id;

    private Job[] Jobs = new Job[9];

    private LinkedList<string>[] Job_Progression = new LinkedList<string>[9];

    //public Dictionary<string, LinkedList<string>> Job_Progression = new Dictionary<string, LinkedList<string>>();
 
    public Job()
    {
        Hourly_Wage = 0;
        Name = "";
        Education_Requirement = "";

        Jobs[0] = new Job(8, "Barista", "Bachelor's", 0);
        Jobs[1] = new Job(12, "Hostess", "Bachelor's", 1);
        Jobs[2] = new Job(20, "IT Technician", "Bachelor's", 2);

        Jobs[3] = new Job(15, "Bank Teller", "Master's", 3);
        Jobs[4] = new Job(30, "Salesmen", "Master's", 4);
        Jobs[5] = new Job(50, "Software Developer", "Master's", 5);

        Jobs[6] = new Job(40, "Assistant College Professor", "Doctorate", 6);
        Jobs[7] = new Job(80, "General Practitioner", "Doctorate", 7);
        Jobs[8] = new Job(100, "Aerospace Engineer", "Doctorate", 8);


        Populate_Job_Progression();

    }
    public Job(int hourly_wage, string name, string ed_requirement, int job_id)
    {
            Hourly_Wage = hourly_wage;
            Name = name;
            Education_Requirement = ed_requirement;
            this.Job_Id = job_id;
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
        Game_Manager.instance.Player.Player_Job.Job_Id = Player_Job.Job_Id;
    }

    private void Populate_Job_Progression()
    {
        for(int i = 0; i < Job_Progression.Length; i++ )
        {
            Job_Progression[i] = new LinkedList<string>();
        }

        // Barista Progression
        Job_Progression[0].AddLast("Shift Manager");
        Job_Progression[0].AddLast("Store Manager");
        Job_Progression[0].AddLast("Franchise Owner");

        // Server Progression
        Job_Progression[1].AddLast("Server");
        Job_Progression[1].AddLast("Shift Manager");
        Job_Progression[1].AddLast("Manager");

        // Web Developer
        Job_Progression[2].AddLast("Quality Assurance");
        Job_Progression[2].AddLast("Developer");
        Job_Progression[2].AddLast("Senior Developer");

        // Bank Teller
        Job_Progression[3].AddLast("Banker");
        Job_Progression[3].AddLast("Service Manager");
        Job_Progression[3].AddLast("Branch Manager");

        // Salesman
        Job_Progression[4].AddLast("Account Manager");
        Job_Progression[4].AddLast("Manager");
        Job_Progression[4].AddLast("Senior Manager");

        // Software Developer
        Job_Progression[5].AddLast("Senior Developer");
        Job_Progression[5].AddLast("Project Lead");
        Job_Progression[5].AddLast("Project Manager");

        // College Professor
        Job_Progression[6].AddLast("Assocaite College Professor");
        Job_Progression[6].AddLast("College Professor");
        Job_Progression[6].AddLast("Dean");

        // Medical Doctor
        Job_Progression[7].AddLast("Medical Specialist");
        Job_Progression[7].AddLast("Surgeon");
        Job_Progression[7].AddLast("Cheif of Staff");

        // Astronaut
        Job_Progression[8].AddLast("Senior Engineer");
        Job_Progression[8].AddLast("Flight Crew");
        Job_Progression[8].AddLast("NASA Administrator");

    }

    public void Promotion()
    {
        Game_Manager.instance.Player.Player_Job.Name = Job_Progression[Job_Id].First.Value;
        Job_Progression[Job_Id].RemoveFirst();
    }
}
