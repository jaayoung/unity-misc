using System.Collections.Generic;
using UnityEngine;

public class SchlickComponent : MonoBehaviour
{
    [SerializeField][Range(0f, 16f)]
    private float slope = 1f;

    [SerializeField][Range(0f, 1f)]
    private float threshold = 0.5f;

    [SerializeField]
    private bool showGizmo;

    [SerializeField]
    private bool showGizmoPoints;
    
    [SerializeField]
    private float gizmoXOffset = -0.5f;

    [SerializeField]
    private float gizmoYOffset = 0.5f;
    

    private void OnDrawGizmos()
    {
        if(!showGizmo) return;
        
        const int count = 25;
        const float increment = 1f / (count - 1);
        float startX = transform.position.x + gizmoXOffset;
        float startY = transform.position.y + gizmoYOffset;
        float z = transform.position.z;
        
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(new Vector3(transform.position.x, startY+0.5f, z), Vector3.one);
        
        Gizmos.color = Color.magenta;
        var points = new List<float>(count);
        for (int i = 0; i < count; ++i)
        {
            var offset = i * increment;
            points.Add(SchlickCurve.Evaluate(offset, slope, threshold));
            if (i > 0)
            {
                var start = new Vector3(startX + offset - increment, startY + points[i - 1], z);
                var end = new Vector3(startX + offset, startY + points[i], z);
                Gizmos.DrawLine(start, end);

                if (showGizmoPoints)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawSphere(start, 0.025f);
                    if (i == count - 1)
                    {
                        Gizmos.DrawSphere(end, 0.025f);
                    }
                }
            }
        }
        Gizmos.color = Color.white;
    }
}
