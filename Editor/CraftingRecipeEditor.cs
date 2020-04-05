#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MM.Systems.CraftingSystem
{
    [CustomEditor(typeof(CraftingRecipe))]
    public class CraftingRecipeEditor : Editor
    {


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open in editor window"))
            {
                CraftingRecipeEditorWindow.Open((CraftingRecipe)target);
            }

            base.OnInspectorGUI();
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