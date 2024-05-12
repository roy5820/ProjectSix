using UnityEngine;
[CreateAssetMenu(fileName = "CharacterStatus", menuName = "ScriptableObjects/CharacterStatus", order = 1)]
public class CharacterStatus : ScriptableObject
{
    public int maxHp;
    public int nowHp;
    public int offensePower;
    public int maxBattery;
}
