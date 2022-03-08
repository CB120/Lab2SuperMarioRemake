using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //------Timer------
    [SerializeField] private Text timerText;
    [SerializeField] private int timeLeft = 400;
    [SerializeField] private GameObject Mario;
    [SerializeField] private MarioStateController marioStateController;


    void Start()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
        marioStateController = Mario.GetComponent<MarioStateController>();
        StartTimer();
    }



    /*==================
        Timer Functions
    ==================*/
    private void DecrementTime()
    {
        if (timeLeft > 0)
        {
            timerText.text = "Time" + "\n" + timeLeft.ToString();
            timeLeft--;
        }
        else
        {
            CancelInvoke();
            timeLeft = 0;
            timerText.text = "Time" + "\n" + timeLeft.ToString();
            marioStateController.MarioIsDead();
        }
    }


    void StartTimer()
    {
        InvokeRepeating("DecrementTime", 0f, 0.1f);
    }

    public void PlayerIsDead()
    {
        CancelInvoke();
        Invoke("RestartScene", 5f);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
