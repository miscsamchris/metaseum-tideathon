using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachEvent : MonoBehaviour
{
    public static ApproachEvent current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }
    public event Action<int> onGrab;
    public void OnGrabGenerateNFT(int i)
    {
        if (onGrab != null)
        {
            onGrab(i);
        }
    }

    public event Action<int> onUnGrab;
    public void OnUnGrabDoNothing(int i)
    {
        if (onUnGrab != null)
        {
            onUnGrab(i);
        }
    }
}
