using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UILine : Graphic
{
    [SerializeField] private float _width;
    [SerializeField] private bool _loop;
    [SerializeField] private Vector2[] _positions;

    [SerializeField] private Vector2 _position0;
    [SerializeField] private Vector2 _position1;
    [SerializeField] private Vector2 _position2;

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
        vh.Clear();

        Vector2 offset0 = GetOffset(_position0, _position1);
        Vector2 offset1 = GetOffset(_position1, _position2);
        Vector2 vertexPosition0a = _position0 - offset0;
        Vector2 vertexPosition0b = _position0 + offset0;
        Vector2 vertexPosition1a = _position1 - offset0;
        Vector2 vertexPosition1b = _position1 + offset0;
        Vector2 vertexPosition1A = _position1 - offset1;
        Vector2 vertexPosition1B = _position1 + offset1;
        Vector2 vertexPosition2a = _position2 - offset1;
        Vector2 vertexPosition2b = _position2 + offset1;
        Vector2 vertexPosition1IA
            = GetIntersection(vertexPosition0a, vertexPosition1a, vertexPosition1A, vertexPosition2a);
        Vector2 vertexPosition1IB
            = GetIntersection(vertexPosition0b, vertexPosition1b, vertexPosition1B, vertexPosition2b);

        AddVert(vh, vertexPosition0a);
        AddVert(vh, vertexPosition0b);
        AddVert(vh, vertexPosition1IA);
        AddVert(vh, vertexPosition1IB);
        AddVert(vh, vertexPosition2a);
        AddVert(vh, vertexPosition2b);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(1, 2, 3);
        vh.AddTriangle(2, 3, 4);
        vh.AddTriangle(3, 4, 5);
    }

    void AddVert(VertexHelper vh, Vector2 position)
    {
        UIVertex vertex = UIVertex.simpleVert;

        vertex.color = color;
        vertex.position = position;

        vh.AddVert(vertex);
    }

    Vector2 GetOffset(Vector2 position0, Vector2 position1)
    {
        float halfWidth = _width / 2f;
        Vector2 distance = position1 - position0;

        if (distance.y == 0f)
            return Vector2.up * halfWidth;

        float x = 1f;
        float y = -distance.x / distance.y;
        Vector2 offset = new Vector2(x, y).normalized * halfWidth;

        return distance.y > 0f ? -offset : offset;
    }

    Vector2 GetIntersection(Vector2 position0a, Vector2 position0b, Vector2 position1a, Vector2 position1b)
    {
        float s1 = ((position1b.x - position1a.x) * (position0a.y - position1a.y)
            - (position1b.y - position1a.y) * (position0a.x - position1a.x))
            / 2f;
        float s2 = ((position1b.x - position1a.x) * (position1a.y - position0b.y)
            - (position1b.y - position1a.y) * (position1a.x - position0b.x))
            / 2f;
        float st = s1 + s2;

        if (st == 0f)
            return position0a;

        float x = position0a.x + (position0b.x - position0a.x) * s1 / st;
        float y = position0a.y + (position0b.y - position0a.y) * s1 / st;

        return new Vector2(x, y);
    }
}
