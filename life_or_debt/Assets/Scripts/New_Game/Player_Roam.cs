using UnityEngine;
using System.Collections;

public class Player_Roam : MonoBehaviour
{

    private float Speed = 0.25f;
    private Vector3 wayPoint;
    private float xmin;
    private float xmax;
    private float ymin;
    private float ymax;
    public int radius;

    private float time_change;

    void Start()
    {
        time_change = 0;

        if (Game_Manager.instance.Player.Player_House.Min_Payment == 800)
        {
            xmin = -3.59f;
            xmax = -0.4f;
            ymin = -0.24f;
            ymax = 2.27f;
        }
        else if (Game_Manager.instance.Player.Player_House.Min_Payment == 1200)
        {
            xmin = -3.63f;
            xmax = -0.74f;
            ymin = -0.21f;
            ymax = 2.32f;
        }
        else
        {
            xmin = -3.65f;
            xmax = -0.78f;
            ymin = -0.18f;
            ymax = 2.33f;
        }

        //xmin = -4f;
        //xmax = -1.6f;
        //ymin = -1.0f;
        //ymax = 1.5f;
        Wander();
    }

    void Update()
    {
        if(Time.time >= time_change)
        {
            Wander();
            time_change = Time.time + 15;
        }
        transform.Translate(wayPoint * Speed * Time.deltaTime);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 89.5f);
        if(transform.localPosition.x >= xmax || transform.localPosition.x <= xmin)
        {
            wayPoint = new Vector3(-wayPoint.x, wayPoint.y, wayPoint.z);
        }
        if (transform.localPosition.y >= ymax || transform.localPosition.y <= ymin)
        {
            wayPoint = new Vector3(wayPoint.x, -wayPoint.y, wayPoint.z);
        }
        float xclamp = Mathf.Clamp(transform.localPosition.x, xmin, xmax);
        float yclamp = Mathf.Clamp(transform.localPosition.y, ymin, ymax);
        transform.localPosition = new Vector3(xclamp, yclamp, transform.localPosition.z);
    }

    private void Wander()
    {
        float x = Random.Range(xmin, xmax);
        float y = Random.Range(ymin, ymax);
        wayPoint = new Vector3(x, y, 89.5f);
        Debug.Log(wayPoint + " and " + (transform.localPosition - wayPoint).magnitude);
    }
}
