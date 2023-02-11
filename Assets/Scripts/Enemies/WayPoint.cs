using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [Header("Offset")]
    [SerializeField] private Vector2 _leftTopAngle = new Vector2(-0.5f, 0.5f);
    [SerializeField] private Vector2 _rightTopAngle = new Vector2(0.5f, 0.5f);

    [SerializeField] private Vector2 _leftBottomAngle = new Vector2(-0.5f, -0.5f);
    [SerializeField] private Vector2 _rightBottomAngle = new Vector2(0.5f, -0.5f);

    [Header("Gizmos Settings")]
    [SerializeField] private Color _gizmozColor = new Color(0, 0, 0, 60);

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmozColor;
        Gizmos.DrawLine(_leftTopAngle + (Vector2)transform.position, _rightTopAngle + (Vector2)transform.position);
        Gizmos.DrawLine(_rightTopAngle + (Vector2)transform.position, _rightBottomAngle + (Vector2)transform.position);
        Gizmos.DrawLine(_rightBottomAngle + (Vector2)transform.position, _leftBottomAngle + (Vector2)transform.position);
        Gizmos.DrawLine(_leftBottomAngle + (Vector2)transform.position, _leftTopAngle + (Vector2)transform.position);
    }

    public Vector2 WayPointPosition
    {
        get
        {
            Vector3 interpolatedLeftBorder = Vector2.Lerp(_leftTopAngle + (Vector2)transform.position, _leftBottomAngle + (Vector2)transform.position, Random.Range(0f, 1f));
            Vector3 interpolatedRightBorder = Vector2.Lerp(_rightTopAngle + (Vector2)transform.position, _rightBottomAngle + (Vector2)transform.position, Random.Range(0f, 1f));

            return Vector2.Lerp(interpolatedRightBorder, interpolatedLeftBorder, Random.Range(0f, 1f));
        }
    }
}
