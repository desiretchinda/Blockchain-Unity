using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NftAsset
{
    public int id;

    public string token_id;

    public string name;

    public NftAssetContract asset_contract;

    public List<NftAssetTrait> traits = new List<NftAssetTrait>();

    public NftAssetCollection collection;
}
