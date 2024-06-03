using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    //씬이동 처리
    public void OnSceneMove(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    //게임 종료처리 함수
    public void OnCloseGame()
    {
        Application.Quit();
    }
    //팝업 Open
    public void OnOpenPopupPanel(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    //팝업 Close
    public void OnClosePopupPanel(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    //인게임 종료 시 게임메니저 제거를 위한 함수
    public void OnRemoveGamemanager()
    {
        GameManager _gameManager = GameManager.Instance;
        if (_gameManager != null)
        {
            Destroy(_gameManager);
        }
    }
}
