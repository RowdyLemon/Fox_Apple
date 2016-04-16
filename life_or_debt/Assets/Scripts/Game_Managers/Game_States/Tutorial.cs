using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject top;
    public GameObject back;

    public GameObject name_bubble;
    public GameObject debt_bubble;
    public GameObject store_bubble;
    public GameObject bank_bubble;
    public GameObject time_bubble;
    public GameObject work_bubble;
    public GameObject go_out_bubble;
    public GameObject fridge_bubble;
    public GameObject tv_couch_bubble;
    public GameObject bed_bubble;
    public GameObject bar_bubble;

    private Vector2 player;
    private Vector2 debt;
    private Vector2 store;
    private Vector2 bank;
    private Vector2 time;
    private Vector2 work;
    private Vector2 go_out;
    private Vector2 fridge;
    private Vector2 tv_couch;
    private Vector2 bed;
    private Vector2 bar;

    private Vector2 off_screen;

    void Start()
    {
        font_init();

        off_screen = new Vector2(1000, 1000);
        player = name_bubble.transform.localPosition;
        debt = debt_bubble.transform.localPosition;
        store = store_bubble.transform.localPosition;
        bank = bank_bubble.transform.localPosition;
        time = time_bubble.transform.localPosition;
        work = work_bubble.transform.localPosition;
        go_out = go_out_bubble.transform.localPosition;
        fridge = fridge_bubble.transform.localPosition;
        tv_couch = tv_couch_bubble.transform.localPosition;
        bed = bed_bubble.transform.localPosition;
        bar = bar_bubble.transform.localPosition;

        debt_bubble.transform.localPosition = off_screen;
        store_bubble.transform.localPosition = off_screen;
        bank_bubble.transform.localPosition = off_screen;
        time_bubble.transform.localPosition = off_screen;
        work_bubble.transform.localPosition = off_screen;
        go_out_bubble.transform.localPosition = off_screen;
        fridge_bubble.transform.localPosition = off_screen;
        tv_couch_bubble.transform.localPosition = off_screen;
        bed_bubble.transform.localPosition = off_screen;
        bar_bubble.transform.localPosition = off_screen;
    }

    private void font_init()
    {
        top.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        back.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size;

        name_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        name_bubble.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size;

        debt_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        debt_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        store_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        store_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        bank_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        bank_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        time_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        time_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        work_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        work_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        go_out_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        go_out_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        fridge_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        fridge_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        tv_couch_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        tv_couch_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        bed_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        bed_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;

        bar_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
        bar_bubble.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size;
    }

    void Update()
    {

    }

	public void go_back()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.START;
        Game_Manager.instance.scene_loaded = false;
    }

    public void next_button(string s)
    {
        switch(s)
        {
            case "name":
                name_bubble.transform.localPosition = off_screen;
                debt_bubble.transform.localPosition = debt;
                break;
            case "debt":
                debt_bubble.transform.localPosition = off_screen;
                store_bubble.transform.localPosition = store;
                break;
            case "store":
                store_bubble.transform.localPosition = off_screen;
                bank_bubble.transform.localPosition = bank;
                break;
            case "bank":
                bank_bubble.transform.localPosition = off_screen;
                time_bubble.transform.localPosition = time;
                break;
            case "time":
                time_bubble.transform.localPosition = off_screen;
                work_bubble.transform.localPosition = work;
                break;
            case "work":
                work_bubble.transform.localPosition = off_screen;
                go_out_bubble.transform.localPosition = go_out;
                break;
            case "go_out":
                go_out_bubble.transform.localPosition = off_screen;
                fridge_bubble.transform.localPosition = fridge;
                break;
            case "fridge":
                fridge_bubble.transform.localPosition = off_screen;
                tv_couch_bubble.transform.localPosition = tv_couch;
                break;
            case "tv_couch":
                tv_couch_bubble.transform.localPosition = off_screen;
                bed_bubble.transform.localPosition = bed;
                break;
            case "bed":
                bed_bubble.transform.localPosition = off_screen;
                bar_bubble.transform.localPosition = bar;
                break;
            case "bar":
                bar_bubble.transform.localPosition = off_screen;
                Game_Manager.instance.current_state = Game_Manager.Game_States.START;
                Game_Manager.instance.scene_loaded = false;
                break;
        }
    }

}
