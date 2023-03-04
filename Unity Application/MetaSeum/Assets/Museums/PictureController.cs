using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Data
{
    public string metadataUrl;
    public string creatorAddress;
}
public class PictureController : MonoBehaviour
{
    public int id;
    public GameObject Background;
    public TMP_Text Artist_id, Art_name, Art_desc;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        ApproachEvent.current.onGrab += OnGrabObject;
        ApproachEvent.current.onUnGrab += OnUnGrabObject;
        Background = transform.Find("Background").gameObject;
        Artist_id= transform.Find("Background/CreatorAddress").GetComponent<TMP_Text>();
        Art_name = transform.Find("Background/ArtworkName").GetComponent<TMP_Text>();
        Art_desc = transform.Find("Background/ArtworkDescription").GetComponent<TMP_Text>();
        button = transform.Find("Background/Buy").GetComponent<Button>();
        button.onClick.AddListener(OnPurchaseNFT);
    }
    public void OnPurchaseNFT()
    {
        button.transform.GetComponentInChildren<TMP_Text>().text = "Sold out";
        button.interactable = false;
    }
    // Update is called once per frame
    public void OnGrabObject(int i)
    {
        if (id == i)
        {
            Background.SetActive(true);
        }
    }
    public void OnUnGrabObject(int i)
    {
        if (id == i)
        {
            Background.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator request(string imagehash)
    {
        var user = new Data();
        user.metadataUrl = imagehash;
        user.creatorAddress = "0x351Ac7e94d0e4f2bBE5DAC2d469B91e7725f8078";

        string json = JsonUtility.ToJson(user);
        var req = new UnityWebRequest("https://leetvision.lz9.in/api/v1.0/delegateNFTCreation", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.timeout = 10;
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
