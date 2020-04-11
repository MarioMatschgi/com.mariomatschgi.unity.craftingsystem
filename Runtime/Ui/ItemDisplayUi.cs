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