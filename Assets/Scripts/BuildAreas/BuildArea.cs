using System;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class BuildArea : MonoBehaviour
{
    public Action<BuildArea> OnClicked;
    public bool IsPlaced;
    public ITower TowerInArea;
    public bool IsClickedOnBuildArea;

    public bool MouseInArea;


    private void OnMouseUp()
    {
        OnClicked?.Invoke(this);    
    }
}
