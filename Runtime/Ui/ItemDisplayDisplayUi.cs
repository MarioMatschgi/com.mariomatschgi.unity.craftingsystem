using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
    public class ItemDisplayDisplayUi : MonoBehaviour
    {
        [Header("General")]
        public List<ItemData> itemDatas;
        public GameObject itemDisplayPrefab;


        [Header("Outlets")]
        public RectTransform itemDisplayParent;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

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

        public void UpdatePanel(List<ItemData> _itemDatas, List<ItemData> _missingDatas)
        {
            // Set itemDatas
            itemDatas = _itemDatas;

            // Setup content
            ItemDisplayUi[] _itemDisplays = itemDisplayParent.GetComponentsInChildren<ItemDisplayUi>();
            int i;
            for (i = 0; i < itemDatas.Count; i++)
            {
                // If less children than current recipe create new cell, else set the recipe
                if (_itemDisplays.Length < i + 1)
                    Instantiate(itemDisplayPrefab, itemDisplayParent).GetComponent<ItemDisplayUi>().Setup(itemDatas[i], _missingDatas.Contains(itemDatas[i]));
                else
                    itemDisplayParent.GetChild(i).GetComponent<ItemDisplayUi>().Setup(itemDatas[i], _missingDatas.Contains(itemDatas[i]));
            }
            // Destroy remaining cells
            for (int j = i; j < itemDisplayParent.childCount; j++)
                Destroy(itemDisplayParent.GetChild(j).gameObject);
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