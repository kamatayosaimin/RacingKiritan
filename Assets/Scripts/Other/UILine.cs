using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UILine : MaskableGraphic
{
    [SerializeField] private int _cornerVertices;
    [SerializeField] private float _width;
    [SerializeField] private Vector2[] _positions;

    public int CornerVeritices
    {
        get
        {
            return _cornerVertices;
        }
        set
        {
            _cornerVertices = value;
        }
    }

    public float Width
    {
        get
        {
            return _width;
        }
        set
        {
            _width = value;
        }
    }

    public Vector2[] Positions
    {
        get
        {
            return _positions;
        }
        set
        {
            _positions = value;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Vector2 pivot = rectTransform.pivot;
        Vector2 size = rectTransform.rect.size;
        Vector2 origin = Vector2.Scale(-pivot, size); //RectTransformの左下隅を原点とする

        vh.Clear();

        int cornerCount = _positions.Length;

        if (cornerCount < 2)
            return;

        int segmentCount = cornerCount - 1;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector2 a = origin + _positions[i];
            Vector2 b = origin + _positions[i + 1];

            AddSegment(vh, a, b, color, _width);

            if (i < (segmentCount - 1))
            {
                Vector2 c = origin + _positions[i + 2];
                Vector2 tangentFrom = (b - a).normalized;
                Vector2 tangentTo = (c - b).normalized;

                int tangentCount = _cornerVertices + 1;
                float tangentAngle = -Vector2.SignedAngle(tangentFrom, tangentTo) * Mathf.Deg2Rad;
                Vector2[] tangents = Enumerable.Range(0, tangentCount)
                    .Select(index =>
                    {
                        float angle = Mathf.Lerp(0.0f, tangentAngle, index / (tangentCount - 1.0f));
                        float cosAngle = Mathf.Cos(angle);
                        float sinAngle = Mathf.Sin(angle);

                        return new Vector2(
                            (cosAngle * tangentFrom.x) + (sinAngle * tangentFrom.y),
                            (cosAngle * tangentFrom.y) - (sinAngle * tangentFrom.x));
                    })
                    .ToArray();

                for (int j = 0; j < _cornerVertices; j++)
                    AddJoint(vh, b, tangents[j], tangents[j + 1], color, _width);
            }
        }
    }

    //p点に接続部を追加
    //tangentFromは入っていく線分の向き
    //tangentToは出ていく線分の向き
    private static void AddJoint(
        VertexHelper vh,
        Vector2 p,
        Vector2 tangentFrom,
        Vector2 tangentTo,
        Color color,
        float width)
    {
        float halfWidth = width / 2;
        Vector2 dFrom = new Vector2(-tangentFrom.y, tangentFrom.x) * halfWidth;
        Vector2 dTo = new Vector2(-tangentTo.y, tangentTo.x) * halfWidth;

        int offset = vh.currentVertCount;
        UIVertex vertex = UIVertex.simpleVert;

        vertex.position = p - dFrom;
        vertex.color = color;

        vh.AddVert(vertex);

        vertex.position = p + dFrom;

        vh.AddVert(vertex);

        vertex.position = p + dTo;

        vh.AddVert(vertex);

        vertex.position = p - dTo;

        vh.AddVert(vertex);

        vh.AddTriangle(offset + 0, offset + 1, offset + 2);
        vh.AddTriangle(offset + 2, offset + 3, offset + 0);
    }

    //a、b点間に線分を追加
    private static void AddSegment(
        VertexHelper vh,
        Vector2 a,
        Vector2 b,
        Color color,
        float width)
    {
        Vector2 tangent = (b - a).normalized;
        Vector2 normal = new Vector2(-tangent.y, tangent.x);
        Vector2 dA = width * 0.5f * normal;
        Vector2 dB = width * 0.5f * normal;

        int offset = vh.currentVertCount;
        UIVertex vertex = UIVertex.simpleVert;

        vertex.color = color;
        vertex.position = a - dA;

        vh.AddVert(vertex);

        vertex.position = a + dA;

        vh.AddVert(vertex);

        vertex.position = b + dB;

        vh.AddVert(vertex);

        vertex.position = b - dB;

        vh.AddVert(vertex);

        vh.AddTriangle(offset + 0, offset + 1, offset + 2);
        vh.AddTriangle(offset + 2, offset + 3, offset + 0);
    }
}
