using UnityEngine;

[CreateAssetMenu(fileName = "CarDatabase", menuName = "Game/Car Database")]
public class CarDatabase : ScriptableObject
{
    public CarData[] cars;   // Danh sách tất cả xe
}
