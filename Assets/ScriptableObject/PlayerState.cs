using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerState", menuName = "ScriptableObject/PlayerState")]
public class PlayerState : ScriptableObject
{
    public int maxHp = 100;
    public int nowHp;
    public int level;
}