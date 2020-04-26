using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
    [AddComponentMenu("MM CraftingSystem/ItemDisplay Ui")]
    public class ItemDisplayUi : MonoBehaviour
    {
        [Header("General")]
        public Color notCraftableColor;
        [SerializeField]
        ItemData m_itemData;
        public ItemData itemData
        {
            get { return m_itemData; }
            set
            {
                m_itemData = value;
                UpdateItemDisplay(itemData);
            }
        }
        [Space]
        [SerializeField]
        bool m_isGrayedOut;
        public bool isGrayedOut
        {
            get { return m_isGrayedOut; }
            set
            {
                m_isGrayedOut = value;
                UpdateIsGrayedOut();
            }
        }

        [Header("Outlets")]
        public Image image;
        public TMP_Text nameText;
        public TMP_Text amountText;


        Color normalImageColor;
        Color normalNameColor;
        Color normalAmountColor;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/MM CraftingSystem/ItemDisplay Ui", false, 10)]
        public static void OnCreate()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            
            CraftingCellUi[] _cells = (CraftingCellUi[])FindObjectsOfTypeAll(typeof(CraftingCellUi));
            CraftingCellUi _cell = _cells.Length > 0 ? _cells[0] : null;
            if (_cell == null || _cell.gameObject == null || _cell.gameObject.scene.name == null || _cell.gameObject.scene.name.Equals(string.Empty))
            {
                CraftingCellUi.OnCreate();

                _cell = (CraftingCellUi)FindObjectsOfTypeAll(typeof(CraftingCellUi))[0];
            }

            // Create ItemDisplayUi
            GameObject _itemDisplayObj = (GameObject)Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath("Packages/com.mariomatschgi.unity.craftingsystem/Prefabs/ItemDisplayUi.prefab",
                typeof(GameObject)), _cell.transform);
            _itemDisplayObj.transform.name = "ItemDisplayUi";

#pragma warning restore CS0618 // Type or member is obsolete
        }
#endif

        public void Setup(ItemData _itemData, bool _isGrayedOut)
        {
            itemData = _itemData;
            isGrayedOut = _isGrayedOut;
        }

        void Awake()
        {
            // Setup variables
            normalImageColor = image.color;
            if (nameText != null)
                normalNameColor = nameText.color;
            normalAmountColor = amountText.color;
        }

        void Start()
        {
            UpdateItemDisplay(itemData);
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

        public void UpdateItemDisplay(ItemData _item)
        {
            // Return if item is null
            if (_item == null || _item.itemPreset == null)
                return;

            // Update sprite
            image.sprite = _item.itemPreset.sprite;

            // Update text
            if (nameText != null)
                nameText.text = _item.itemPreset.name;
            amountText.text = _item.itemAmount.ToString();
        }

        public void UpdateIsGrayedOut()
        {
            if (isGrayedOut)
            {
                // Update sprite
                image.color = notCraftableColor;

                // Update text
                if (nameText != null)
                    nameText.color = notCraftableColor;
                amountText.color = notCraftableColor;
            }
            else
            {
                // Update sprite
                image.color = normalImageColor;

                // Update text
                if (nameText != null)
                    nameText.color = normalNameColor;
                amountText.color = normalAmountColor;
            }
        }

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