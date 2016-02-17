using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Choose_Job : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
                Game_Manager.instance.Player.Debt += 40000;
                break;
            case "Master's":
                Game_Manager.instance.Player.Debt += 80000;
                break;
            default:
                Game_Manager.instance.Player.Debt += 160000;
                break;
        }
    }
}
