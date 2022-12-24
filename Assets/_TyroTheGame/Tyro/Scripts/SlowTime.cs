using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowTime : MonoBehaviour
{
    private bool slowMo = false;
    private float stopSlowMoTime;
    public float maxSlowMoTime = 2f;

    public GameObject tyroCanvas;
    public Slider manaBar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            slowMo = !slowMo;
            if (slowMo) //It slowmo has to be done
            {
                StartSlowMo();
                stopSlowMoTime = Time.time + maxSlowMoTime;
            }
            else
            {
                StopSlowMo();
            }
            //Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
            Debug.Log(stopSlowMoTime);
        }

        if (slowMo)
        {
            calculateBar();
            if (Time.time >= stopSlowMoTime)
            {
                StopSlowMo();
            }
        }
        else
        {
            manaBar.value = 1;
        }
    }

    private void calculateBar()
    {
        manaBar.value = (stopSlowMoTime - Time.time) / maxSlowMoTime;
    }


    private void StopSlowMo()
    {
        slowMo = false;
        TimeManager.instance.RestoreNormalTime();
    }

    private void StartSlowMo()
    {
        slowMo = true;
        TimeManager.instance.DoSlowMo();
    }
}
