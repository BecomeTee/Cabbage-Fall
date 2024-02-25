using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QTE : MonoBehaviour
{
    private Rigidbody2D rb;
    //qte
    int complite = 0;
    public bool qteDone = false;
    //Timer
    public bool TimerOn = false;
    public float TimeLeft;
    public bool timerDone = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                Debug.Log(TimeLeft);
            }
            else
            {
                //Debug.Log("Time is up");
                TimeLeft = 0;
                TimerOn = !TimerOn;
                timerDone = true;
            }
        }    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("QTEWorm") && !qteDone)
        {
            StartCoroutine(Clicer(10, 5, "f"));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("QTEWorm") && qteDone)
        {
            qteDone = !qteDone;
        }
    }

    public IEnumerator Clicer(float TimeLeft, int timesTap, string _key)
    {
        rb.bodyType = RigidbodyType2D.Static; 
        this.TimeLeft = TimeLeft;
        TimerOn = true;
        complite = 0;

        while (complite != timesTap && !timerDone)
        {
            if (Input.GetKeyDown(_key))
            {
                complite++;
            }
            yield return 0;
        }

        TimerOn = false;
        
        if (timerDone)
        {
            Debug.Log("Не успел");
        }
        else
        {
            Debug.Log("Успел");
        }

        qteDone = !qteDone;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
