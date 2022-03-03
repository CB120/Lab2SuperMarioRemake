using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //------Timer------
    [SerializeField] private Text timerText;
    [SerializeField] private int timeLeft = 400;


    void Start()
    {
        StartTimer();
    }

  
    void Update()
    {
        
    }


    /*==================
        Timer Functions
    ==================*/
    private void DecrementTime()
    {
        timerText.text = "Time" + "\n" + timeLeft.ToString();
        timeLeft--;
    }


    void StartTimer()
    {
        InvokeRepeating("DecrementTime", 0f, 0.5f);
    }
}
