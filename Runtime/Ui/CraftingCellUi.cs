using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MM.Systems.CraftingSystem
{
    public class CraftingCellUi : MonoBehaviour
    {
        [Header("General")]
        public CraftingRecipe recipe;

        [Header("Outlets")]
        public Image iconImage;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        public void Setup(CraftingRecipe _recipe)
        {
            // Setup variables
            recipe = _recipe;

            // Setup content
            //iconImage.sprite = _recipe
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