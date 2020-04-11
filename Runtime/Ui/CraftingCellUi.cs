using System.Collections;
using System.Collections.Generic;
using MM.Systems.InventorySystem;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace MM.Systems.CraftingSystem
{
    public class CraftingCellUi : MonoBehaviour
    {
        [Header("General")]
        public CraftingRecipe recipe;
        public CraftingScreenUi craftingScreen;

        [Header("Outlets")]
        public GameObject imageTextCellPrefab;
        public RectTransform imageParent;
        public Button button;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        public void Setup(CraftingRecipe _recipe, CraftingScreenUi _craftingScreen)
        {
            // Setup variables
            recipe = _recipe;
            craftingScreen = _craftingScreen;

            // Setup content
            Image[] _images = imageParent.GetComponentsInChildren<Image>();
            int i;
            for (i = 0; i < recipe.outElements.Count; i++)
            {
                // If less children than current recipe create new cell, else set the recipe
                if (_images.Length < i + 1)
                    Instantiate(imageTextCellPrefab, imageParent).GetComponent<ItemDisplayUi>().item = recipe.outElements[i];
                else
                    imageParent.GetChild(i).GetComponent<ItemDisplayUi>().item = recipe.outElements[i];
            }
            // Destroy remaining cells
            for (int j = i; j < imageParent.childCount; j++)
                Destroy(imageParent.GetChild(j).gameObject);

            // Setup button callback
            button.onClick.AddListener(delegate { OnButtonPressed(); });
        }

        void Start()
        {

        }

        void OnButtonPressed()
        {
            Debug.Log("BTN Pressed: " + ((MonoBehaviour)craftingScreen.interactor).gameObject.name);

            //List<ItemData> _remainingItems = CraftingSystem.TryCrafting(recipe, craftingScreen.interactor.inventoryUi.mainInventory.items.Single());
            //craftingScreen.interactor.inventoryUi.mainInventory.UpdateSlots(_remainingItems);


            ItemData[][] _remainingItems = CraftingSystem.TryCrafting(recipe, craftingScreen.interactor.inventoryUi.mainInventory.items);

            foreach (ItemData[] _datas in _remainingItems)
            {
                foreach (ItemData _data in _datas)
                {
                    if (_data != null && _data.itemPreset != null)
                        Debug.Log("N: " + _data.itemPreset.name + " A: " + _data.itemAmount);
                }
            }

            craftingScreen.interactor.inventoryUi.mainInventory.UpdateSlots(_remainingItems);
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