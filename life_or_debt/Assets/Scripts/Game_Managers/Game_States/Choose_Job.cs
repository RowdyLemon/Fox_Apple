using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Choose_Job : MonoBehaviour {

    public GameObject b_degree;
    public GameObject m_degree;
    public GameObject d_degree;
    public GameObject choose;

	// Use this for initialization
	void Start ()
    {
        b_degree.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        m_degree.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        d_degree.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        choose.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Choose_Job_Screen()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.CHOOSE_JOB;
        Game_Manager.instance.scene_loaded = false;
        Game_Manager.instance.Player.Degree = EventSystem.current.currentSelectedGameObject.name;
        switch (Game_Manager.instance.Player.Degree)
        {
            case "Bachelor's":
                Game_Manager.instance.Player.Debt -= 40000;
                Game_Manager.instance.Player.Student_Loan += 40000;
                break;
            case "Master's":
                Game_Manager.instance.Player.Debt -= 80000;
                Game_Manager.instance.Player.Student_Loan += 80000;
                break;
            default:
                Game_Manager.instance.Player.Debt -= 160000;
                Game_Manager.instance.Player.Student_Loan += 160000;
                break;
        }
    }
}
