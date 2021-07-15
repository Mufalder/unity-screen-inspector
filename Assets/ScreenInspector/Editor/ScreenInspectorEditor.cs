using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NorthLab
{
    [CustomEditor(typeof(ScreenInspector))]
    public class ScreenInspectorEditor : Editor
    {

        private SerializedProperty show;
        private SerializedProperty onlyInEditMode;
        private SerializedProperty mirrored;
        private SerializedProperty color;
        private SerializedProperty corners;
        private SerializedProperty type;
        private SerializedProperty homeButton;
        private SerializedProperty alignType;

        private void OnEnable()
        {
            show = serializedObject.FindProperty("show");
            onlyInEditMode = serializedObject.FindProperty("onlyInEditMode");
            mirrored = serializedObject.FindProperty("mirrored");
            color = serializedObject.FindProperty("color");
            corners = serializedObject.FindProperty("corners");
            type = serializedObject.FindProperty("type");
            homeButton = serializedObject.FindProperty("homeButton");
            alignType = serializedObject.FindProperty("alignType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(show);
            EditorGUILayout.PropertyField(onlyInEditMode);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(mirrored);
            EditorGUILayout.PropertyField(color);
            EditorGUILayout.PropertyField(corners);
            EditorGUILayout.PropertyField(type);
            if (type.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(homeButton);
            }
            else if (type.enumValueIndex == 3)
            {
                EditorGUILayout.PropertyField(alignType);
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
}