using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
    public class ItemDisplayUi : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        ItemData m_item;
        public ItemData item
        {
            get { return m_item; }
            set
            {
                m_item = value;
                UpdateItemDisplay(item);
            }
        }

        [Header("Outlets")]
        public Image image;
        public TMP_Text text;


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

        void Start()
        {
            UpdateItemDisplay(item);
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
            text.text = _item.itemAmount.ToString();
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