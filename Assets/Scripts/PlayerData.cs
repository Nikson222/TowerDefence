using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static Action OnMoneyChanged;

    private static int _moneyAmount = 0;

    public static int MoneyAmount { get { return _moneyAmount; } }

    public static void GetMoney(int amount)
    {
        _moneyAmount += amount;
        OnMoneyChanged?.Invoke();
    }

    public static bool CheckBalance(int amount)
    {
        if (MoneyAmount - amount >= 0)
            return true;
        else
            return false;
    }

    public static void SpendMoney(int amount)
    {
        if (MoneyAmount - amount >= 0)
        {
            _moneyAmount -= amount;
            OnMoneyChanged?.Invoke();
        }
    }
}

