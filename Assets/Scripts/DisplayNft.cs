using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class DisplayNft : MonoBehaviour
{

    public List<Button> nfts = new List<Button>();

    public TextMeshProUGUI txtBalance;


    async void Start()
    {
        string chain = "ethereum";
        string network = "mainnet";
        string contract1 = "0x3Fe1a4c1481c8351E91B64D5c398b159dE07cbc5";//"0x60f80121c31a0d46b5279700f9df786054aa5ee5";
        string contract2 = "0xb894E12536486141158Bc26684bDef85eA90379a";//"0x60f80121c31a0d46b5279700f9df786054aa5ee5";
        string contract3 = "0xd668A2E001f3385B8BBC5a8682AC3C0D83C19122";//"0x60f80121c31a0d46b5279700f9df786054aa5ee5";
        string contract4 = "0x495f947276749Ce646f68AC8c248420045cb7b5e";//"0x60f80121c31a0d46b5279700f9df786054aa5ee5";
        string contract5 = "0x6391A41819C699972b75bF61db6B34ef940C96F0";//"0x60f80121c31a0d46b5279700f9df786054aa5ee5";
        string account = PlayerPrefs.GetString("Account"); //0x6b2be2106a7e883f282e2ea8e203f516ec5b77f7";

        BigInteger balance = await ERC721.BalanceOf(chain, network, contract1, account);
        
        if (nfts.Count > 0 && nfts[0])
            if (balance > 0)
                nfts[0].interactable = true;
            else
            {
                nfts[0].interactable = false;
            }

        balance = await ERC721.BalanceOf(chain, network, contract2, account);

        if (nfts.Count > 0 && nfts[1])
            if (balance > 0)
                nfts[1].interactable = true;
            else
            {
                nfts[1].interactable = false;
            }

        balance = await ERC721.BalanceOf(chain, network, contract3, account);

        if (nfts.Count > 0 && nfts[2])
            if (balance > 0)
                nfts[2].interactable = true;
            else
            {
                nfts[2].interactable = false;
            }

        balance = await ERC1155.BalanceOf(chain, network, contract4, account, "51671675960190169403012972771059802082535596719777329950241731026478226735105");

        if (nfts.Count > 0 && nfts[3])
            if (balance > 0)
                nfts[3].interactable = true;
            else
            {
                nfts[3].interactable = false;
            }

        balance = await ERC721.BalanceOf(chain, network, contract5, account);

        if (nfts.Count > 0 && nfts[4])
            if (balance > 0)
                nfts[4].interactable = true;
            else
            {
                nfts[4].interactable = false;
            }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
