#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MM.Libraries.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FlexibleGridLayout))]
    public class FlexibleGridLayoutEditor : Editor
    {


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            #region General Properties
            /*
             * General Properties
             */

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rows"), new GUIContent("Row amount",
                "The amount of rows in the grid" +
                "\nOnly changeable if Fittype is not Width, Height, Uniform or FixedColumns"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("colums"), new GUIContent("Column amount",
                "The amount of rows in the grid" +
                "\nOnly changeable if Fittype is not Width, Height, Uniform or FixedRows"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("space"), new GUIContent("Space",
                "The space between cells"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cellSize"), new GUIContent("Cell size",
                "The size of each individual cell\nOnly changeable if Fit X-Axis or Fit Y-Axis is false"));
            EditorGUILayout.EndVertical();
            #endregion


            #region Padding Properties
            /*
             * Padding Properties
             */

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Padding", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("padding"), new GUIContent("Padding",
                "The padding around the grid"));
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            #endregion


            #region Fitting Properties
            /*
             * Fitting Properties
             */

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Fitting", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fitType"), new GUIContent("Fit type",
                "The type of fitting:" +
                "\nUniform: Equal number of rows and columns" +
                "\nWidth: More columns than rows" +
                "\nHeight: More rows than columns" +
                "\nFixedRows: Fixed number of rows" +
                "\nFixedColumns: Fixed number of columns"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fitX"), new GUIContent("Fit X-Axis",
                "Scales the width of the cell" +
                "\nOnly changeable if Fittype is not Width, Height or Uniform"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fitY"), new GUIContent("Fit Y-Axis",
                "Scales the height of the cell" +
                "\nOnly changeable if Fittype is not Width, Height or Uniform"));
            EditorGUILayout.EndVertical();
            #endregion

            EditorGUILayout.EndVertical();

            // Update
            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Gameplay Methodes
        /*
         *
         *  Gameplay Methodes
         *
         */

        #endregion

        #region Helper Methodes
        /*
         *
         *  Helper Methodes
         * 
         */

        #endregion
    }
}

#endif