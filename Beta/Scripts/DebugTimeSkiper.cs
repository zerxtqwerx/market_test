using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTimeSkiper : MonoBehaviour
{
    public DayNightManager dnm;
    public void En()
    {
        enabled = !enabled;
    }

    private void Update()
    {
        dnm.time += 60 * Time.deltaTime;
    }
}
