using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NftInfo : MonoBehaviour
{

    public Image image;
    Response item;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitNft(Response data, Sprite img)
    {
        image.sprite = img;

        item = data;
    }

    public void OnClick()
    {
        DisplayNft2.Instance.OnNftClickV2(item);
    }
}
