using UnityEngine;

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] protected TypeOfTower typeOfTower;

        [Header("Prices")]
        [SerializeField] protected int _buildPrice;
        [SerializeField] protected int _updgadePrice;

        [Header("Level Settings")]
        [SerializeField] protected int _towerLevel = 1;
        [SerializeField] protected int _towerMaxLevel;
    }

    public enum TypeOfTower
    {
        Rifle,
        Buff,
        Draft
    }
}