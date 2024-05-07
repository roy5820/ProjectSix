using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBatteryGauge : MonoBehaviour
{
    private PlayerController _playerController;//플레이어 컨트롤러
    public Text maxBatteryGauge;
    public Text nowBatteryGauge;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//캐릭터 매니저 초기화
    }

    private void Update()
    {
        //배터리 값 갱신
        if (_playerController)
        {
            if(maxBatteryGauge != null)
            {
                maxBatteryGauge.text = _playerController.maxBattery.ToString();
            }
            if(nowBatteryGauge != null)
            {
                nowBatteryGauge.text = _playerController.nowBattery.ToString();
            }
        }
    }
}
