using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipInfoWIndow : MonoBehaviour
{
    public CheepInfo chipInfo = null;
    public Text chipName;
    public Text chipEffect;

    // Start is called before the first frame update
    void Start()
    {
        if(chipInfo != null)
        {
            chipName.text = chipInfo.CheepName;
            chipEffect.text = chipInfo.CheepEffectExplanationTxt;
        }
    }
}
