using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EngineCurveAttribute))]
public class EngineCurveDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.AnimationCurve)
        {
            EditorGUI.PropertyField(position, property, label);

            return;
        }

        EngineCurveAttribute engineCurve = (EngineCurveAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        AnimationCurve torqueCurve = property.animationCurveValue;

        Rect headerRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.LabelField(headerRect, label, EditorStyles.boldLabel);

        Rect graphRect = new Rect(position.x, position.y + 20f, position.width, 200f);

        EditorGUI.CurveField(graphRect, property, Color.red, engineCurve.ranges, GUIContent.none);
        EditorGUI.DrawRect(graphRect, new Color(0.15f, 0.15f, 0.15f, 1f));

        Handles.color = new Color(0.3f, 0.3f, 0.3f, 1f);

        int sectionX = 8;
        int sectionY = 10;

        for (int i = 1; i < sectionX; i++)
        {
            float x = graphRect.x + (graphRect.width * i / sectionX);

            Handles.DrawLine(new Vector3(x, graphRect.y, 0f), new Vector3(x, graphRect.yMax, 0f));
        }

        for (int i = 1; i < sectionY; i++)
        {
            float y = graphRect.y + (graphRect.height * i / sectionY);

            Handles.DrawLine(new Vector3(graphRect.x, y, 0f), new Vector3(graphRect.xMax, y, 0f));
        }

        if (torqueCurve.keys.Length >= 2)
        {
            Vector2 maxTorque = Vector2.zero;
            Vector2 maxPower = Vector2.zero;
            List<Vector2> torqueList = new List<Vector2>();
            List<Vector2> powerList = new List<Vector2>();

            System.Action<float> arg = r =>
            {
                Vector2 torque = new Vector2(r, torqueCurve.Evaluate(r));

                if (torque.y > maxTorque.y)
                    maxTorque = torque;

                torqueList.Add(torque);

                // POWER = TORQUE * rpm * (Mathf.PI * 2 / (75kgm * 60sec.))
                Vector2 power = new Vector2(r, torque.y * r * (Mathf.PI * 2f / 4500f));

                if (power.y >= maxPower.y)
                    maxPower = power;

                powerList.Add(power);
            };

            arg(torqueCurve.keys.First().time);

            for (float r = 0f; r < torqueCurve.keys.Last().time; r += 50f)
            {
                if (r <= torqueCurve.keys.First().time)
                    continue;

                arg(r);
            }

            arg(torqueCurve.keys.Last().time);

            float maxRpm = 16000f;

            Handles.color = Color.red;

            Handles.DrawPolyLine(torqueList.Select(t =>
            {
                float x = graphRect.x + t.x * graphRect.width / maxRpm;
                float y = graphRect.yMax - t.y * graphRect.height / 200f;

                return new Vector3(x, y, 0f);
            }
            ).ToArray());

            Handles.color = Color.cyan;

            Handles.DrawPolyLine(powerList.Select(p =>
            {
                float x = graphRect.x + p.x * graphRect.width / maxRpm;
                float y = graphRect.yMax - p.y * graphRect.height / 2000f;

                return new Vector3(x, y, 0f);
            }
            ).ToArray());

            torqueList.Clear();
            powerList.Clear();

            EditorGUILayout.LabelField(string.Format("{0:0} PS @ {1:0} rpm", maxPower.y, maxPower.x));
            EditorGUILayout.LabelField(string.Format("{0:0.0} kgm @ {1:0} rpm", maxTorque.y, maxTorque.x));
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 230f;
    }
}
