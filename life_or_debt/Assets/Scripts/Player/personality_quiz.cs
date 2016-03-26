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

    private int happy;
    private int sad;

    private int rest;
    private int lazy;

    private int thrifty;
    private int thriftless;

    public GameObject question_1;
    public GameObject question_2;
    public GameObject question_3;
    public GameObject question_4;
    public GameObject result;

    private Vector2 off_screen;
    private Vector2 on_screen;

    void Start()
    {
        happy = 0;
        sad = 0;
        rest = 0;
        lazy = 0;
        thrifty = 0;
        thriftless = 0;
        off_screen = new Vector2(1000, 1000);
        on_screen = question_1.transform.localPosition;
        question_2.transform.localPosition = off_screen;
        question_3.transform.localPosition = off_screen;
        question_4.transform.localPosition = off_screen;
        result.transform.localPosition = off_screen;
    }

    public void Question_1_Submit()
    {
        IEnumerable toggled = question_1.GetComponentInChildren<ToggleGroup>().ActiveToggles();
        foreach (Toggle t in toggled)
        {
            switch (t.name) {
                case "Option_1":
                    lazy += 5;
                    break;
                case "Option_2":
                    lazy += 2;
                    break;
                case "Option_3":
                    rest += 2;
                    break;
                case "Option_4":
                    rest += 5;
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
                    thriftless += 5;
                    break;
                case "Option_2":
                    thriftless += 2;
                    break;
                case "Option_3":
                    thrifty += 2;
                    break;
                case "Option_4":
                    thrifty += 5;
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
                    lazy += 5;
                    break;
                case "Option_2":
                    lazy += 2;
                    break;
                case "Option_3":
                    rest += 2;
                    break;
                case "Option_4":
                    rest += 5;
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
                    rest += 5;
                    break;
                case "Option_2":
                    happy += 6;
                    lazy += 1;
                    break;
                case "Option_3":
                    happy += 2;
                    rest += 2;
                    break;
                case "Option_4":
                    thrifty += 5;
                    break;
            }
        }
        question_4.transform.localPosition = off_screen;
        Results();
        result.transform.localPosition = on_screen;
    }

    private void Results()
    {
        result.GetComponentsInChildren<Text>()[1].text = "Happy: " + happy;
        result.GetComponentsInChildren<Text>()[2].text = "Sad: " + sad;
        result.GetComponentsInChildren<Text>()[3].text = "Work Ethic: " + rest;
        result.GetComponentsInChildren<Text>()[4].text = "Laziness: " + lazy;
        result.GetComponentsInChildren<Text>()[5].text = "Thriftyness: " + thrifty;
        result.GetComponentsInChildren<Text>()[6].text = "Thriftlessness: " + thriftless;
    }
}
