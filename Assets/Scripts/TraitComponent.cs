using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TraitComponent : MonoBehaviour
{
    public TextMeshProUGUI txtType;

    public TextMeshProUGUI txtValue;

    public void Display(Attribut trait)
    {
        if (trait == null)
            return;

        if (txtType)
            txtType.text = "" + trait.trait_type;

        if (txtValue)
            txtValue.text = "" + trait.value;

        //if (percent)
        //    percent.text = Mathf.Floor(trait.trait_count/100) + "% Has this trait";
    }
}
