using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM.Systems.InventorySystem;
using MM.Systems.CraftingSystem;

namespace MM.Systems.CraftingSystem
{
    public class ExampleCraftingManager : MonoBehaviour
    {
        public CraftingRecipe recipe;
        public List<ItemData> inItems;
        public List<ItemData> resultItems;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        void Start()
        {
            resultItems = CraftingSystem.TryCrafting(recipe, inItems.ToArray());
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