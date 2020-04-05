#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    protected void DrawProperties(SerializedProperty _prop, bool _checkHasChildren = false)
    {
        string _lastPropPath = string.Empty;

        while ( (_checkHasChildren && _prop.hasVisibleChildren && _prop.NextVisible(true)) || (!_checkHasChildren && _prop.NextVisible(true)) )
        {
            if (_prop.isArray && _prop.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                _prop.isExpanded = EditorGUILayout.Foldout(_prop.isExpanded, _prop.displayName);
                EditorGUILayout.EndHorizontal();

                if (_prop.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(_prop, true);
                    EditorGUI.indentLevel--;
                }

                continue;
            }

            // Don't draw the ones from the dropwown normal as well
            if (!string.IsNullOrEmpty(_lastPropPath) && _prop.propertyPath.Contains(_lastPropPath))
                continue;

            _lastPropPath = _prop.propertyPath;
            EditorGUILayout.PropertyField(_prop, true);
        }
    }
}

#endif