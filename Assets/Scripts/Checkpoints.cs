using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoints : MonoBehaviour
{
    public GameObject[] checkpoints;
    public int passed;
    public Text CPText;
    public Text anyKeyText;
    public static bool validLap;
    public static bool raceIsGo;
    public bool countdownIsRunning;
    Animator anim;
    int x = 3;

    void Start()
    {
        validLap = true;
        passed = 0;
        anim = CPText.GetComponent<Animator>();
        raceIsGo = false;
        countdownIsRunning = false;
        anyKeyText.enabled = true;
    }

    void FixedUpdate()
    {
        if(Input.anyKeyDown && !raceIsGo)
        {
            anyKeyText.enabled = false;
            StartCountdown();
        }
    }

    void StartCountdown()
    {
        countdownIsRunning = true;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        updateCountdown(x);
        x--;
        if (x == 0)
        {
            raceIsGo = true;
        }
        yield return new WaitForSeconds(1);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit Something");
        if (other.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint triggered");
            if (other.name == (passed + 1).ToString())
            {
                Debug.Log("Calling passCheckpoint()");
                passCheckpoint();
            }
            else { }
        }
        else { }
    }

    void passCheckpoint()
    {
        validLap = false;
        Debug.Log("Executing passCheckpoint()");
        passed++;
        checkFull();
        updateText();
        if (passed == checkpoints.Length)
        {
            passed = 0;
        }
    }

    void checkFull()
    {
        if(passed == checkpoints.Length)
        {
            validLap = true;
        }
    }

    void updateText()
    {
        CPText.text = "Checkpoint " + passed + "/" + checkpoints.Length;
        anim.speed = 1;
        anim.SetTrigger("Triggered");
    }

    void updateCountdown(int n)
    {
        CPText.text = n.ToString();
        anim.speed = 1;
        anim.SetTrigger("Triggered");
    }
}
