using System.Collections;
using System.Collections.Generic;
using MM.Systems.InventorySystem;
using MM.Libraries.UI;
using UnityEngine;

namespace MM.Systems.CraftingSystem
{
    [AddComponentMenu("MM CraftingSystem/CraftingHoverPanel")]
    [RequireComponent(typeof(CanvasGroup))]
    public class CraftingHoverPanel : MonoBehaviour
    {
        [Header("General")]
        public CraftingPanelUi craftingScreen;
        [SerializeField]
        CraftingRecipe m_recipe;
        public CraftingRecipe recipe
        {
            get { return m_recipe; }
            set
            {
                m_recipe = value;
                UpdatePanels();
            }
        }
        [Space]
        public float animationTime = .1f;
        public bool isOpen;
        [Space]
        public Vector2 mouseOffset;


        [Header("Outlets")]
        public ItemDisplayDisplayUi ingredientsPanel;
        public ItemDisplayDisplayUi outcomePanel;


        CanvasGroup canvasGroup;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        void Awake()
        {
            // Setup Variables
            canvasGroup = GetComponent<CanvasGroup>();

            // Update Panels
            //UpdatePanels();

            // Hide
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            isOpen = false;
        }

        void Start()
        {

        }

        void Update()
        {
            // Move to mouse
            if (isOpen)
            {
                transform.position = Input.mousePosition + new Vector3(mouseOffset.x, mouseOffset.y, transform.position.z);
            }
        }

        #endregion

        #region Gameplay Methodes
        /*
         *
         *  Gameplay Methodes
         *
         */

        public void UpdateCraftingHoverPanelVisibility(bool _shouldOpen)
        {
            isOpen = _shouldOpen;

            // Manage Hiding/Showing of the CraftingScreen
            if (_shouldOpen)
                StartCoroutine(OpenCraftingHoverPanel());
            else
                StartCoroutine(CloseCraftingHoverPanel());
        }

        public void UpdatePanels()
        {
            if (recipe == null)
                return;

            List<ItemData> _missingItemDatas = CraftingSystem.CanCraft(recipe, craftingScreen.craftor.inventoryUi.mainInventory.items);
            ingredientsPanel.UpdatePanel(recipe.inElements, _missingItemDatas);
            outcomePanel.UpdatePanel(recipe.outElements, _missingItemDatas);
        }

        #endregion

        #region Helper Methodes
        /*
         *
         *  Helper Methodes
         * 
         */

        /// <summary>
        /// Corroutine for opening the Inventory
        /// </summary>
        /// <returns></returns>
        IEnumerator OpenCraftingHoverPanel()
        {
            StopAllCoroutines();
            // Set isOpen
            isOpen = true;

            // Fade
            canvasGroup.FadeIn(animationTime, this, false);
            canvasGroup.blocksRaycasts = true;

            // Wait til anim is finished
            float _time = animationTime;
            while (_time > 0)
            {
                _time -= Time.deltaTime;
                yield return null;
            }
        }

        /// <summary>
        /// Corroutine for closing the Inventory
        /// </summary>
        /// <returns></returns>
        IEnumerator CloseCraftingHoverPanel()
        {
            StopAllCoroutines();
            // Set isOpen
            isOpen = false;

            // Fade
            canvasGroup.FadeOut(animationTime, this, false);
            canvasGroup.blocksRaycasts = false;

            // Wait til anim is finished
            float _time = animationTime;
            while (_time > 0)
            {
                _time -= Time.deltaTime;
                yield return null;
            }
        }

        #endregion
    }
}