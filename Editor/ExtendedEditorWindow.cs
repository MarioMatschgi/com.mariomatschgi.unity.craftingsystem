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

    List<SerializedProperty> selectedProperties;

    int currentSidebarIdx;


    protected virtual void OnGUI()
    {
        Apply();
        ResetSidebarIds();
    }

    protected void DrawSidebar(SerializedProperty _prop)
    {
        if (selectedProperties == null)
            selectedProperties = new List<SerializedProperty>();

        EditorGUILayout.BeginVertical();

        #region Header
        /*
         * Header
         */

        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Sidebar for property: " + _prop.displayName, EditorStyles.boldLabel);

        if (_prop.isArray)
        {
            EditorGUILayout.BeginHorizontal();
            float _tmpW = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 30;
            _prop.arraySize = EditorGUILayout.IntField("Size", _prop.arraySize);
            EditorGUIUtility.labelWidth = _tmpW;
            EditorGUILayout.EndHorizontal();
        }

        //EditorGUILayout.IntField();
        EditorGUILayout.EndHorizontal();
        #endregion


        #region Content
        /*
         * Content
         */

        EditorGUILayout.BeginHorizontal();

        #region Sidebar-Buttons
        /*
         * Sidebar-Buttons
         */

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        IEnumerator _e = _prop.Copy().GetEnumerator();
        while (_e.MoveNext())
        {
            SerializedProperty _p = (SerializedProperty)_e.Current;
            if (_p.hasChildren)
                if (GUILayout.Button(_p.displayName))
                {
                    if (selectedProperties.Count <= currentSidebarIdx)
                        selectedProperties.Add(serializedObject.FindProperty(_p.propertyPath));
                    else
                        selectedProperties[currentSidebarIdx] = serializedObject.FindProperty(_p.propertyPath);
                }
        }
        EditorGUILayout.EndVertical();
        #endregion

        #region Sidebar-Content
        /*
         * Sidebar-Content
         */

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        if (selectedProperties.Count <= currentSidebarIdx || selectedProperties[currentSidebarIdx] == null)
            EditorGUILayout.LabelField("Please select an element from the list to the left!", EditorStyles.wordWrappedLabel);
        else
        {
            EditorGUILayout.LabelField("Editing: " + selectedProperties[currentSidebarIdx].displayName, EditorStyles.boldLabel);

            DrawProperties(selectedProperties[currentSidebarIdx], true, false);
        }
        EditorGUILayout.EndVertical();
        #endregion

        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.EndVertical();

        // Incrase Sidebar index
        currentSidebarIdx++;
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

    protected void DrawProperties(SerializedProperty _prop, bool _useEnumerator, bool _checkHasChildren = false)
    {
        string _lastPropPath = string.Empty;

        if (_useEnumerator)
        {
            IEnumerator _e = _prop.Copy().GetEnumerator();
            while (_e.MoveNext())
                if (DrawProperty(((SerializedProperty)_e.Current).propertyPath))
                    continue;
        }
        else
        {
            while ((_checkHasChildren && _prop.hasVisibleChildren && _prop.NextVisible(true)) || (!_checkHasChildren && _prop.NextVisible(true)))
            {
                // Don't draw the ones from the dropwown normal as well
                if (!string.IsNullOrEmpty(_lastPropPath) && _prop.propertyPath.Contains(_lastPropPath))
                    continue;

                _lastPropPath = _prop.propertyPath;

                if (DrawProperty(_prop.propertyPath))
                    continue;
            }
        }
    }

    /// <summary>
    /// Returns false if drawing was canceld by ie Sidebar
    /// </summary>
    /// <param name="_propName"></param>
    /// <returns></returns>
    protected bool DrawProperty(string _propName)
    {
        if (serializedObject != null)
        {
            SerializedProperty _prop = serializedObject.FindProperty(_propName);
            if (_prop.isArray && _prop.propertyType == SerializedPropertyType.Generic)
            {
                DrawSidebar(_prop);
                return false;
            }
        }

        if (serializedObject != null)
            EditorGUILayout.PropertyField(serializedObject.FindProperty(_propName), true);

        return true;
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }

    void ResetSidebarIds()
    {
        currentSidebarIdx = 0;
    }
}

#endif