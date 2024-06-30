using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressInfo: MonoBehaviour
{
    private GameManager _gameManager;
    public Text nowStageTxt;
    public Text maxStageTxt;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        nowStageTxt.text = (_gameManager.nowProgress+1).ToString();
        maxStageTxt.text = _gameManager.stageLevel.Count.ToString();
    }
}
