using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPcheck : MonoBehaviour
{
    public int lapCount = 0;
    public int CPnum = 0;
    public int CPforLap = 3;
    public int lapCap = 5;


    private float LapTimerS = 0;
    private float LapTimerM = 0;

    public Text CPText;
    public Text LapText;
    public Text Timer;
    public Text RestartText;
    public Text finalText;
    //CPS
    public GameObject CP1;
    public GameObject CP2;
    public GameObject CP3;
    public GameObject Car;
    public GameObject CP4;

    public Transform resPoint;
    //vehicle

    public Transform CPchecker;
    public float CPfloat;
    public LayerMask CPMask;

    bool CPpassed;
    private bool hasStarted = false;
    /*private void OnTriggerEnter(Collider other)
    {
        GameObject CPC = GameObject.FindGameObjectWithTag("CP");
        Collider CPCollider = CPC.GetComponent<Collider>();
        if (other == CPCollider)
        {
            lapCount += 1;
        }
    }*/

    void Update()
    {
        CPpassed = Physics.CheckSphere(CPchecker.position, CPfloat, CPMask);
        if(CPpassed)
        {
            CPnum += 1;
            CPpassed = false;
        }
        if(CPnum == 0)
        {
            CP1.GetComponent<BoxCollider>().enabled = true;
            CP2.GetComponent<BoxCollider>().enabled = false;
            CP3.GetComponent<BoxCollider>().enabled = false;
            CP4.GetComponent<BoxCollider>().enabled = false;
        }
        if(CPnum == 1)
        {
            CP1.GetComponent<BoxCollider>().enabled = false;
            CP2.GetComponent<BoxCollider>().enabled = true;
            CP3.GetComponent<BoxCollider>().enabled = false;
            CP4.GetComponent<BoxCollider>().enabled = false;
        }
        if (CPnum == 2)
        {
            CP1.GetComponent<BoxCollider>().enabled = false;
            CP2.GetComponent<BoxCollider>().enabled = false;
            CP3.GetComponent<BoxCollider>().enabled = true;
            CP4.GetComponent<BoxCollider>().enabled = false;
        }
        if (CPnum == 3)
        {
            CP1.GetComponent<BoxCollider>().enabled = false;
            CP2.GetComponent<BoxCollider>().enabled = false;
            CP3.GetComponent<BoxCollider>().enabled = false;
            CP4.GetComponent<BoxCollider>().enabled = true;
        }
        if (CPnum == CPforLap)
        {
            CP4.GetComponent<BoxCollider>().enabled = false;
            CPnum = 0;
            lapCount += 1;
        }

        CPText.text = "CP: " + CPnum + "/" + (CPforLap - 1);
        LapText.text = "Lap: " + lapCount + "/" + lapCap;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            hasStarted = true;
        }
        if(lapCount != lapCap && hasStarted)
        {
            
            LapTimerS += Time.deltaTime % 60;
            if(LapTimerS >= 60)
            {
                LapTimerM += 1;
                LapTimerS = 0;
            }
            Timer.text = LapTimerM.ToString("00") + ":" + LapTimerS.ToString("00");
        }
        if (lapCount == lapCap)
        {
            Car.GetComponent<Movement>().enabled = false;
            RestartText.text = "Press 'R' to restart";
            finalText.text = "Fianl Time " + LapTimerM.ToString("00") + ":" + LapTimerS.ToString("00");
            if (Input.GetKeyUp(KeyCode.R))
            {
                CPnum = 0;
                lapCount = 0;
                transform.position = resPoint.position;
                Car.GetComponent<Movement>().enabled = true;
                LapTimerM = 0;
                LapTimerS = 0;
            }
        }
        if (lapCount != lapCap)
        {
            RestartText.text = "";
            finalText.text = "";
        }
    }
}
