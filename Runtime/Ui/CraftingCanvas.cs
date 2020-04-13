using UnityEngine;

namespace MM.Systems.CraftingSystem
{
    [AddComponentMenu("MM CraftingSystem/CraftingCanvas Ui")]
    public class CraftingCanvas : MonoBehaviour
    {


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/MM CraftingSystem/CraftingCanvas Ui", false, 10)]
        public static void OnCreate()
        {
            // Create CraftingCanvas
            GameObject _craftingCanvasObj = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Packages/com.mariomatschgi.unity.craftingsystem/Prefabs/CraftingCanvasUi.prefab",
                typeof(GameObject)));
            _craftingCanvasObj.transform.name = "CraftingCanvas";
        }
#endif

        void Start()
        {

        }

        void Update()
        {

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