using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
    public class CraftingSystem
    {


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        #endregion

        #region Gameplay Methodes
        /*
         *
         *  Gameplay Methodes
         *
         */

        /// <summary>
        /// Returns the items left, null if crafting is not possible
        /// </summary>
        /// <returns></returns>
        public static List<ItemData> TryCrafting(CraftingRecipe _craftingRecipe, params ItemData[] _inItems)
        {
            // Get recipe values
            List<ItemData> _outItems = new List<ItemData>(_craftingRecipe.outElements);
            List<ItemData> _requiredItems = new List<ItemData>(_craftingRecipe.inElements);

            // Set requiredPresetAmountDictionary
            Dictionary<ItemPreset, int> _requiredPresetAmountDictionary = new Dictionary<ItemPreset, int>();
            foreach (ItemData _data in _requiredItems)
            {
                // Fill Dictionary
                if (_requiredPresetAmountDictionary.ContainsKey(_data.itemPreset))
                    _requiredPresetAmountDictionary[_data.itemPreset] = _data.itemAmount;
                else
                    _requiredPresetAmountDictionary.Add(_data.itemPreset, _data.itemAmount);
            }
            // Set inPresetAmountDictionary
            Dictionary<ItemPreset, int> _inPresetAmountDictionary = new Dictionary<ItemPreset, int>();
            foreach (ItemData _data in _inItems)
            {
                // Fill Dictionary
                if (_inPresetAmountDictionary.ContainsKey(_data.itemPreset))
                    _inPresetAmountDictionary[_data.itemPreset] = _data.itemAmount;
                else
                    _inPresetAmountDictionary.Add(_data.itemPreset, _data.itemAmount);
            }

            // Test if requirements are met
            foreach (KeyValuePair<ItemPreset, int> _element in _requiredPresetAmountDictionary)
            {
                // Test if inItems contain, if so remove the amount, check if new amount is greater zero, else cancel crafting
                if (_inPresetAmountDictionary.ContainsKey(_element.Key))
                {
                    _inPresetAmountDictionary[_element.Key] = _inPresetAmountDictionary[_element.Key] - _element.Value;

                    // Not enough of item XYZ
                    if (_inPresetAmountDictionary[_element.Key] < 0)
                        return null;
                }
                // No item XYZ
                else
                    return null;
            }

            // Set leftItems
            List<ItemData> _leftItems = new List<ItemData>();
            foreach (KeyValuePair<ItemPreset, int> _element in _inPresetAmountDictionary)
                // Add to List, if itemAmount is greater than zero
                if (_element.Value > 0)
                    _leftItems.Add(new ItemData(_element.Key, _element.Value));

            // Combine leftItems and outItems
            _outItems.AddRange(_leftItems);

            // Return result
            return _outItems;
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
