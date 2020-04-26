using System;
using System.Collections.Generic;
using MM.Systems.InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MM.Systems.CraftingSystem
{
    [AddComponentMenu("MM CraftingSystem/CraftingCell Ui")]
    public class CraftingCellUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("General")]
        public CraftingRecipe recipe;
        public CraftingPanelUi craftingScreen;

        [Header("Colors")]
        public Color notCraftableColor;

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

            CraftingPanelUi[] _screens = (CraftingPanelUi[])FindObjectsOfTypeAll(typeof(CraftingPanelUi));
            CraftingPanelUi _craftingScreen = _screens.Length > 0 ? _screens[0] : null;
            if (_craftingScreen == null || _craftingScreen.gameObject == null || _craftingScreen.gameObject.scene.name == null || _craftingScreen.gameObject.scene.name.Equals(string.Empty))
            {
                CraftingPanelUi.OnCreate();

                _craftingScreen = (CraftingPanelUi)FindObjectsOfTypeAll(typeof(CraftingPanelUi))[0];
            }

            // Create ItemDisplayUi
            GameObject _craftingScreenObj = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Packages/com.mariomatschgi.unity.craftingsystem/Prefabs/CraftingCellUi.prefab",
                typeof(GameObject)), _craftingScreen.craftingCellPanel);
            _craftingScreenObj.transform.name = "CraftingCell";

#pragma warning restore CS0618 // Type or member is obsolete
        }
#endif

        public void Setup(CraftingRecipe _recipe, CraftingPanelUi _craftingScreen)
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
                    Instantiate(imageTextCellPrefab, itemDisplayParent).GetComponent<ItemDisplayUi>().itemData = recipe.outElements[i];
                else
                    itemDisplayParent.GetChild(i).GetComponent<ItemDisplayUi>().itemData = recipe.outElements[i];
            }
            // Destroy remaining cells
            for (int j = i; j < itemDisplayParent.childCount; j++)
                Destroy(itemDisplayParent.GetChild(j).gameObject);

            // Setup button callback
            button.onClick.AddListener(delegate { OnButtonPressed(); });

            // Setup craftableCallback
            craftingScreen.craftor.inventoryUi.inventoryChangedCallback += OnInventoryChanged;
        }

        void Start()
        {

        }

        void OnDestroy()
        {
            // Remove craftableCallback
            if (craftingScreen != null && craftingScreen.craftor != null && craftingScreen.craftor.inventoryUi != null)
                if (craftingScreen.craftor.inventoryUi.inventoryChangedCallback != null)
                    craftingScreen.craftor.inventoryUi.inventoryChangedCallback -= OnInventoryChanged;
        }

        public void OnInventoryChanged()
        {
            if (CraftingSystem.CanCraft(recipe, craftingScreen.craftor.inventoryUi.mainInventory.items).Count <= 0)
                button.image.color = button.colors.normalColor;
            else
                button.image.color = notCraftableColor;
        }

        void OnButtonPressed()
        {
            List<ItemData> _notFit = new List<ItemData>();  // ToDo: Manage notFit items, eg drop them

            // Try crafting
            ItemData[][] _remainingItems = CraftingSystem.TryCrafting(recipe, out _notFit, craftingScreen.craftor.inventoryUi.mainInventory.items);
            if (_remainingItems != null)
                craftingScreen.craftor.inventoryUi.mainInventory.UpdateSlots(_remainingItems);

            // Drop not fit items
            foreach (ItemData _data in _notFit)
                InventoryUiManager.instance.DropItem(_data, craftingScreen.craftor);

            // Invoke Event
            if (craftingScreen.craftor.inventoryUi.inventoryChangedCallback != null)
                craftingScreen.craftor.inventoryUi.inventoryChangedCallback.Invoke();
        }

        public void OnPointerEnter(PointerEventData _eventData)
        {
            craftingScreen.hoverPanel.recipe = recipe;
            craftingScreen.hoverPanel.UpdateCraftingHoverPanelVisibility(true);
        }

        public void OnPointerExit(PointerEventData _eventData)
        {
            craftingScreen.hoverPanel.UpdateCraftingHoverPanelVisibility(false);
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