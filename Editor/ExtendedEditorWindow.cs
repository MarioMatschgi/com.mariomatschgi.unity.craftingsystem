#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MM.Extentions;

public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    string selectedPropertyPath;
    protected SerializedProperty selectedProperty;


    protected virtual void OnGUI()
    {
        Apply();
    }

    protected void DrawSidebar(SerializedProperty _prop)
    {
        SerializedProperty _p = _prop.Copy();
        //_p.NextVisible(true);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        Debug.Log("PROP: " + _p.displayName);
        //Debug.Log("LLL: " + _p.CountInProperty());
        int i = 0;
        while (_p.NextVisible(true))
        {
            Debug.Log(i + " SubPROP: " + _p.displayName + " : " + _p.CountInProperty());
            //if (_p.isArray && _prop.propertyType == SerializedPropertyType.Generic)
            //if (_p.isArray)
            //{
            //Debug.Log("ArrPROP: " + _p.displayName);
            if (GUILayout.Button(_p.displayName))
            {
                selectedPropertyPath = _p.propertyPath;
            }
            //}

            i++;
        }
        Debug.Log("i: " + i);
        EditorGUILayout.EndVertical();

        // Set selected Property
        if (!string.IsNullOrEmpty(selectedPropertyPath))
            selectedProperty = serializedObject.FindProperty(selectedPropertyPath);

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if (selectedProperty == null)
            EditorGUILayout.LabelField("SELECT");
        else
            DrawProperties(selectedProperty);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }

    protected int DrawTabs<T>(int _intValue) where T : Enum
    {
        EditorGUILayout.BeginHorizontal("box");

        int _val = _intValue;
        for (int i = 0; i < Enum.GetValues(typeof(T)).Length; i++)
        {
            if (GUILayout.Button(((Enum)Enum.GetValues(typeof(T)).GetValue(i)).GetStringValue()))
            {
                _val = i;
            }
        }
        EditorGUILayout.EndHorizontal();

        return _val;
    }

    protected void DrawProperties(SerializedProperty _prop, bool _checkHasChildren = false)
    {
        string _lastPropPath = string.Empty;

        while ( (_checkHasChildren && _prop.hasVisibleChildren && _prop.NextVisible(true)) || (!_checkHasChildren && _prop.NextVisible(true)) )
        {
            if (_prop.isArray && _prop.propertyType == SerializedPropertyType.Generic)
            {
                DrawSidebar(_prop);


                //EditorGUILayout.BeginHorizontal();
                //_prop.isExpanded = EditorGUILayout.Foldout(_prop.isExpanded, _prop.displayName);
                //EditorGUILayout.EndHorizontal();

                //if (_prop.isExpanded)
                //{
                //    EditorGUI.indentLevel++;
                //    DrawProperties(_prop, true);
                //    EditorGUI.indentLevel--;
                //}

                continue;
            }

            // Don't draw the ones from the dropwown normal as well
            if (!string.IsNullOrEmpty(_lastPropPath) && _prop.propertyPath.Contains(_lastPropPath))
                continue;

            _lastPropPath = _prop.propertyPath;
            EditorGUILayout.PropertyField(_prop, true);
        }
    }

    protected void DrawProperty(string _propName, bool _relative)
    {
        if (_relative && serializedProperty != null)
        {
            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative(_propName));
        }
        else if (serializedObject != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(_propName), true);
        }
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}

#endif