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

    private Dictionary<string, int> Salaries = new Dictionary<string, int>();

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
        Populate_Salaries();

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
        else if (Degree.Equals("Master's"))
        {
            Player_Job = Jobs[Random.Range(3, 6)];
            Game_Manager.instance.Player.Health -= 20;
        }
        else
        {
            Player_Job = Jobs[Random.Range(6, 9)];
            Game_Manager.instance.Player.Health -= 40;
        }

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
        Job_Progression[0].Add("Regional Manager");

        // Server Progression
        Job_Progression[1].Add("Hostess");
        Job_Progression[1].Add("Server");
        Job_Progression[1].Add("Restaraunt Shift Manager");
        Job_Progression[1].Add("Manager");

        // Web Developer
        Job_Progression[2].Add("IT Technician");
        Job_Progression[2].Add("Quality Assurance");
        Job_Progression[2].Add("Junior Developer");
        Job_Progression[2].Add("Developer");

        // Bank Teller
        Job_Progression[3].Add("Bank Teller");
        Job_Progression[3].Add("Banker");
        Job_Progression[3].Add("Service Manager");
        Job_Progression[3].Add("Branch Manager");

        // Salesman
        Job_Progression[4].Add("Salesman");
        Job_Progression[4].Add("Account Manager");
        Job_Progression[4].Add("Senior Manager");
        Job_Progression[4].Add("COO");

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

    private void Populate_Salaries()
    {
        // Barista
        Salaries.Add("Barista", 8);
        Salaries.Add("Shift Manager", 10);
        Salaries.Add("Store Manager", 14);
        Salaries.Add("Regional Manager", 18);

        // Hostess
        Salaries.Add("Hostess", 12);
        Salaries.Add("Server", 15);
        Salaries.Add("Restaraunt Shift Manager", 14);
        Salaries.Add("Manager", 20);

        // IT
        Salaries.Add("IT Technician", 20);
        Salaries.Add("Quality Assurance", 25);
        Salaries.Add("Junior Developer", 30);
        Salaries.Add("Developer", 40);

        // Bank Teller
        Salaries.Add("Bank Teller", 15);
        Salaries.Add("Banker", 18);
        Salaries.Add("Service Manager", 20);
        Salaries.Add("Branch Manager", 25);

        // Salesman
        Salaries.Add("Salesman", 30);
        Salaries.Add("Account Manager", 35);
        Salaries.Add("Senior Manager", 40);
        Salaries.Add("COO", 50);

        // Software Developer
        Salaries.Add("Software Developer", 50);
        Salaries.Add("Senior Developer", 60);
        Salaries.Add("Project Lead", 70);
        Salaries.Add("Project Manager", 100);

        // Assistant College Professor
        Salaries.Add("Assistant College Professor", 4000);
        Salaries.Add("Associate College Professor", 4500);
        Salaries.Add("College Professor", 5000);
        Salaries.Add("Dean", 8000);

        // General Practitioner
        Salaries.Add("General Practitioner", 8000);
        Salaries.Add("Medical Specialist", 10000);
        Salaries.Add("Surgeon", 11000);
        Salaries.Add("Cheif of Staff", 13000);

        // Aerospace Engineer
        Salaries.Add("Aerospace Engineer", 10000);
        Salaries.Add("Senior Engineer", 12000);
        Salaries.Add("Flight Crew", 15000);
        Salaries.Add("NASA Administrator", 18000);
    }

    public void Promotion()
    {
        int new_wage;
        string job_name = (string)Job_Progression[Job_Id][Game_Manager.instance.Player.Job_Level];
        Salaries.TryGetValue(job_name, out new_wage);
        Game_Manager.instance.Player.Player_Job.Name = (string)Job_Progression[Job_Id][Game_Manager.instance.Player.Job_Level];
        Game_Manager.instance.Player.Player_Job.Hourly_Wage = new_wage;
    }
}
