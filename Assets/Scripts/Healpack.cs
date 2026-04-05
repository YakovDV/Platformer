using UnityEngine;

public class HealPack : MonoBehaviour
{
    [SerializeField] private int _healValue = 10;

    public int HealValue => _healValue;
}