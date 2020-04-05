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
            SerializedProperty _p = serializedObject.GetIterator();
            _p.NextVisible(true);
            DrawProperties(_p);
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