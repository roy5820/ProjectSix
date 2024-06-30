using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoddingScene : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image ProgressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoddingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if(op.progress < 0.5f)
            {
                ProgressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                ProgressBar.fillAmount = Mathf.Lerp(0.5f, 1f, timer);
                if(ProgressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
