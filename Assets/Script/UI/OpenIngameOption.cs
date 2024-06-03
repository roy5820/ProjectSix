using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenIngameOption : MonoBehaviour
{
    public GameObject ingameOptionPopup;
    public KeyCode OpenOptionPopupKey;

    private void Update()
    {
        if (Input.GetKeyDown(OpenOptionPopupKey))
            OnIngamePopup();
    }

    public void OnIngamePopup()
    {
        if(ingameOptionPopup.activeSelf)
            ingameOptionPopup.SetActive(false);
        else 
            ingameOptionPopup.SetActive(true);
    }
}
