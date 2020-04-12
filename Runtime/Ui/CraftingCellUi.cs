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
        public RectTransform itemDisplayParent;
        public Button button;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/MM CraftingSystem/CraftingCell Ui", false, 10)]
        public static void OnCreate()
        {
#pragma warning disable CS0618 // Type or member is obsolete

            CraftingScreenUi[] _screens = (CraftingScreenUi[])FindObjectsOfTypeAll(typeof(CraftingScreenUi));
            CraftingScreenUi _craftingScreen = _screens.Length > 0 ? _screens[0] : null;
            if (_craftingScreen == null || _craftingScreen.gameObject == null || _craftingScreen.gameObject.scene.name == null || _craftingScreen.gameObject.scene.name.Equals(string.Empty))
            {
                CraftingScreenUi.OnCreate();

                _craftingScreen = (CraftingScreenUi)FindObjectsOfTypeAll(typeof(CraftingScreenUi))[0];
            }

            // Create ItemDisplayUi
            GameObject _craftingScreenObj = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Packages/com.mariomatschgi.unity.craftingsystem/Prefabs/CraftingCellUi.prefab",
                typeof(GameObject)), _craftingScreen.craftingCellPanel);
            _craftingScreenObj.transform.name = "CraftingCell";

#pragma warning restore CS0618 // Type or member is obsolete
        }
#endif

        public void Setup(CraftingRecipe _recipe, CraftingScreenUi _craftingScreen)
        {
            // Setup variables
            recipe = _recipe;
            craftingScreen = _craftingScreen;

            // Setup content
            Image[] _images = itemDisplayParent.GetComponentsInChildren<Image>();
            int i;
            for (i = 0; i < recipe.outElements.Count; i++)
            {
                // If less children than current recipe create new cell, else set the recipe
                if (_images.Length < i + 1)
                    Instantiate(imageTextCellPrefab, itemDisplayParent).GetComponent<ItemDisplayUi>().item = recipe.outElements[i];
                else
                    itemDisplayParent.GetChild(i).GetComponent<ItemDisplayUi>().item = recipe.outElements[i];
            }
            // Destroy remaining cells
            for (int j = i; j < itemDisplayParent.childCount; j++)
                Destroy(itemDisplayParent.GetChild(j).gameObject);

            // Setup button callback
            button.onClick.AddListener(delegate { OnButtonPressed(); });
        }

        void Start()
        {

        }

        void OnButtonPressed()
        {
            Debug.Log("BTN Pressed: " + ((MonoBehaviour)craftingScreen.craftor).gameObject.name);

            ItemData[][] _remainingItems = CraftingSystem.TryCrafting(recipe, craftingScreen.craftor.inventoryUi.mainInventory.items);
            if (_remainingItems != null)
                craftingScreen.craftor.inventoryUi.mainInventory.UpdateSlots(_remainingItems);
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