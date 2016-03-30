using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Job {

    public int Hourly_Wage;
    public string Name;
    public string Education_Requirement;
    public int Job_Id;

    private Job[] Jobs = new Job[9];

    private ArrayList[] Job_Progression = new ArrayList[9];

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
            Job_Progression[i] = new ArrayList();
        }

        // Barista Progression
        Job_Progression[0].Add("Barista");
        Job_Progression[0].Add("Shift Manager");
        Job_Progression[0].Add("Store Manager");
        Job_Progression[0].Add("Franchise Owner");

        // Server Progression
        Job_Progression[1].Add("Hostess");
        Job_Progression[1].Add("Server");
        Job_Progression[1].Add("Shift Manager");
        Job_Progression[1].Add("Manager");

        // Web Developer
        Job_Progression[2].Add("IT Technician");
        Job_Progression[2].Add("Quality Assurance");
        Job_Progression[2].Add("Developer");
        Job_Progression[2].Add("Senior Developer");

        // Bank Teller
        Job_Progression[3].Add("Bank Teller");
        Job_Progression[3].Add("Banker");
        Job_Progression[3].Add("Service Manager");
        Job_Progression[3].Add("Branch Manager");

        // Salesman
        Job_Progression[4].Add("Salesman");
        Job_Progression[4].Add("Account Manager");
        Job_Progression[4].Add("Manager");
        Job_Progression[4].Add("Senior Manager");

        // Software Developer
        Job_Progression[5].Add("Software Developer");
        Job_Progression[5].Add("Senior Developer");
        Job_Progression[5].Add("Project Lead");
        Job_Progression[5].Add("Project Manager");

        // College Professor
        Job_Progression[6].Add("Assistant College Professor");
        Job_Progression[6].Add("Associate College Professor");
        Job_Progression[6].Add("College Professor");
        Job_Progression[6].Add("Dean");

        // Medical Doctor
        Job_Progression[7].Add("General Practitioner");
        Job_Progression[7].Add("Medical Specialist");
        Job_Progression[7].Add("Surgeon");
        Job_Progression[7].Add("Cheif of Staff");

        // Astronaut
        Job_Progression[8].Add("Aerospace Engineer");
        Job_Progression[8].Add("Senior Engineer");
        Job_Progression[8].Add("Flight Crew");
        Job_Progression[8].Add("NASA Administrator");

    }

    public void Promotion()
    {
        Game_Manager.instance.Player.Player_Job.Name = (string)Job_Progression[Job_Id][Game_Manager.instance.Player.Job_Level];
        Debug.Log((string)Job_Progression[Job_Id][Game_Manager.instance.Player.Job_Level]);
    }
}
