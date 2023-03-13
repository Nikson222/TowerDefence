using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TowerItem : MonoBehaviour
{
    public Action OnTowerSendedToBuilder;
    private Image _buttonImage;
    private GameObject _towerObjectToSpawn;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void InitItem(Sprite image, GameObject towerPrefab, Color spriteColor)
    {
        _buttonImage.sprite = image;
        _towerObjectToSpawn = towerPrefab;
        _buttonImage.color = spriteColor;
    }

    public void SendPrefabToBuilder()
    {
        TowerBuilder.Instance.GetPrefabToSpawn(_towerObjectToSpawn);
        OnTowerSendedToBuilder();
    }
}
