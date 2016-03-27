using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class personality_quiz : MonoBehaviour
{

    /*
        Question 1: You feel a little under the weather do you
        A) Sleep in and forget about the world?
        B) Call out sick (dont want to spread this around)?
        C) begrudgingly go (no money no food)?
        D) Slap on your underwear like its nobodys business and head to work!

        Question 2: Today is pay day! what do you do?
        A) MAKE IT RAIN!!!
        B) All work and no play makes jack a dull boy. Im going out for some fun!
        C) Put some of it asside. Im saving for that new game I want.
        D) Im no frivilous spender! Its all going into savings.

        Question 3: Your boss just drops a big project into your lap at the end of the day
        and expects it finished on his desk by tomorrow.
        A) Nah dog, my day ends at 5... 
        B) due tomorrow, do tomorrow!
        C) Ill do as much as I can but it might be a little late.
        D) Looks like Im pulling an all nighter 

        Question 4: Of these colors what's your favorite color?
        A) Red (physically active/sleep less)
        B) Yellow (Cheerful/happy)
        C) Green (Responsible/hard worker)
        D) Blue (not spontaneous/thrifty)
    */

    //private int happy;
    //private int sad;


    //private int rest;
    //private int lazy;

    //private int thrifty;
    //private int thriftless;

    private int mood;
    private int work_ethic;
    private int laziness;
    private int thriftyness;

    public GameObject title;
    public GameObject question_1;
    public GameObject question_2;
    public GameObject question_3;
    public GameObject question_4;
    public GameObject question_5;

    public GameObject result;

    private Vector2 off_screen;
    private Vector2 on_screen;

    void Start()
    {
        mood = 0;
        work_ethic = 0;
        laziness = 0;
        thriftyness = 0;
        off_screen = new Vector2(1000, 1000);
        on_screen = question_1.transform.localPosition;
        question_2.transform.localPosition = off_screen;
        question_3.transform.localPosition = off_screen;
        question_4.transform.localPosition = off_screen;
        question_5.transform.localPosition = off_screen;
        result.transform.localPosition = off_screen;
        font_init();
    }

    private void font_init()
    {
        //question 1
        title.GetComponent<Text>().fontSize = Game_Manager.instance.Font_Size * 2;
        question_1.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size/2;
        question_1.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_1.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_1.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_1.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_1.GetComponentsInChildren<Text>()[5].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        //question 2
        question_2.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_2.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_2.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_2.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_2.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_2.GetComponentsInChildren<Text>()[5].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        //question 3
        question_3.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_3.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_3.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_3.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_3.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_3.GetComponentsInChildren<Text>()[5].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        //question 4
        question_4.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_4.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_4.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_4.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_4.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_4.GetComponentsInChildren<Text>()[5].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        //question 5
        question_5.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_5.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_5.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_5.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_5.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        question_5.GetComponentsInChildren<Text>()[5].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        //Results
        result.GetComponentsInChildren<Text>()[0].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        result.GetComponentsInChildren<Text>()[1].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        result.GetComponentsInChildren<Text>()[2].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        result.GetComponentsInChildren<Text>()[3].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        result.GetComponentsInChildren<Text>()[4].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
        result.GetComponentsInChildren<Text>()[5].fontSize = Game_Manager.instance.Font_Size + Game_Manager.instance.Font_Size / 2;
    }

    public void Question_1_Submit()
    {
        IEnumerable toggled = question_1.GetComponentInChildren<ToggleGroup>().ActiveToggles();
        foreach (Toggle t in toggled)
        {
            switch (t.name) {
                case "Option_1":
                    laziness += 5;
                    mood -= 2;
                    break;
                case "Option_2":
                    laziness += 2;
                    break;
                case "Option_3":
                    work_ethic += 2;
                    mood -= 1;  
                    break;
                case "Option_4":
                    work_ethic += 5;
                    mood += 2;
                    break;
            }
        }

        question_1.transform.localPosition = off_screen;
        question_2.transform.localPosition = on_screen;
    }

    public void Question_2_Submit()
    {
        IEnumerable toggled = question_2.GetComponentInChildren<ToggleGroup>().ActiveToggles();
        foreach (Toggle t in toggled)
        {
            switch (t.name)
            {
                case "Option_1":
                    thriftyness -= 5;
                    mood += 1;
                    break;
                case "Option_2":
                    thriftyness -= 2;
                    mood += 2;
                    break;
                case "Option_3":
                    thriftyness += 2;
                    work_ethic += 1;
                    break;
                case "Option_4":
                    thriftyness += 5;
                    break;
            }
        }
        question_2.transform.localPosition = off_screen;
        question_3.transform.localPosition = on_screen;
    }

    public void Question_3_Submit()
    {
        IEnumerable toggled = question_3.GetComponentInChildren<ToggleGroup>().ActiveToggles();
        foreach (Toggle t in toggled)
        {
            switch (t.name)
            {
                case "Option_1":
                    laziness += 5;
                    work_ethic -= 3;
                    break;
                case "Option_2":
                    laziness += 2;
                    work_ethic -= 1;
                    break;
                case "Option_3":
                    work_ethic += 2;
                    laziness -= 2;
                    break;
                case "Option_4":
                    work_ethic += 5;
                    mood -= 3;
                    break;
            }
        }
        question_3.transform.localPosition = off_screen;
        question_4.transform.localPosition = on_screen;
    }

    public void Question_4_Submit()
    {
        IEnumerable toggled = question_4.GetComponentInChildren<ToggleGroup>().ActiveToggles();
        foreach (Toggle t in toggled)
        {
            switch (t.name)
            {
                case "Option_1":
                    work_ethic += 5;
                    break;
                case "Option_2":
                    mood += 6;
                    laziness += 1;
                    break;
                case "Option_3":
                    mood += 2;
                    work_ethic += 2;
                    break;
                case "Option_4":
                    thriftyness += 5;
                    break;
            }
        }
        question_4.transform.localPosition = off_screen;
        question_5.transform.localPosition = on_screen;
    }


    public void Question_5_Submit()
    {
        IEnumerable toggled = question_4.GetComponentInChildren<ToggleGroup>().ActiveToggles();
        foreach (Toggle t in toggled)
        {
            switch (t.name)
            {
                case "Option_1":
                    mood -= 4;
                    work_ethic += 1;
                    break;
                case "Option_2":
                    laziness -= 3;
                    work_ethic += 2;
                    break;
                case "Option_3":
                    thriftyness -= 2;
                    work_ethic += 2;
                    break;
                case "Option_4":
                    thriftyness -= 2;
                    work_ethic -= 1;
                    break;
            }
        }

        if (laziness < 0)
            laziness = 0;

        if (mood < 0)
            mood = 0;

        if (thriftyness < 0)
            thriftyness = 0;

        if (thriftyness > 10)
            thriftyness = 10;

        if (mood > 10)
            mood = 10;

        if (laziness > 10)
            laziness = 10;

        if (work_ethic > 10)
            work_ethic = 10;


        question_5.transform.localPosition = off_screen;
        Results();
        result.transform.localPosition = on_screen;
    }

    private void Results()
    {
        result.GetComponentsInChildren<Text>()[1].text = "Mood: " + mood;
        result.GetComponentsInChildren<Text>()[2].text = "Work ethic: " + work_ethic;
        result.GetComponentsInChildren<Text>()[3].text = "Laziness: " + laziness;
        result.GetComponentsInChildren<Text>()[4].text = "Thriftyness: " + thriftyness;

        base_trait Player_Trait = new base_trait(mood, work_ethic, laziness, thriftyness);
        Game_Manager.instance.Player.Player_Traits = Player_Trait;

    }

    public void next_scene()
    {
        Game_Manager.instance.current_state = Game_Manager.Game_States.CHOOSE_EDUCATION;
        Game_Manager.instance.scene_loaded = false;
    }
}
