using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;
public class NFTLoader : MonoBehaviour
{
    public string apikey = "", wallet = "";
    public JSONObject resp;
    // Start is called before the first frame update
    void Awake()
    {
        WWW req = new WWW("https://polygon-mainnet.g.alchemy.com/nft/v2/" + apikey + "/getNFTs?owner=" + wallet);
        StartCoroutine(RestCall(req));
    }

    public IEnumerator RestCall(WWW req)
    {
        yield return req;
        if (req.text.Length >= 5)
        {
            resp = new JSONObject(req.text);
            Debug.Log(resp);
            initialize(resp);
        }
    }

    public void initialize(JSONObject a)
    {
        var total_count = int.Parse(a.GetField("totalCount").ToString());
        if (total_count > 0)
        {
            var nfts = a.GetField("ownedNfts");
            var imagescount = 0;
            for (int j = 0; j < nfts.count; j++)
            {
                var nft = nfts[j];
                var image_type = "";
                if (nft.GetField("media")[0].HasField("format"))
                {
                    image_type = nft.GetField("media")[0].GetField("format").ToString().Replace("\"", "");
                }
                if (image_type == "png" || image_type == "jpeg")
                {
                    imagescount++;
                }
            }
            print(imagescount);
            MuseumManager.current.initialize(imagescount);
            //var paintingsboxes = this.GetComponentsInChildren<TriggerArea>();
            //var min = Mathf.Min(total_count, paintingsboxes.Length);
            var pictureStands = MuseumManager.current.pictureStands;
            int i = 1;
            for (int k = 0; k < pictureStands.Count; k++)
            {
                for (int j = i-1; j < nfts.count; j++)
                {
                    var nft = nfts[j];
                    var image_uri = nft.GetField("media")[0].GetField("gateway").ToString().Replace("\"", "");
                    var image_name = nft.GetField("title").ToString().Replace("\"", "");
                    var image_description = nft.GetField("description").ToString().Replace("\"", "");
                    var artistAddress = nft.GetField("contract").GetField("address").ToString().Replace("\"", "");
                    var image_type = "";
                    if (nft.GetField("media")[0].HasField("format"))
                    {
                        image_type = nft.GetField("media")[0].GetField("format").ToString().Replace("\"", "");
                    }
                    print(image_type);
                    if (image_type == "png" || image_type == "jpeg")
                    {
                        var pictures = pictureStands[k].GetComponent<PictureStandManager>().pictures;
                        foreach (var picture in pictures)
                        {
                            var triggeri = picture.GetComponent<TriggerArea>().i;
                            if (triggeri==i)
                            {

                                var mat = picture.GetComponent<Renderer>().material;
                                StartCoroutine(pictureStands[k].GetComponent<PictureStandManager>().GetText(image_uri.Replace("\"",""), mat));
                                picture.GetComponent<PictureController>().Artist_id.text = artistAddress;
                                picture.GetComponent<PictureController>().Art_name.text = image_name;
                                picture.GetComponent<PictureController>().Art_desc.text = image_description;
                                i++;
                                break;
                            }
                        }
                        imagescount++;
                    }
                }

            }
            //    var nft = nfts[j];
            //    var image_uri = nft.GetField("media")[0].GetField("gateway").ToString();
            //    var image_type = "";
            //    if (nft.GetField("media")[0].HasField("format"))
            //    {
            //        image_type = nft.GetField("media")[0].GetField("format").ToString().Replace("\"", "");
            //    }
            //    if (image_type == "png" || image_type == "jpeg")
            //    {
            //        for (int k = 0; k < paintingsboxes.Length; k++)
            //        {
            //            var triggerarea = paintingsboxes[k];
            //            if (triggerarea.i == i + 1)
            //            {
            //                var gameobject = triggerarea.gameObject;
            //                var pictureloader = gameobject.GetComponent<PictureLoading>();
            //                pictureloader.url = image_uri.Replace("\"", "");
            //                StartCoroutine(pictureloader.GetText());
            //                i++;
            //                break;
            //            }

            //        }
            //    }
            //}
        }
    }
}
