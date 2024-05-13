using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스테이지 정보
[System.Serializable]
public class StageInfo
{
    public List<WaveInfo> waveList;
    public int bestTurn;
    public int worstTurn;
    public int stageLevel;
}