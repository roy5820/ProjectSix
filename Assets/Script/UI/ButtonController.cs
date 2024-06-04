using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public float popupMoveSeed = 0.1f;
    private Coroutine moveCorutine = null;
    //���̵� ó��
    public void OnSceneMove(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    //���� ����ó�� �Լ�
    public void OnCloseGame()
    {
        Application.Quit();
    }
    //�˾� Open
    public void OnOpenPopupPanel(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    //�˾� Close
    public void OnClosePopupPanel(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    //�˾� Up
    public void OnUpPopupPanel(GameObject gameObject)
    {
        if(moveCorutine == null)
            moveCorutine = StartCoroutine(UpDownPopup(gameObject, 0.5f, popupMoveSeed));
    }

    //�˾� Down
    public void OnDownPopupPanel(GameObject gameObject)
    {
        if (moveCorutine == null)
            moveCorutine = StartCoroutine(UpDownPopup(gameObject, 1.5f, popupMoveSeed));
    }

    //�˾�â �ø��� ������ ��� ������ �ϴ� �ڷ�ƾ �Լ�
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

    //�ΰ��� ���� �� ���Ӹ޴��� ���Ÿ� ���� �Լ�
    public void OnRemoveGamemanager()
    {
        GameManager _gameManager = GameManager.Instance;
        if (_gameManager != null)
        {
            Destroy(_gameManager.gameObject);
        }
    }
}
