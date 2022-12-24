using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    #region Singleton
    public static TimeManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one TimeManager");
            return;
        }

        instance = this;
    }
    #endregion
    public float globalSpeed = 1f;

    public GameObject pp;
    public void DoSlowMo()
    {
        globalSpeed = 0.2f;
        pp.SetActive(true);
    }

    public void RestoreNormalTime()
    {
        globalSpeed = 1f;
        pp.SetActive(false);
    }
}
