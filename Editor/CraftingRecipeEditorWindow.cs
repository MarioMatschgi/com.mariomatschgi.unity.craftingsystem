#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MM.Systems.CraftingSystem
{
    public class CraftingRecipeEditorWindow : ExtendedEditorWindow
    {
        // Public static
        public static CraftingRecipeEditorWindow window;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        public static void Open(CraftingRecipe _craftingRecipe)
        {
            window = GetWindow<CraftingRecipeEditorWindow>("Crafting Recipe Editor");
            window.serializedObject = new SerializedObject(_craftingRecipe);
        }

        void OnGUI()
        {
            //SerializedProperty _p = serializedObject.GetIterator();
            //_p.NextVisible(true);
            //DrawProperties(_p);


            SerializedProperty _p = serializedObject.GetIterator();
            _p.NextVisible(true);
            serializedProperty = _p;

            EditorGUILayout.BeginHorizontal();

            DrawSidebar(serializedProperty);

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
            if (selectedProperty != null)
            {
                DrawProperties(selectedProperty);
            }
            else
            {
                EditorGUILayout.LabelField("Select a property");
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

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