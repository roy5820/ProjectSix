using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CheepInfo", menuName = "ScriptableObjects/CheepInfo", order = 1)]
public class CheepInfo : ScriptableObject
{
    public int CheepID;//칩 ID
    public string CheepName;//칩 이름
    public List<ChangeStateInfo> ChangeStateList = new List<ChangeStateInfo>();//변경할 상태 목록 
}
