using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Camera camera;
    public GameObject Laser;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (GameObject nftui in GameObject.FindGameObjectsWithTag("NFTUI"))
        {
            if (nftui.name == "Background")
            {
                nftui.GetComponent<Canvas>().worldCamera = camera;
                nftui.GetComponent<OVRRaycaster>().pointer = Laser;
                nftui.SetActive(false);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
