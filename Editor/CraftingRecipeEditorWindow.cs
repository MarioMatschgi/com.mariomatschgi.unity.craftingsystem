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

        protected override void OnGUI()
        {
            serializedProperty = serializedObject.GetIterator();
            serializedProperty.NextVisible(true);

            DrawProperties(serializedProperty);

            /*
            SerializedProperty _p = serializedObject.GetIterator();
            _p.NextVisible(true);
            serializedProperty = _p;

            EditorGUILayout.BeginHorizontal();

            DrawSidebar(serializedProperty);

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
            if (selectedProperty != null)
                DrawProperties(selectedProperty);
            else
                EditorGUILayout.LabelField("Select an item");
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            */

            // Call base, so all gets applyed
            base.OnGUI();
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