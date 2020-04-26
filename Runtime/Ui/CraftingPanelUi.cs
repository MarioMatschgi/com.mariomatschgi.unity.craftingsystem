using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MM.Libraries.UI;

namespace MM.Systems.CraftingSystem
{
    [AddComponentMenu("MM CraftingSystem/CraftingPanel Ui")]
    public class CraftingPanelUi : MonoBehaviour
    {
        [Header("General")]
        public Transform craftingCellPanel;
        public CraftingCellUi[] craftingCells;
        [Space]
        public List<CraftingRecipe> craftingRecipes;
        [Space]
        public int interactorId;
        public ICraftor craftor;
        bool m_isInventoryOpen;
        public bool isCraftingScreenOpen
        {
            get
            {
                return m_isInventoryOpen;
            }
            set
            {
                m_isInventoryOpen = value;

                UpdateCraftingScreenVisibility(m_isInventoryOpen);
            }
        }
        [Space]
        public float animationTime = .1f;
        public bool isFinishedAnimating;

        [Header("Outlets")]
        public CanvasGroup content;
        public CraftingHoverPanel hoverPanel;

        [Header("Prefabs")]
        public GameObject craftingCellPrefab;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/MM CraftingSystem/CraftingPanel Ui", false, 10)]
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
            GameObject _craftingScreenObj = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Packages/com.mariomatschgi.unity.craftingsystem/Prefabs/CraftingPanelUi.prefab",
                typeof(GameObject)), _craftingCanvas.transform);
            _craftingScreenObj.transform.name = "CraftingPanel";

#pragma warning restore CS0618 // Type or member is obsolete
        }
#endif

        void Awake()
        {
            // Setup variables
            IEnumerable<ICraftor> _enumerable = FindObjectsOfType<MonoBehaviour>().OfType<ICraftor>();
            foreach (ICraftor _craftor in _enumerable)
                if (_craftor.interactorId == interactorId)
                {
                    craftor = _craftor;
                    craftor.craftingScreen = this;

                    break;
                }
            hoverPanel.craftingScreen = this;
            isFinishedAnimating = true;
            content.alpha = 0;
            content.blocksRaycasts = false;

            // Setup CraftingCells
            craftingCells = craftingCellPanel.GetComponentsInChildren<CraftingCellUi>();
            int i;
            for (i = 0; i < craftingRecipes.Count; i++)
            {
                // If less children than current recipe create new cell, else set the recipe
                if (craftingCells.Length < i + 1)
                    Instantiate(craftingCellPrefab, craftingCellPanel).GetComponent<CraftingCellUi>().Setup(craftingRecipes[i], this);
                else
                    craftingCellPanel.GetChild(i).GetComponent<CraftingCellUi>().Setup(craftingRecipes[i], this);
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

        public void UpdateCraftingScreenVisibility(bool _shouldOpen)
        {
            if (!isFinishedAnimating)
                return;

            // Manage Hiding/Showing of the CraftingScreen
            if (_shouldOpen)
                StartCoroutine(OpenCraftingScreen());
            else
                StartCoroutine(CloseCraftingScreen());

            // Update Crafting cells
            for (int i = 0; i < craftingRecipes.Count; i++)
                craftingCellPanel.GetChild(i).GetComponent<CraftingCellUi>().OnInventoryChanged();
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
        IEnumerator OpenCraftingScreen()
        {
            // Set isFinishedAnimating
            isFinishedAnimating = false;

            // Fade
            content.FadeIn(animationTime, this, false);
            content.blocksRaycasts = true;

            // Wait til anim is finished
            float _time = animationTime;
            while (_time > 0)
            {
                _time -= Time.deltaTime;
                yield return null;
            }

            // SetInventoriesActive
            //SetInventoriesActive(true);

            // Set isFinishedAnimating
            isFinishedAnimating = true;
        }

        /// <summary>
        /// Corroutine for closing the Inventory
        /// </summary>
        /// <returns></returns>
        IEnumerator CloseCraftingScreen()
        {
            // Set isFinishedAnimating
            isFinishedAnimating = false;

            // SetInventoriesActive
            //SetInventoriesActive(false);

            // Fade
            content.FadeOut(animationTime, this, false);
            content.blocksRaycasts = false;

            // Wait til anim is finished
            float _time = animationTime;
            while (_time > 0)
            {
                _time -= Time.deltaTime;
                yield return null;
            }

            // Set isFinishedAnimating
            isFinishedAnimating = true;
        }

        #endregion
    }
}