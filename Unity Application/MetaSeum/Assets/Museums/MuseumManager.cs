using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumManager : MonoBehaviour
{
    public List<GameObject> pictureStands = new List<GameObject>();
    public static MuseumManager current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }
    public void initialize(int numofNfts)
    {
        var gopictureStands = GameObject.FindGameObjectsWithTag("PictureStand");
        var n = (int)Mathf.Min(numofNfts,52) / 4;
        var nftcounter = 1;
        Debug.Log("N is "+n+ "numofNfts is "+ numofNfts);
        for (int i = 0; i < gopictureStands.Length; i++)
        {
            var pictureStand = gopictureStands[i];
            if ((i > n && numofNfts % 4!=0) || i >=n)
            {
                pictureStand.SetActive(false);
            }
            else
            {
                pictureStands.Add(pictureStand);
                pictureStand.SetActive(true);
                Debug.Log(i+" is "+pictureStand.transform.position.x + " ," + pictureStand.transform.position.y + " ," + pictureStand.transform.position.z);
                var standManager = pictureStand.GetComponent<PictureStandManager>();
                foreach (var picture in standManager.pictures)
                {
                    if (nftcounter<=numofNfts)
                    {
                        var trigger = picture.GetComponent<TriggerArea>();
                        var picturecontroller = picture.GetComponent<PictureController>();
                        trigger.i = nftcounter;
                        picturecontroller.id = nftcounter;
                        nftcounter++;
                    }
                    else
                    {
                        picture.SetActive(false);
                    }
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
