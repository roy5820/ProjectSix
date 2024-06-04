using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public float popupMoveSeed = 0.1f;
    private Coroutine moveCorutine = null;
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

    //팝업 Up
    public void OnUpPopupPanel(GameObject gameObject)
    {
        if(moveCorutine == null)
            moveCorutine = StartCoroutine(UpDownPopup(gameObject, 0.5f, popupMoveSeed));
    }

    //팝업 Down
    public void OnDownPopupPanel(GameObject gameObject)
    {
        if (moveCorutine == null)
            moveCorutine = StartCoroutine(UpDownPopup(gameObject, 1.5f, popupMoveSeed));
    }

    //팝업창 올리고 내리는 기능 구현을 하는 코루틴 함수
    private IEnumerator UpDownPopup(GameObject popupObject, float targetY, float moveSpeed)
    {
        RectTransform rectTransform = popupObject.GetComponent<RectTransform>();
        float nowY = rectTransform.pivot.y;
        
        while (nowY != targetY)
        {
            yield return new WaitForFixedUpdate();
            nowY = nowY > targetY ? Mathf.Round((nowY - moveSpeed)*10)* 0.1f : Mathf.Round((nowY + moveSpeed) * 10) * 0.1f;
            popupObject.GetComponent<RectTransform>().pivot = new Vector2(rectTransform.pivot.x, nowY);
        }
        moveCorutine = null;
    }

    //인게임 종료 시 게임메니저 제거를 위한 함수
    public void OnRemoveGamemanager()
    {
        GameManager _gameManager = GameManager.Instance;
        if (_gameManager != null)
        {
            Destroy(_gameManager.gameObject);
        }
    }
}
