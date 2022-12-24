using UnityEngine;

public class SlowTimeSkill : MonoBehaviour
{

    private bool slowMo = false;
    private float stopSlowMoTime;
    public float maxSlowMoTime = 2f;
    
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
        }

        if (slowMo)
        {
            if (Time.time >= stopSlowMoTime)
            {
                StopSlowMo();
            }
        }

        
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

