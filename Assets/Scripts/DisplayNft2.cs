using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;


[System.Serializable]
public class Response
{
    public string name;
    public string description;
    public string image;
    public string external_url;

    public List<Attribut> attributes = new List<Attribut>();
}


[System.Serializable]
public class Attribut
{
    public string key;
    public string trait_type;
    public string value;
}

public class DisplayNft2 : MonoBehaviour
{

    public List<NftInfo> nfts = new List<NftInfo>();

    public static DisplayNft2 Instance;

    public NftInfo prefabItem;

    public GameObject loader;

    public TextMeshProUGUI txtprojetFloor;

    public TextMeshProUGUI txtRarity;

    public List<TraitComponent> traits = new List<TraitComponent>();

    public List<NftAsset> assetsCache = new List<NftAsset>();

    public string chain = "ethereum";
    public string network = "mainnet";
    public string contract = "0xd668a2e001f3385b8bbc5a8682ac3c0d83c19122";
    public int lastToken = 9;

    RectTransform listParent;

    private void Awake()
    {        
        Instance = this;
    }

    void Start()
    {
        listParent = GetComponent<RectTransform>();
        //OnNftClick("0x3fe1a4c1481c8351e91b64d5c398b159de07cbc5");
        ImportNft();
    }

    // Update is called once per frame
    void Update()
    {

    }


    async void ImportNft()
    {
        for (int i = 0, length = listParent.childCount; i < length; i++)
        {
            Destroy(listParent.GetChild(i).gameObject);
        }


        for (int i = 0; i < lastToken; i++)
        {
            string uri = await ERC721.URI(chain, network, contract, i.ToString());

            //Debug.Log(uri);
            UnityWebRequest webRequest = UnityWebRequest.Get(uri);

            await webRequest.SendWebRequest();
            byte[] result = webRequest.downloadHandler.data;
            string dataJSON = Encoding.UTF8.GetString(result);
            Response data = JsonConvert.DeserializeObject<Response>(dataJSON);
            UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(data.image);
            await textureRequest.SendWebRequest();
            Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
            DisplayMetaDataV2(data, Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), UnityEngine.Vector2.zero), i == 0);

        }
    }


    string openSeaLink;

    public void OnNftClickV2(Response data)
    {
        txtprojetFloor.text = "jump to website: " + data.external_url;
        openSeaLink = data.external_url;
        if (data.attributes == null || data.attributes.Count <= 0)
            return;
        int j = 0;
        for (int i = 0, length = traits.Count; i < length; i++)
        {
            if (!traits[i])
                continue;

            if (data.attributes.Count > j && i >= j)
            {
                traits[i].gameObject.SetActive(true);
                traits[i].Display(data.attributes[j]);

                j++;
                continue;
            }
            if (traits[i])
                traits[i].gameObject.SetActive(false);
        }
    }

    public void OnLinkClick()
    {
        Application.OpenURL(openSeaLink);
    }

    public void OnNftClick(string contract)
    {
        NftAsset asset = AssetInCache(contract);
        if (asset != null)
        {
            DisplayMetaData(asset, contract);
        }
        else
        {
            StartCoroutine(DisplayOnSide(contract));
        }

    }

    public void OnNftClicssssk(string contract)
    {
        StartCoroutine(DisplayOnSide(contract));
    }

    public IEnumerator DisplayOnSide(string contract)
    {
        if (loader)
            loader.SetActive(true);
        UnityWebRequest webRequest1 = UnityWebRequest.Get("https://www.google.com");

        yield return webRequest1.SendWebRequest();

        while (!webRequest1.isDone) ;

        Debug.Log("Response: " + webRequest1.responseCode);
        if (webRequest1.responseCode == 200) //player has internet connection
        {
            Debug.Log("you have internet connection");

            UnityWebRequest webRequest = UnityWebRequest.Get("https://api.opensea.io/api/v1/asset/" + contract + "/1/");

            yield return webRequest.SendWebRequest();

            while (!webRequest.isDone) ;

            if (webRequest.responseCode == 200) //the information has been retrieved
            {
                byte[] result = webRequest.downloadHandler.data;
                string dataJSON = Encoding.UTF8.GetString(result);
                NftAsset asset = new NftAsset();
                asset = JsonConvert.DeserializeObject<NftAsset>(dataJSON);
                if (asset != null)
                {
                    assetsCache.Add(asset);
                    DisplayMetaData(asset, contract);
                }
            }

        }
        else
        {
            Debug.Log("please connect to internet");
        }
        if (loader)
            loader.SetActive(false);
    }

    public void DisplayMetaDataV2(Response asset, Sprite imageNft, bool first = false)
    {
        var item = Instantiate(prefabItem, listParent);
        item.InitNft(asset, imageNft);
        if (first)
            OnNftClickV2(asset);
        //if (asset == null)
        //    return;

        //if (asset.collection != null && asset.collection.stats != null)
        //{
        //    if (txtprojetFloor)
        //    {
        //        txtprojetFloor.gameObject.SetActive(true);
        //        txtprojetFloor.text = "Project floor: " + asset.collection.stats.floor_price + " ETH";
        //    }
        //}



        //if (txtRarity)
        //{
        //    txtRarity.gameObject.SetActive(true);
        //    txtRarity.text = "Rarity #: ";
        //}


        //if (asset.traits == null || asset.traits.Count <= 0)
        //    return;
        //int j = 0;
        //for (int i = 0, length = traits.Count; i < length; i++)
        //{
        //    if (!traits[i])
        //        continue;

        //    if (asset.traits.Count > j && i >= j)
        //    {
        //        traits[i].gameObject.SetActive(true);
        //        traits[i].Display(asset.traits[j]);

        //        j++;
        //        continue;
        //    }
        //    if (traits[i])
        //        traits[i].gameObject.SetActive(false);
        //}

        //CarComponent.Instance.DisplayInCar(contrat);

    }

    public void DisplayMetaData(NftAsset asset, string contrat)
    {
        //if (asset == null)
        //    return;

        //if (asset.collection != null && asset.collection.stats != null)
        //{
        //    if (txtprojetFloor)
        //    {
        //        txtprojetFloor.gameObject.SetActive(true);
        //        txtprojetFloor.text = "Project floor: " + asset.collection.stats.floor_price + " ETH";
        //    }
        //}



        //if (txtRarity)
        //{
        //    txtRarity.gameObject.SetActive(true);
        //    txtRarity.text = "Rarity #: ";
        //}


        //if (asset.traits == null || asset.traits.Count <= 0)
        //    return;
        //int j = 0;
        //for (int i = 0, length = traits.Count; i < length; i++)
        //{
        //    if (!traits[i])
        //        continue;

        //    if (asset.traits.Count > j && i >= j)
        //    {
        //        traits[i].gameObject.SetActive(true);
        //        traits[i].Display(asset.at[j]);

        //        j++;
        //        continue;
        //    }
        //    if (traits[i])
        //        traits[i].gameObject.SetActive(false);
        //}

        //CarComponent.Instance.DisplayInCar(contrat);

    }

    public NftAsset AssetInCache(string contrat)
    {
        if (assetsCache == null || assetsCache.Count == 0)
            return null;

        for (int i = 0, length = assetsCache.Count; i < length; i++)
        {
            if (assetsCache[i] == null || assetsCache[i].asset_contract == null)
                continue;
            if ((assetsCache[i].asset_contract.address).ToLower() == contrat.ToLower())
                return assetsCache[i];
        }

        return null;
    }


}
