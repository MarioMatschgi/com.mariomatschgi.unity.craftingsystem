using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MM.Systems.InventorySystem;
using System.Linq;

namespace MM.Systems.CraftingSystem
{
    public class CraftingScreenUi : MonoBehaviour
    {
        [Header("General")]
        public Transform craftingCellPanel;
        [Space]
        public List<CraftingRecipe> craftingRecipes;
        [Space]
        public int interactorId;
        public IInteractor interactor;

        [Header("Prefabs")]
        public GameObject craftingCellPrefab;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/MM CraftingSystem/CraftingScreen Ui", false, 10)]
        public static void OnCreate()
        {
#pragma warning disable CS0618 // Type or member is obsolete

            CraftingCanvas[] _canvases = (CraftingCanvas[])FindObjectsOfTypeAll(typeof(CraftingCanvas));
            CraftingCanvas _craftingCanvas = _canvases.Length > 0 ? _canvases[0] : null;
            if (_craftingCanvas == null || _craftingCanvas.gameObject == null || _craftingCanvas.gameObject.scene.name == null || _craftingCanvas.gameObject.scene.name.Equals(string.Empty))
            {
                CraftingCanvas.OnCreate();

                _craftingCanvas = (CraftingCanvas)FindObjectsOfTypeAll(typeof(CraftingCanvas))[0];
            }

            // Create ItemDisplayUi
            GameObject _craftingScreenObj = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Packages/com.mariomatschgi.unity.craftingsystem/Prefabs/CraftingScreenUi.prefab",
                typeof(GameObject)), _craftingCanvas.transform);
            _craftingScreenObj.transform.name = "CraftingScreen";

#pragma warning restore CS0618 // Type or member is obsolete
        }
#endif

        void Awake()
        {
            CraftingCellUi[] _craftingCells = craftingCellPanel.GetComponentsInChildren<CraftingCellUi>();
            int i;
            for (i = 0; i < craftingRecipes.Count; i++)
            {
                // If less children than current recipe create new cell, else set the recipe
                if (_craftingCells.Length < i + 1)
                    Instantiate(craftingCellPrefab, craftingCellPanel).GetComponent<CraftingCellUi>().Setup(craftingRecipes[i], this);
                else
                    craftingCellPanel.GetChild(i).GetComponent<CraftingCellUi>().Setup(craftingRecipes[i], this);
            }
            // Destroy remaining cells
            for (int j = i; j < craftingCellPanel.childCount; j++)
                Destroy(craftingCellPanel.GetChild(j).gameObject);

            // Setup interactor
            IEnumerable<IInteractor> _enumerable = FindObjectsOfType<MonoBehaviour>().OfType<IInteractor>();
            foreach (IInteractor _interactor in _enumerable)
                if (_interactor.interactorId == interactorId)
                {
                    interactor = _interactor;
                    break;
                }
        }

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