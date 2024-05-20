using System;
using UnityEngine;

[Serializable]
public class RayCollisionCheck
{
    [SerializeField] Transform CheckTransform;
    [SerializeField] float CheckDistance;
    [SerializeField] LayerMask CheckLayer;
    [SerializeField] Vector2 Direction;

    [Header("DeveloperTools")] 
    [SerializeField] Color LineColor;

    [SerializeField] private bool visibile;

    public bool Detected() => Physics2D.Raycast(CheckTransform.position, Direction, CheckDistance, CheckLayer);

    private void OnDrawGizmos()
    {
        var pos = CheckTransform.position;
        Gizmos.color = LineColor;
        
        if(visibile)
            Gizmos.DrawLine(pos, pos + (Vector3)(Direction * CheckDistance));
    }
}
