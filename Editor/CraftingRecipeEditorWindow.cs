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


        // Private
        Vector2 scrollPos;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        [MenuItem("MM EditorWindows/Crafting recipe editor")]
        public static void Open()
        {
            Open(null);
        }

        public static void Open(CraftingRecipe _craftingRecipe)
        {
            window = GetWindow<CraftingRecipeEditorWindow>("Crafting Recipe Editor");
            window.serializedObject = new SerializedObject(_craftingRecipe);
        }

        protected override void OnGUI()
        {
            if (serializedObject == null)
                return;

            serializedProperty = serializedObject.GetIterator();
            serializedProperty.NextVisible(true);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            DrawProperties(serializedProperty, false);
            EditorGUILayout.EndScrollView();

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