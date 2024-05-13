using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMovement : MonoBehaviour
{
    public void OnSceneMove(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
