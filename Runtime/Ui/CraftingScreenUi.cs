using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MM.Systems.CraftingSystem
{
    public class CraftingScreenUi : MonoBehaviour
    {
        [Header("General")]
        public Transform craftingCellPanel;
        [Space]
        public List<CraftingRecipe> craftingRecipes;

        [Header("Prefabs")]
        public GameObject craftingCellPrefab;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        void Awake()
        {
            CraftingCellUi[] _craftingCells = craftingCellPanel.GetComponentsInChildren<CraftingCellUi>();
            int i;
            for (i = 0; i < craftingRecipes.Count; i++)
            {
                // If less children than current recipe create new cell, else set the recipe
                if (_craftingCells.Length < i + 1)
                    Instantiate(craftingCellPrefab, craftingCellPanel).GetComponent<CraftingCellUi>().Setup(craftingRecipes[i]);
                else
                    craftingCellPanel.GetChild(i).GetComponent<CraftingCellUi>().Setup(craftingRecipes[i]);
            }
            // Destroy remaining cells
            for (int j = i; j < craftingCellPanel.childCount; j++)
                Destroy(craftingCellPanel.GetChild(j).gameObject);
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