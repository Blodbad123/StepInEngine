using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class WebService : MonoBehaviour
{
    private List<Texture2D> imgs;
    public List<Titel> titels;
    
    private const string PathApp = "http://localhost:80/api/app";
    private const string PathImg = "http://localhost:80/api/img/";

    public string data = "{\"devices\":\"iphone\",\"version\":\"99\",\"locales\":\"danish\",\"require\":[\"accelerometer\"]}";

    public WebService instance = null;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        titels = new List<Titel>();
        StartCoroutine(RequestTitles(PathApp, data));
    }
    
    private IEnumerator RequestTitles(string path, string data)
    {
        for (int attemt = 0, attemts = 3; attemt < attemts; attemt++)
        {
            using (UnityWebRequest www = UnityWebRequest.Put(path, data))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogError(www.error);                    
                }
                else
                {
                    foreach (JSONNode title in JSON.Parse(www.downloadHandler.text)["titles"].Values)
                    {
                        imgs = new List<Texture2D>();
                        foreach (var img in title["screenshots"])
                        {       
                            yield return StartCoroutine(GetImage(PathImg, img.Value));
                        }

                        var descriptions = GetDescriptionsFromTitel(title);
                        var titleTranslations = GetTitleTranslationsFromTitel(title);
                        
                        if (imgs != null)
                        {
                            titels.Add(new Titel(imgs, descriptions, titleTranslations));
                        } 
                    }

                    print(titels.Count);
                    yield break;
                }
            }
        }
        
        Debug.LogError("Timeout", this);
    }

    private static Dictionary<string, string> GetTitleTranslationsFromTitel(JSONNode title)
    {
        var tmp = new Dictionary<string, string>();
        var values = new List<string>();
        var keys = new List<string>();

        JSONNode.ValueEnumerator titleTranslations = title["titles"].Values;
        JSONNode.KeyEnumerator keysE = title["titles"].Keys;

        foreach (JSONNode node in keysE)
        {
            keys.Add(node);
        }

        foreach (JSONNode titleTranslation in titleTranslations)
        {
            values.Add(titleTranslation);
        }

        for (int i = 0; i < values.Count; i++)
        {
            tmp.Add(keys[i], values[i]);
        }

        return tmp;
    }

    private static Dictionary<string, string> GetDescriptionsFromTitel(JSONNode title)
    {
        var tmp = new Dictionary<string, string>();
        var values = new List<string>();
        var keys = new List<string>();
        JSONNode.ValueEnumerator descriptions = title["descriptions"].Values;
        JSONNode.KeyEnumerator keysE = title["descriptions"].Keys;

        foreach (JSONNode node in keysE)
        {
            keys.Add(node);
        }
        
        foreach (JSONNode description in descriptions)
        {
            values.Add(description);
        }

        for (int i = 0; i < values.Count; i++)
        {
            tmp.Add(keys[i], values[i]);
        }

        return tmp;
    }

    private IEnumerator GetImage(string path, string img)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(path + img))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture) www.downloadHandler).texture;
                imgs.Add(texture);
            }
        }
    }
}