using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool timerEnd = false;
    //public PlayerMove a;

    public IEnumerator RunTimer(float time)
    {

        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            Debug.Log(time);
        }

    }
}
