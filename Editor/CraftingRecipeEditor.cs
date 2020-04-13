#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

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

        [OnOpenAsset]
        public static bool OnOpen(int _instanceId, int _line)
        {
            CraftingRecipe _craftingRecipe = EditorUtility.InstanceIDToObject(_instanceId) as CraftingRecipe;
            if (_craftingRecipe != null)
            {
                CraftingRecipeEditorWindow.Open(_craftingRecipe);

                return true;
            }

            return false;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open in editor window"))
            {
                CraftingRecipeEditorWindow.Open((CraftingRecipe)target);
            }
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