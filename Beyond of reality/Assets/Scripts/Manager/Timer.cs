using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        string min = ((int)t/60).ToString();
        string sec = (t%60).ToString("F2");

        if(Input.GetKey(KeyCode.U)){
           timerText.text = min+","+sec;
        }
    }
}
