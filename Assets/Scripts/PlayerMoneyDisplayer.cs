using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoneyDisplayer : MonoBehaviour
{
    [SerializeField] private Text _moneyAmountText;

    public void UpdateText()
    {
        if (_moneyAmountText != null)
            _moneyAmountText.text = PlayerData.MoneyAmount.ToString();
    }
}
