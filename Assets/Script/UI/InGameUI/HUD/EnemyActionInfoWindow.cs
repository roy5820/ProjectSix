using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyActionInfoWindow : MonoBehaviour
{
    public string actionName = null;
    public string actionEffect = null;
    public Text actionNameTxt;
    public Text actionEffectTxt;

    // Start is called before the first frame update
    void Start()
    {
        actionNameTxt.text = actionName;
        actionEffectTxt.text = actionEffect;
    }
}
