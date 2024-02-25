using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Score : MonoBehaviour
{

    public static float score = 0;
    public static float Oldscore = 0;

    private bool qteKey = true;
    private Animator anime;
    private Rigidbody2D rb;
    private BoxCollider2D call;

    private float timeStart = 0;
    private int target = 10;
    private int pass = 0;
    
    public static Func<float, float> onQte;


    [SerializeField] private Text countTxt; 

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        call = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (anime.GetInteger("State") == 2 && collision.gameObject.CompareTag("Carrot"))
        {
            score += 1f;
        }
        else if (anime.GetInteger("State") == 4 && collision.gameObject.CompareTag("Potato"))
        {
            score += 5f;
        }
        else if (anime.GetInteger("State") == 5 && collision.gameObject.CompareTag("WallPaper"))
        {
            score += 11f;
        }
        
        countTxt.text = "–екорд:" + Oldscore +"\n" +"—чет:" + (int)score;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("death"))
        {
            if (Oldscore < score)
            {
                Oldscore = score;
            }
            score = 0f;
        }
    }


    public float getScore()
    {
        return score;
    }

    public void setScore(float newScore)
    {
        score = newScore;
    }


}
