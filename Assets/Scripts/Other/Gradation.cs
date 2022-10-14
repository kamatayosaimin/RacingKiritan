using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GradiationType
{
    None,
    LeftToRight,
    TopToBottom,
    DownToTheRight,
    RisingToTheRight
}

public class Gradation : BaseMeshEffect
{

    enum ColorType
    {
        Start,
        Mid,
        End
    }

    enum GraphicType
    {
        Other,
        Image,
        Text
    }

    private bool _isUpdated;
    [SerializeField] private GradiationType _type;
    [SerializeField] private Color _startColor = Color.white, _endColor = Color.white;

    public GradiationType Type
    {
        get
        {
            return _type;
        }
        set
        {
            _isUpdated = true;
            _type = value;
        }
    }

    public Color StartColor
    {
        get
        {
            return _startColor;
        }
        set
        {
            _isUpdated = true;
            _startColor = value;
        }
    }

    public Color EndColor
    {
        get
        {
            return _endColor;
        }
        set
        {
            _isUpdated = true;
            _endColor = value;
        }
    }

    public Color GraphicColor
    {
        get
        {
            return graphic.color;
        }
        set
        {
            graphic.color = value;
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

    void LateUpdate()
    {
        if (!_isUpdated)
            return;

        _isUpdated = false;

        graphic.SetVerticesDirty();
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive() || _type == GradiationType.None)
            return;

        GraphicType graphicType = GetGraphicType();

        if (graphicType == GraphicType.Other)
            return;

        List<UIVertex> vertexList = new List<UIVertex>();

        vh.GetUIVertexStream(vertexList);

        ColorType[] colorTypes = GetColorTypes(graphicType);

        for (int i = 0; i < vertexList.Count; i++)
        {
            UIVertex vertex = vertexList[i];

            vertex.color = GetColor(colorTypes[i % 6]);

            vertexList[i] = vertex;
        }

        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexList);
    }

    ColorType[] GetColorTypes(GraphicType graphicType)
    {
        switch (graphicType)
        {
            case GraphicType.Image:
                return GetColorTypesImage();
            case GraphicType.Text:
                return GetColorTypesText();
        }

        return null;
    }

    ColorType[] GetColorTypesImage()
    {
        ColorType[] colorTypes = null;

        switch (_type)
        {
            case GradiationType.LeftToRight:
                colorTypes = new[]
                {
                    ColorType.Start,
                    ColorType.Start,
                    ColorType.End,
                    ColorType.End,
                    ColorType.End,
                    ColorType.Start
                };

                return colorTypes;
            case GradiationType.TopToBottom:
                colorTypes = new[]
                {
                    ColorType.End,
                    ColorType.Start,
                    ColorType.Start,
                    ColorType.Start,
                    ColorType.End,
                    ColorType.End
                };

                return colorTypes;
            case GradiationType.DownToTheRight:
                colorTypes = new[]
                {
                    ColorType.Mid,
                    ColorType.Start,
                    ColorType.Mid,
                    ColorType.Mid,
                    ColorType.End,
                    ColorType.Mid
                };

                return colorTypes;
            case GradiationType.RisingToTheRight:
                colorTypes = new[]
                {
                    ColorType.Start,
                    ColorType.Mid,
                    ColorType.End,
                    ColorType.End,
                    ColorType.Mid,
                    ColorType.Start
                };

                return colorTypes;
        }

        return colorTypes;
    }

    ColorType[] GetColorTypesText()
    {
        ColorType[] colorTypes = null;

        switch (_type)
        {
            case GradiationType.LeftToRight:
                colorTypes = new[]
                {
                    ColorType.Start,
                    ColorType.End,
                    ColorType.End,
                    ColorType.End,
                    ColorType.Start,
                    ColorType.Start
                };

                return colorTypes;
            case GradiationType.TopToBottom:
                colorTypes = new[]
                {
                    ColorType.Start,
                    ColorType.Start,
                    ColorType.End,
                    ColorType.End,
                    ColorType.End,
                    ColorType.Start
                };

                return colorTypes;
            case GradiationType.DownToTheRight:
                colorTypes = new[]
                {
                    ColorType.Start,
                    ColorType.Mid,
                    ColorType.End,
                    ColorType.End,
                    ColorType.Mid,
                    ColorType.Start
                };

                return colorTypes;
            case GradiationType.RisingToTheRight:
                colorTypes = new[]
                {
                    ColorType.Mid,
                    ColorType.End,
                    ColorType.Mid,
                    ColorType.Mid,
                    ColorType.Start,
                    ColorType.Mid
                };

                return colorTypes;
        }

        return colorTypes;
    }

    GraphicType GetGraphicType()
    {
        if (graphic is Image || graphic is RawImage)
            return GraphicType.Image;

        if (graphic is Text)
            return GraphicType.Text;

        return GraphicType.Other;
    }

    Color GetColor(ColorType type)
    {
        switch (type)
        {
            case ColorType.Start:
                return _startColor;
            case ColorType.Mid:
                return Color.Lerp(_startColor, _endColor, 0.5f);
            case ColorType.End:
                return _endColor;
        }

        return Color.white;
    }
}
