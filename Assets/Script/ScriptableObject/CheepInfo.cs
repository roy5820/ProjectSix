using UnityEngine;
[CreateAssetMenu(fileName = "CheepInfo", menuName = "ScriptableObjects/CheepInfo", order = 1)]
public class CheepInfo : ScriptableObject
{
    public int CheepID;//칩 ID
    public string CheepName;//칩 이름
    public StateEnum ApplyStateType;//적용할 상태 타입
    public StateBase ApplyState;//적용할 상태
}
