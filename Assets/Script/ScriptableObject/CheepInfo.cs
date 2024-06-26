using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CheepInfo", menuName = "ScriptableObjects/CheepInfo", order = 1)]
public class CheepInfo : ScriptableObject
{
    public int CheepID;//칩 ID
    public string CheepName;//칩 이름
    public string CheepEffectExplanationTxt;//칩효과 설명 텍스트
    public Sprite CheepIcon;//칩 아이콘
    public CheepType cheepType;//적욕할 칩 타입
}
