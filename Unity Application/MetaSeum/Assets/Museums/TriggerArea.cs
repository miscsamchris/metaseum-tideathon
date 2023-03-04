using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int i;
    private void OnTriggerEnter(Collider other)
    {
        ApproachEvent.current.OnGrabGenerateNFT(i);
    }

    private void OnTriggerExit(Collider other)
    {
        ApproachEvent.current.OnUnGrabDoNothing(i);
    }
}
