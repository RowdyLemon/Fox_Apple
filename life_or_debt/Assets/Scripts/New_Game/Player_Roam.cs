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
        //xmin = transform.localPosition.x - 1.8f;
        //xmax = transform.localPosition.x + 2.0f;
        //ymin = transform.localPosition.y - 1.5f;
        //ymax = transform.localPosition.y + 1.0f;
        xmin = -4f;
        xmax = -1.6f;
        //xmax = 3.8f;
        ymin = -1.0f;
        //ymin = -0.2f;
        ymax = 1.5f;
        Wander();
    }

    void Update()
    {
        if(Time.time >= time_change)
        {
            Wander();
            time_change = Time.time + 1/*Random.Range(0.5f, 1.5f)*/;
        }
        transform.Translate(wayPoint * Speed * Time.deltaTime);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 89.5f);
        if(transform.localPosition.x >= xmax || transform.localPosition.x <= xmin)
        {
            wayPoint = new Vector3(-wayPoint.x, wayPoint.y, wayPoint.z);
            //Wander();
        }
        if (transform.localPosition.y >= ymax || transform.localPosition.y <= ymin)
        {
            wayPoint = new Vector3(wayPoint.x, -wayPoint.y, wayPoint.z);
            //Wander();
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
